using UnityEngine.InputSystem;

namespace XFramework.FSM
{
  public class PlayerWalkingState : PlayerMovingState
  {
    private PlayerWalkData walkData;
    public PlayerWalkingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
      walkData = movementData.WalkData;
    }
    public override void Enter()
    {
      // 设置正确的速度修饰符
      stateMachine.ReusableData.MovementSpeedModifier = walkData.SpeedModifier;

      stateMachine.ReusableData.BackWardsCameraRecenteringData = walkData.BackwardsCameraRecenteringData;

      base.Enter();

      StartAnimation(stateMachine.player.animationData.WalkParameterHash);

      stateMachine.ReusableData.CurrentJumpForce = airborneData.jumpData.WeakForce;
    }

    public override void Exit()
    {
      base.Exit();

      StopAnimation(stateMachine.player.animationData.WalkParameterHash);

      SetBaseCameraRecenteringData();
    }

    #region  input methods
    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
      //切换状态为轻停止状态
      stateMachine.ChangeState(stateMachine.lightStoppingState);

      base.OnMovementCanceled(context);
    }
    //溜达状态切换时
    protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
    {
      base.OnWalkToggleStarted(context);

      //切换状态为奔跑
      stateMachine.ChangeState(stateMachine.runningState);
    }
    #endregion
  }
}
