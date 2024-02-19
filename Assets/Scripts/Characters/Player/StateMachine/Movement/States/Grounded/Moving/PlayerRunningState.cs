using UnityEngine.InputSystem;

namespace XFramework.FSM
{
  public class PlayerRunningState : PlayerMovingState
  {
    public PlayerRunningState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }
    public override void Enter()
    {
      base.Enter();

      // 设置正确的速度修饰符
      stateMachine.ReusableData.MovementSpeedModifier = movementData.RunData.SpeedModifier;
    }
    #region  input methods
    //溜达状态切换时
    protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
    {
      base.OnWalkToggleStarted(context);


      //切换状态为奔跑
      stateMachine.ChangeState(stateMachine.walkingState);
    }
    #endregion
  }
}
