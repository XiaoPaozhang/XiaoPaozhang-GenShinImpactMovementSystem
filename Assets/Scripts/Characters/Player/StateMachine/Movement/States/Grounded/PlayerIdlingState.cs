using UnityEngine;

namespace XFramework.FSM
{
  public class PlayerIdlingState : PlayerGroundedState
  {
    private PlayerIdleData idleData;
    public PlayerIdlingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
      idleData = movementData.IdleData;
    }
    public override void Enter()
    {

      stateMachine.ReusableData.MovementSpeedModifier = 0f;

      stateMachine.ReusableData.BackWardsCameraRecenteringData = idleData.BackwardsCameraRecenteringData;

      base.Enter();

      StartAnimation(stateMachine.player.animationData.IdleParameterHash);

      stateMachine.ReusableData.CurrentJumpForce = airborneData.jumpData.StationaryForce;

      //进入待机时,将速度设置为0
      //此方法在父类 PlayerMovementState 中定义
      ResetVelocity();
    }
    public override void Exit()
    {
      base.Exit();

      StopAnimation(stateMachine.player.animationData.IdleParameterHash);

    }

    public override void Update()
    {
      base.Update();

      if (stateMachine.ReusableData.MovementInput == Vector2.zero)
        return;

      //此方法在父类 PlayerGroundedState 中定义
      OnMove();
    }
    public override void PhysicsUpdate()
    {
      base.PhysicsUpdate();

      if (!IsMovingHorizontally())
        return;

      ResetVelocity();
    }
  }
}
