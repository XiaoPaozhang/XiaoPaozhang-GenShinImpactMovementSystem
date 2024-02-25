using UnityEngine.InputSystem;

namespace XFramework.FSM
{
  public class PlayerLandingState : PlayerGroundedState
  {
    public PlayerLandingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {

    }
    public override void Enter()
    {
      base.Enter();

      StartAnimation(stateMachine.player.animationData.LandingParameterHash);

    }
    public override void Exit()
    {
      base.Exit();

      StopAnimation(stateMachine.player.animationData.LandingParameterHash);

    }
  }
}
