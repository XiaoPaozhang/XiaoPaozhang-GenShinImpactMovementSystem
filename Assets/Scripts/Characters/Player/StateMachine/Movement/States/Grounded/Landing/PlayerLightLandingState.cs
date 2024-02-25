
using UnityEngine;

namespace GenshinImpactMovementSystem
{
  public class PlayerLightLandingState : PlayerLandingState
  {
    public PlayerLightLandingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
      stateMachine.ReusableData.MovementSpeedModifier = 0f;

      base.Enter();

      stateMachine.ReusableData.CurrentJumpForce = airborneData.jumpData.StationaryForce;

      ResetVelocity();
    }

    public override void Update()
    {
      base.Update();

      if (stateMachine.ReusableData.MovementInput == Vector2.zero)
        return;

      OnMove();
    }

    public override void PhysicsUpdate()
    {
      base.PhysicsUpdate();

      if (!IsMovingHorizontally())
        return;

      ResetVelocity();
    }

    public override void OnAnimationTransitionEvent()
    {
      base.OnAnimationTransitionEvent();

      stateMachine.ChangeState(stateMachine.idlingState);
    }
  }
}
