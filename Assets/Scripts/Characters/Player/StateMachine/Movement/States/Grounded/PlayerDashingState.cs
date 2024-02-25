using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshinImpactMovementSystem
{
  public class PlayerDashingState : PlayerGroundedState
  {
    private PlayerDashData dashData;

    private float startTime;//冲刺起始时间
    private int consecutiveDashesUsed;//连续冲刺已使用数

    private bool shouldKeepRotating;//是否应该保持旋转

    public PlayerDashingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
      dashData = movementData.DashData;
    }
    #region IState methods
    public override void Enter()
    {
      //dash时修改移动系数
      stateMachine.ReusableData.MovementSpeedModifier = dashData.SpeedModifier;

      base.Enter();

      StartAnimation(stateMachine.player.animationData.DashParameterHash);

      stateMachine.ReusableData.CurrentJumpForce = airborneData.jumpData.StrongForce;

      stateMachine.ReusableData.RotationData = dashData.RotationData;

      //处理从静止状态转换过来的情况,添加一个力
      Dash();

      //如果按下任何移动键,该值为true
      shouldKeepRotating = stateMachine.ReusableData.MovementInput != Vector2.zero;

      UpdateConsecutiveDashes();

      startTime = Time.time;
    }


    public override void Exit()
    {
      base.Exit();

      StopAnimation(stateMachine.player.animationData.DashParameterHash);

      SetBaseRotationData();
    }

    public override void PhysicsUpdate()
    {
      base.PhysicsUpdate();

      if (!shouldKeepRotating)
        return;

      RotateTowardsTargetRotation();
    }
    public override void OnAnimationTransitionEvent()
    {
      //未输入时,dash转换为idle
      if (stateMachine.ReusableData.MovementInput == Vector2.zero)
      {
        stateMachine.ChangeState(stateMachine.hardStoppingState);
        return;
      }

      //否则转换为sprint
      stateMachine.ChangeState(stateMachine.sprintingState);
    }
    #endregion

    #region main methods
    private void Dash()
    {

      //获取玩家面朝向
      Vector3 dashDirection = stateMachine.player.transform.forward;

      dashDirection.y = 0f;

      //更新旋转,不考虑摄像机
      UpdateTargetRotation(dashDirection, false);

      //如果有方向输入,就跳过
      if (stateMachine.ReusableData.MovementInput != Vector2.zero)
      {
        UpdateTargetRotation(GetMovementInputDirection());

        dashDirection = GetTargetRotationDirection(stateMachine.ReusableData.CurrentTargetRotation.y);
      }

      //当玩家未输入时给一个力让其冲刺
      stateMachine.player.rb.velocity = dashDirection * GetMovementSpeed(false);
    }

    //更新连续冲刺次数
    private void UpdateConsecutiveDashes()
    {
      //不是连续冲刺
      if (!IsConsecutive())
      {
        //第一次冲初始化已经冲刺的次数
        consecutiveDashesUsed = 0;
      }

      // 冲一次加一
      ++consecutiveDashesUsed;

      //如果达到最大冲刺数
      if (consecutiveDashesUsed == dashData.ConsecutiveDashesLimitAmount)
      {
        //恢复冲刺数
        consecutiveDashesUsed = 0;

        //开启协程将传入的输入action禁用n秒
        stateMachine.player.Input.DisableActionFor(
          stateMachine.player.Input.playerActions.Dash,
          dashData.DashLimitReachedCoolDown
          );
      }
    }

    //检测是否在连续dash时间范围内
    private bool IsConsecutive()
    {
      return Time.time < startTime + dashData.TimeToBeConsideredConsecutive;
    }
    #endregion

    #region input methods
    protected override void OnDashStarted(InputAction.CallbackContext context)
    {
    }
    protected override void OnMovementPerformed(InputAction.CallbackContext context)
    {
      shouldKeepRotating = true;
    }
    #endregion

    #region reusable methods
    protected override void AddInputActionsCallbacks()
    {
      base.AddInputActionsCallbacks();

      stateMachine.player.Input.playerActions.Movement.performed += OnMovementPerformed;
    }

    protected override void RemoveInputActionsCallbacks()
    {
      base.RemoveInputActionsCallbacks();

      stateMachine.player.Input.playerActions.Movement.performed -= OnMovementPerformed;
    }

    #endregion
  }
}
