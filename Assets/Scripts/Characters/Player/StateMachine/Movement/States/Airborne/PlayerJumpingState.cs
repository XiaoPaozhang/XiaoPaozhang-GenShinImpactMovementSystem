
using UnityEngine;
using UnityEngine.InputSystem;

namespace XFramework.FSM
{
  public class PlayerJumpingState : PlayerAirborneState
  {
    private bool shouldKeepRotating;
    private bool canStartFalling;
    public PlayerJumpingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }
    #region IState methods
    public override void Enter()
    {
      base.Enter();

      //进入跳跃状态,将移动系数归零,无法移动
      stateMachine.ReusableData.MovementSpeedModifier = 0f;

      stateMachine.ReusableData.MovementDecelerationForce = airborneData.jumpData.DecelerationForce;

      stateMachine.ReusableData.RotationData = airborneData.jumpData.rotationData;

      shouldKeepRotating = stateMachine.ReusableData.MovementInput != Vector2.zero;

      Jump();
    }
    public override void Exit()
    {
      base.Exit();

      SetBaseRotationData();

      canStartFalling = false;
    }
    public override void Update()
    {
      base.Update();

      if (!canStartFalling && IsMovingUp(0f))
      {
        canStartFalling = true;
      }

      if (!canStartFalling || IsMovingUp(0f))
        return;

      stateMachine.ChangeState(stateMachine.fallingState);
    }

    public override void PhysicsUpdate()
    {
      base.PhysicsUpdate();

      if (shouldKeepRotating)
      {
        RotateTowardsTargetRotation();
      }

      if (IsMovingUp())
      {
        DecelerateVertically();
      }
    }
    #endregion

    #region main methods
    private void Jump()
    {
      //获取当前跳跃力度
      Vector3 JumpForce = stateMachine.ReusableData.CurrentJumpForce;

      //获取玩家面朝向
      Vector3 jumpDirection = stateMachine.player.transform.forward;

      //如果有在输入
      if (shouldKeepRotating)
      {
        UpdateTargetRotation(GetMovementInputDirection());

        //改为输入后,玩家该旋转到的角度
        jumpDirection = GetTargetRotationDirection(stateMachine.ReusableData.CurrentTargetRotation.y);
      }

      JumpForce.x *= jumpDirection.x;
      JumpForce.z *= jumpDirection.z;

      JumpForce = GetJumpForceOnSlope(JumpForce);

      ResetVelocity();

      stateMachine.player.rb.AddForce(JumpForce, ForceMode.VelocityChange);
    }

    private Vector3 GetJumpForceOnSlope(Vector3 JumpForce)
    {
      Vector3 capsuleColliderCenterInWorldSpace =
          stateMachine.player.ColliderUtility.
          CapsuleColliderData.Collider.bounds.center;

      Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);

      if (Physics.Raycast(
        downwardsRayFromCapsuleCenter, out RaycastHit hit,
       airborneData.jumpData.JumpToGroundRayDistance,
        stateMachine.player.LayerData.GroundLayer,
        QueryTriggerInteraction.Ignore
      ))
      {
        float groundAngle = Vector3.Angle(hit.normal, -downwardsRayFromCapsuleCenter.direction);

        if (IsMovingUp())
        {
          float forceModifier = airborneData.jumpData.JumpForceModifierOnSlopeUpwards.Evaluate(groundAngle);
          JumpForce.x *= forceModifier;
          JumpForce.z *= forceModifier;
        }

        if (IsMovingDown())
        {
          float forceModifier = airborneData.jumpData.JumpForceModifierOnSlopeDownwards.Evaluate(groundAngle);
          JumpForce.y *= forceModifier;
        }
      }

      return JumpForce;
    }

    #endregion

    #region reusable methods
    protected override void ResetSprintState()
    {
      // base.ResetSprintState();
    }

    #endregion


    #region input methods
    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
    }

    #endregion
  }
}
