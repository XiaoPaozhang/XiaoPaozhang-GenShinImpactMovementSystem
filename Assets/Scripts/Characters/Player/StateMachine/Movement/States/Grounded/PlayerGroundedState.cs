using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace XFramework.FSM
{
  public class PlayerGroundedState : PlayerMovementState
  {
    public PlayerGroundedState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }
    #region IState Methods
    public override void Enter()
    {
      base.Enter();

      StartAnimation(stateMachine.player.animationData.GroundedParameterHash);

      UpdateShouldSprintState();

      UpdateCameraRecenteringState(stateMachine.ReusableData.MovementInput);
    }
    public override void Exit()
    {
      base.Exit();

      StopAnimation(stateMachine.player.animationData.GroundedParameterHash);

    }
    public override void PhysicsUpdate()
    {
      base.PhysicsUpdate();

      Float();
    }
    #endregion
    #region Main Methods
    private void UpdateShouldSprintState()
    {
      if (!stateMachine.ReusableData.ShouldSprint)
        return;
      if (stateMachine.ReusableData.MovementInput != Vector2.zero)
        return;

      stateMachine.ReusableData.ShouldSprint = false;
    }
    private void Float()
    {
      // 获取胶囊碰撞体的中心点在世界空间中的坐标
      Vector3 capsuleColliderCenterInWorldSpace = stateMachine.player.ColliderUtility.CapsuleColliderData.Collider.bounds.center;

      // 创建一个从胶囊碰撞体中心点向下的射线
      Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);

      // 使用射线检测，检查胶囊碰撞体下方是否有地面
      if (Physics.Raycast(
          downwardsRayFromCapsuleCenter,
          out RaycastHit hit,//用于从光线投射中获取信息
          stateMachine.player.ColliderUtility.SlopeData.FloatRayDistance,
          stateMachine.player.LayerData.GroundLayer,
          QueryTriggerInteraction.Ignore
          ))
      {
        //Vector3.Angle: 计算两个方向向量的夹角角度数
        //hit.normal: 是一个向量，表示射线检测（Raycast）命中的表面的法线
        //new Ray().direction: 获取射线方向,这里我们需要的是向上的,和地面向量一致,所以要取反
        float groundAngle = Vector3.Angle(hit.normal, -downwardsRayFromCapsuleCenter.direction);
        //通过我们自定义设置的动画曲线,传入斜坡角度,输出应该在斜坡上行走的速度系数
        float slopeSpeedModifier = SetSlopeSpeedModifierOnAngle(groundAngle);

        if (slopeSpeedModifier == 0f)
          return;

        // 计算从胶囊碰撞体中心到地面的距离
        // 考虑到了玩家缩放,所以中心点世界坐标y轴先乘以玩家y轴缩放再减去的检测点距离
        float distanceToFloatingPoint
        = stateMachine.player.ColliderUtility.CapsuleColliderData.ColliderCenterInLocalSpace.y
        * stateMachine.player.transform.localScale.y
        - hit.distance;

        // 如果距离为0，那么不需要进行浮动，直接返回
        if (distanceToFloatingPoint == 0)
          return;

        // 计算需要提升的高度，这个高度是根据角色当前的垂直速度和斜坡的高度百分比计算的
        float amountToLift
        = distanceToFloatingPoint
        * stateMachine.player.ColliderUtility.SlopeData.StepReachForce
        - GetPlayerVerticalVelocity().y;

        // 创建一个向上的力
        Vector3 liftForce = new Vector3(0f, amountToLift, 0f);

        // 对角色施加这个力，使角色浮动起来
        stateMachine.player.rb.AddForce(liftForce, ForceMode.VelocityChange);
      }
    }

    private float SetSlopeSpeedModifierOnAngle(float angle)
    {
      // AnimationCurve 动画曲线.Evaluate(x值),可以获取到对应y值的量
      //这里曲线x值为角度,y值为对应得玩家在走斜坡时的速度系数
      float slopeSpeedModifier = movementData.SlopeSpeedAngle.Evaluate(angle);

      if (stateMachine.ReusableData.MovementSlopesSpeedModifier != slopeSpeedModifier)
      {
        stateMachine.ReusableData.MovementSlopesSpeedModifier = slopeSpeedModifier;

        UpdateCameraRecenteringState(stateMachine.ReusableData.MovementInput);
      }

      return slopeSpeedModifier;
    }
    private bool IsThereGroundUnderneath()
    {
      PlayerTriggerColliderData triggerColliderData = stateMachine.player.ColliderUtility.TriggerColliderData;

      Vector3 groundColliderCenterInWorldSpace = triggerColliderData.GroundCheckCollider.bounds.center;

      Collider[] overlappedGroundColliders = Physics.OverlapBox(groundColliderCenterInWorldSpace, triggerColliderData.GroundCheckColliderExtents, triggerColliderData.GroundCheckCollider.transform.rotation, stateMachine.player.LayerData.GroundLayer, QueryTriggerInteraction.Ignore);

      return overlappedGroundColliders.Length > 0;
    }
    #endregion

    #region  可重用方法

    protected override void AddInputActionsCallbacks()
    {
      base.AddInputActionsCallbacks();
      stateMachine.player.Input.playerActions.Dash.started += OnDashStarted;
      stateMachine.player.Input.playerActions.Jump.started += OnJumpStarted;
    }

    protected override void RemoveInputActionsCallbacks()
    {
      base.RemoveInputActionsCallbacks();
      stateMachine.player.Input.playerActions.Dash.started -= OnDashStarted;
      stateMachine.player.Input.playerActions.Jump.started -= OnJumpStarted;
    }
    protected virtual void OnMove()
    {
      if (stateMachine.ReusableData.ShouldSprint)
      {
        stateMachine.ChangeState(stateMachine.sprintingState);
        return;
      }

      //如果应该为溜达
      if (stateMachine.ReusableData.ShouldWalk)
      {
        //通过状态机改变状态为溜达
        stateMachine.ChangeState(stateMachine.walkingState);
        return;
      }

      //通过状态机改变状态为奔跑
      stateMachine.ChangeState(stateMachine.runningState);

    }

    protected override void OnContactWithGroundExited(Collider other)
    {
      if (IsThereGroundUnderneath())
        return;

      Vector3 capsuleColliderCenterInWorldSpace =
      stateMachine.player.
      ColliderUtility.CapsuleColliderData.
      Collider.bounds.center;

      Ray downwardsRayFromCapsuleBottom =
      new Ray(capsuleColliderCenterInWorldSpace
          - stateMachine.player.ColliderUtility.
         CapsuleColliderData.ColliderVerticalExtents,
         Vector3.down);

      if (!Physics.Raycast(
        downwardsRayFromCapsuleBottom,
         out _,
         movementData.GroundToFallRayDistance,
         stateMachine.player.LayerData.GroundLayer,
         QueryTriggerInteraction.Ignore
         ))
      {
        OnFall();
      }

    }

    protected virtual void OnFall()
    {
      stateMachine.ChangeState(stateMachine.fallingState);
    }
    protected override void OnMovementPerformed(InputAction.CallbackContext context)
    {
      base.OnMovementPerformed(context);

      UpdateTargetRotation(GetMovementInputDirection());
    }
    #endregion

    #region Input Methods
    // 冲刺时
    protected virtual void OnDashStarted(InputAction.CallbackContext context)
    {
      stateMachine.ChangeState(stateMachine.dashingState);
    }

    //跳跃时
    protected virtual void OnJumpStarted(InputAction.CallbackContext context)
    {
      Debug.Log("<color=yellow>切换为跳跃状态</color>");
      stateMachine.ChangeState(stateMachine.jumpingState);
    }

    #endregion
  }
}
