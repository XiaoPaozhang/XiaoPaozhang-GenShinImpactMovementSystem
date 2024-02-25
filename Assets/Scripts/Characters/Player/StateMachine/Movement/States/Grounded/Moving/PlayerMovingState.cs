namespace GenshinImpactMovementSystem
{
  public class PlayerMovingState : PlayerGroundedState
  {
    public PlayerMovingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
      base.Enter();

      StartAnimation(stateMachine.player.animationData.MovingParameterHash);

    }
    public override void Exit()
    {
      base.Exit();

      StopAnimation(stateMachine.player.animationData.MovingParameterHash);

    }
  }
}
