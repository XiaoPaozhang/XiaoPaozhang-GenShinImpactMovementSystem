using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshinImpactMovementSystem
{
  public class PlayerHardLandingState : PlayerLandingState
  {
    public PlayerHardLandingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
      stateMachine.ReusableData.MovementSpeedModifier = 0f;

      base.Enter();

      StartAnimation(stateMachine.player.animationData.HardLandParameterHash);

      stateMachine.player.Input.playerActions.Movement.Disable();

      ResetVelocity();
    }
    public override void Exit()
    {
      base.Exit();

      StopAnimation(stateMachine.player.animationData.HardLandParameterHash);

      stateMachine.player.Input.playerActions.Movement.Enable();
    }

    public override void PhysicsUpdate()
    {
      base.PhysicsUpdate();

      if (!IsMovingHorizontally())
        return;

      ResetVelocity();
    }

    public override void OnAnimationExitEvent()
    {
      stateMachine.player.Input.playerActions.Movement.Enable();
    }

    public override void OnAnimationTransitionEvent()
    {
      stateMachine.ChangeState(stateMachine.idlingState);
    }
    protected override void AddInputActionsCallbacks()
    {
      base.AddInputActionsCallbacks();

      stateMachine.player.Input.playerActions.Movement.started += OnMovementStarted;
    }

    protected override void RemoveInputActionsCallbacks()
    {
      base.RemoveInputActionsCallbacks();

      stateMachine.player.Input.playerActions.Movement.started += OnMovementStarted;
    }

    protected override void OnMove()
    {
      if (stateMachine.ReusableData.ShouldWalk)
        return;

      stateMachine.ChangeState(stateMachine.runningState);

    }

    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {
    }

    private void OnMovementStarted(InputAction.CallbackContext context)
    {
      OnMove();
    }
  }
}
