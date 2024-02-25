namespace GenshinImpactMovementSystem
{
  public class PlayerMediumStoppingState : PlayerStoppingState
  {
    public PlayerMediumStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }


    #region IState methods
    public override void Enter()
    {
      base.Enter();

      StartAnimation(stateMachine.player.animationData.MediumStopParameterHash);

      stateMachine.ReusableData.MovementDecelerationForce = movementData.StopData.MediumDecelerationForce;

      stateMachine.ReusableData.CurrentJumpForce = airborneData.jumpData.MediumForce;
    }
    public override void Exit()
    {
      base.Exit();

      StopAnimation(stateMachine.player.animationData.MediumStopParameterHash);

    }
    #endregion
  }
}
