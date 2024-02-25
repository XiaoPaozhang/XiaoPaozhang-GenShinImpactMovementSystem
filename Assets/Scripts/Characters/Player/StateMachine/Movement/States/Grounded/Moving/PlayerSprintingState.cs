using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshinImpactMovementSystem
{
  public class PlayerSprintingState : PlayerMovingState
  {
    private PlayerSprintData sprintData;
    private float startTime;//记录时间
    private bool keepSprintingInput;//是否保持冲刺输入
    private bool shouldResetSprintState;

    public PlayerSprintingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
      sprintData = movementData.SprintData;
    }
    #region IState methods
    public override void Enter()
    {
      stateMachine.ReusableData.MovementSpeedModifier = sprintData.SpeedModifier;

      base.Enter();

      StartAnimation(stateMachine.player.animationData.SprintParameterHash);

      stateMachine.ReusableData.CurrentJumpForce = airborneData.jumpData.StrongForce;

      shouldResetSprintState = true;

      startTime += Time.time;
    }
    public override void Exit()
    {
      base.Exit();

      StopAnimation(stateMachine.player.animationData.SprintParameterHash);

      if (shouldResetSprintState)
      {

        keepSprintingInput = false;

        stateMachine.ReusableData.ShouldSprint = false;
      }

    }
    public override void Update()
    {
      base.Update();

      //是否保持冲刺状态
      if (keepSprintingInput)
        return;

      //是否在疾跑状态可预留时间范围内
      if (Time.time < startTime + sprintData.SprintToRunTime)
        return;

      //否则停止冲刺,切换为冲刺之后的状态
      StopSprinting();
    }

    #endregion

    #region main methods
    private void StopSprinting()
    {
      // 正在输入就是跑步状态,不在输入就是停止状态
      if (stateMachine.ReusableData.MovementInput == Vector2.zero)
      {
        stateMachine.ChangeState(stateMachine.idlingState);
        return;
      }

      stateMachine.ChangeState(stateMachine.runningState);
    }
    #endregion

    #region reusable methods
    protected override void AddInputActionsCallbacks()
    {
      base.AddInputActionsCallbacks();

      stateMachine.player.Input.playerActions.Sprint.performed += OnSprintPerformed;
    }


    protected override void RemoveInputActionsCallbacks()
    {
      base.RemoveInputActionsCallbacks();

      stateMachine.player.Input.playerActions.Sprint.performed -= OnSprintPerformed;
    }

    protected override void OnFall()
    {
      shouldResetSprintState = false;

      base.OnFall();

    }
    #endregion

    #region input methods

    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
      //切换状态为重停止状态
      stateMachine.ChangeState(stateMachine.hardStoppingState);

      base.OnMovementCanceled(context);
    }
    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {
      shouldResetSprintState = false;

      base.OnJumpStarted(context);
    }

    // 按住了shift持续1秒生效
    private void OnSprintPerformed(InputAction.CallbackContext context)
    {
      keepSprintingInput = true;

      stateMachine.ReusableData.ShouldSprint = true;

    }

    #endregion
  }
}
