using System;
using UnityEngine;

namespace XFramework.FSM
{
  public class PlayerIdlingState : PlayerMovingState
  {
    public PlayerIdlingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {

    }
    public override void Enter()
    {
      base.Enter();

      stateMachine.ReusableData.MovementSpeedModifier = movementData.RunData.SpeedModifier;

      //进入待机时,将速度设置为0
      //此方法在父类 PlayerMovementState 中定义
      ResetVelocity();
    }

    public override void Update()
    {
      base.Update();

      if (stateMachine.ReusableData.MovementInput == Vector2.zero)
        return;

      //此方法在父类 PlayerGroundedState 中定义
      OnMove();
    }
  }
}
