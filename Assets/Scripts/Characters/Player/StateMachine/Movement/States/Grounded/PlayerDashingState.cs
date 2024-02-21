using UnityEngine;
using UnityEngine.InputSystem;

namespace XFramework.FSM
{
  public class PlayerDashingState : PlayerGroundedState
  {
    private PlayerDashData dashData;

    private float startTime;//冲刺起始时间
    private int consecutiveDashesUsed;//连续冲刺已使用数

    public PlayerDashingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
      dashData = movementData.DashData;
    }
    #region IState methods
    public override void Enter()
    {
      base.Enter();

      //dash时修改移动系数
      stateMachine.ReusableData.MovementSpeedModifier = dashData.SpeedModifier;

      //处理从静止状态转换过来的情况,添加一个力
      AddForceOnTransitionFromStationaryState();

      UpdateConsecutiveDashes();

      startTime = Time.time;
    }

    public override void OnAnimationTransitionEvent()
    {
      base.OnAnimationTransitionEvent();

      //未输入时,dash转换为idle
      if (stateMachine.ReusableData.MovementInput == Vector2.zero)
      {
        stateMachine.ChangeState(stateMachine.idlingState);
        return;
      }

      //否则转换为sprint
      stateMachine.ChangeState(stateMachine.sprintingState);

    }
    #endregion

    #region main methods
    private void AddForceOnTransitionFromStationaryState()
    {
      //如果有方向输入,就跳过
      if (stateMachine.ReusableData.MovementInput != Vector2.zero)
        return;

      //获取玩家面朝向
      Vector3 characterRotationDirection = stateMachine.player.transform.forward;
      characterRotationDirection.y = 0f;

      //当玩家未输入时给一个力让其冲刺
      stateMachine.player.rb.velocity = characterRotationDirection * GetMovementSpeed();
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
    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {

    }
    protected override void OnDashStarted(InputAction.CallbackContext context)
    {
    }
    #endregion
  }
}
