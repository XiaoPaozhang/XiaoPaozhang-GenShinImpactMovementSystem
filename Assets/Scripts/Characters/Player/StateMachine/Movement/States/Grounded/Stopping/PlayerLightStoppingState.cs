namespace XFramework.FSM
{
  public class PlayerLightStoppingState : PlayerStoppingState
  {
    public PlayerLightStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region IState methods
    public override void Enter()
    {
      base.Enter();

      stateMachine.ReusableData.MovementDecelerationForce = movementData.StopData.LightDecelerationForce;

      stateMachine.ReusableData.CurrentJumpForce = airborneData.jumpData.WeakForce;
    }

    #endregion
  }
}
