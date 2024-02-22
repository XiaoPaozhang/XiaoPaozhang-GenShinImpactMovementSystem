using UnityEngine.InputSystem;

namespace XFramework.FSM
{
  public class PlayerStoppingState : PlayerGroundedState
  {
    public PlayerStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }
    #region IState methods
    public override void Enter()
    {
      base.Enter();

      stateMachine.ReusableData.MovementSpeedModifier = 0f;
    }

    public override void PhysicsUpdate()
    {
      base.PhysicsUpdate();

      //旋转
      RotateTowardsTargetRotation();

      if (!IsMovingHorizontally())
        return;

      DecelerateHorizontally();
    }

    public override void OnAnimationTransitionEvent()
    {
      stateMachine.ChangeState(stateMachine.idlingState)
;
    }
    #endregion
    #region Reusable Methods
    protected override void AddInputActionsCallbacks()
    {
      base.AddInputActionsCallbacks();

      stateMachine.player.Input.playerActions.Movement.started += OnMovementStarted;
    }
    protected override void RemoveInputActionsCallbacks()
    {
      base.AddInputActionsCallbacks();

      stateMachine.player.Input.playerActions.Movement.started -= OnMovementStarted;
    }

    private void OnMovementStarted(InputAction.CallbackContext context)
    {
      OnMove();
    }

    #endregion

    #region input methods
    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
    }
    #endregion
  }
}
