using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshinImpactMovementSystem
{
  public class PlayerRunningState : PlayerMovingState
  {
    private PlayerSprintData sprintData;
    private float startTime;//记录时间
    public PlayerRunningState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
      sprintData = movementData.SprintData;
    }
    #region IState methods
    public override void Update()
    {
      base.Update();

      //如果不应该溜达
      if (!stateMachine.ReusableData.ShouldWalk)
        return;

      if (Time.time < startTime + sprintData.RunToWalkTime)
        return;

      //否则,停止跑步
      StopRunning();
    }

    public override void Enter()
    {
      // 设置正确的速度修饰符
      stateMachine.ReusableData.MovementSpeedModifier = movementData.RunData.SpeedModifier;

      base.Enter();

      StartAnimation(stateMachine.player.animationData.RunParameterHash);

      stateMachine.ReusableData.CurrentJumpForce = airborneData.jumpData.MediumForce;

      startTime = Time.time;
    }
    public override void Exit()
    {
      base.Exit();

      StopAnimation(stateMachine.player.animationData.RunParameterHash);

    }
    #endregion

    #region main methods
    private void StopRunning()
    {
      if (stateMachine.ReusableData.MovementInput == Vector2.zero)
      {
        stateMachine.ChangeState(stateMachine.idlingState);
      }

      stateMachine.ChangeState(stateMachine.runningState);
    }
    #endregion

    #region  input methods
    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
      //切换状态为中停止状态
      stateMachine.ChangeState(stateMachine.mediumStoppingState);

      base.OnMovementCanceled(context);
    }
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
