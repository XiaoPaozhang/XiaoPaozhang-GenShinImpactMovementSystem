
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshinImpactMovementSystem
{
  public class PlayerRollingLandingState : PlayerLandingState
  {
    private PlayerRollData rollData;
    public PlayerRollingLandingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
      rollData = movementData.RollData;
    }

    public override void Enter()
    {
      stateMachine.ReusableData.MovementSpeedModifier = rollData.SpeedModifier;

      base.Enter();

      StartAnimation(stateMachine.player.animationData.RollParameterHash);

      stateMachine.ReusableData.ShouldSprint = false;
    }
    public override void Exit()
    {
      base.Exit();

      StopAnimation(stateMachine.player.animationData.RollParameterHash);

    }

    public override void PhysicsUpdate()
    {
      base.PhysicsUpdate();

      if (stateMachine.ReusableData.MovementInput != Vector2.zero)
        return;

      RotateTowardsTargetRotation();
    }

    public override void OnAnimationTransitionEvent()
    {
      base.OnAnimationTransitionEvent();

      if (stateMachine.ReusableData.MovementInput == Vector2.zero)
      {
        stateMachine.ChangeState(stateMachine.mediumStoppingState);

        return;
      }
      OnMove();
    }

    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {

    }
  }
}
