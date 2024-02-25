using UnityEngine;

namespace XFramework.FSM
{
  public class PlayerAirborneState : PlayerMovementState
  {
    public PlayerAirborneState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }
    #region IState methods
    public override void Enter()
    {
      base.Enter();

      StartAnimation(stateMachine.player.animationData.AirborneParameterHash);

      ResetSprintState();
    }
    public override void Exit()
    {
      base.Exit();

      StopAnimation(stateMachine.player.animationData.AirborneParameterHash);

    }

    #endregion

    #region reusable methods
    protected override void OnContactWithGround(Collider other)
    {
      // base.OnContactWithGround(other);

      stateMachine.ChangeState(stateMachine.lightLandingState);
    }

    protected virtual void ResetSprintState()
    {
      stateMachine.ReusableData.ShouldSprint = false;
    }
    #endregion
  }
}
