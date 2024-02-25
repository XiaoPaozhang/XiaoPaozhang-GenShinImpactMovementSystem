namespace XFramework.FSM
{
  public class PlayerHardStoppingState : PlayerStoppingState
  {
    public PlayerHardStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region IState methods
    public override void Enter()
    {
      base.Enter();

      StartAnimation(stateMachine.player.animationData.HardStopParameterHash);

      stateMachine.ReusableData.MovementDecelerationForce = movementData.StopData.HardDecelerationForce;

      stateMachine.ReusableData.CurrentJumpForce = airborneData.jumpData.StrongForce;
    }
    public override void Exit()
    {
      base.Exit();

      StopAnimation(stateMachine.player.animationData.HardStopParameterHash);

    }
    #endregion

    #region Reusable methods
    protected override void OnMove()
    {
      if (stateMachine.ReusableData.ShouldWalk)
        return;
      stateMachine.ChangeState(stateMachine.runningState);
    }

    #endregion
  }
}
