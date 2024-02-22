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

      stateMachine.ReusableData.MovementOnDecelerationForce = movementData.StopData.HardDecelerationForce;
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
