using UnityEngine;

namespace GenshinImpactMovementSystem
{
  public class PlayerFallingState : PlayerAirborneState
  {
    private PlayerFallData fallData;
    private Vector3 playerPositionOnEnter;

    public PlayerFallingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
      fallData = airborneData.fallData;
    }
    #region IState Methods
    public override void Enter()
    {
      base.Enter();

      StartAnimation(stateMachine.player.animationData.FallParameterHash);

      playerPositionOnEnter = stateMachine.player.transform.position;

      stateMachine.ReusableData.MovementSpeedModifier = 0f;

      ResetVerticalVelocity();


    }

    public override void Exit()
    {
      base.Exit();

      StopAnimation(stateMachine.player.animationData.FallParameterHash);

    }

    public override void PhysicsUpdate()
    {
      base.PhysicsUpdate();

      LimitVerticalVelocity();
    }
    #endregion

    #region Mian Methods
    private void LimitVerticalVelocity()
    {
      Vector3 playerVerticalVelocity = GetPlayerVerticalVelocity();

      if (playerVerticalVelocity.y >= -fallData.FallSpeedLimit)
        return;

      Vector3 limitedVelocityForce = new Vector3(0f, -airborneData.fallData.FallSpeedLimit - playerVerticalVelocity.y, 0f);

      stateMachine.player.rb.AddForce(limitedVelocityForce, ForceMode.VelocityChange);
    }

    protected override void ResetSprintState()
    {

    }
    protected override void OnContactWithGroundExited(Collider other)
    {
      float fallDistance = Mathf.Abs(playerPositionOnEnter.y - stateMachine.player.transform.position.y);

      if (fallDistance < fallData.MinimumDistanceToBeConsideredHardFall)
      {
        stateMachine.ChangeState(stateMachine.lightLandingState);

        return;
      }

      if (stateMachine.ReusableData.ShouldWalk &&
       !stateMachine.ReusableData.ShouldSprint ||
        stateMachine.ReusableData.MovementInput == Vector2.zero)
      {
        stateMachine.ChangeState(stateMachine.hardLandingState);

        return;
      }

      stateMachine.ChangeState(stateMachine.rollingLandingState);
    }

    protected override void OnContactWithGround(Collider collider)
    {
      float fallDistance = playerPositionOnEnter.y - stateMachine.player.transform.position.y;

      if (fallDistance < airborneData.fallData.MinimumDistanceToBeConsideredHardFall)
      {
        stateMachine.ChangeState(stateMachine.lightLandingState);

        return;
      }

      if (stateMachine.ReusableData.ShouldWalk && !stateMachine.ReusableData.ShouldSprint || stateMachine.ReusableData.MovementInput == Vector2.zero)
      {
        stateMachine.ChangeState(stateMachine.hardLandingState);

        return;
      }

      stateMachine.ChangeState(stateMachine.rollingLandingState);

    }
    #endregion
  }
}