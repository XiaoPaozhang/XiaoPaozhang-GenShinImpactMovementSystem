using UnityEngine.InputSystem;

namespace GenshinImpactMovementSystem
{
  public class PlayerStoppingState : PlayerGroundedState
  {
    public PlayerStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }
    #region IState methods
    public override void Enter()
    {
      stateMachine.ReusableData.MovementSpeedModifier = 0f;

      SetBaseRotationData();

      base.Enter();

      StartAnimation(stateMachine.player.animationData.StoppingParameterHash);

    }
    public override void Exit()
    {
      base.Exit();

      StopAnimation(stateMachine.player.animationData.StoppingParameterHash);

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
      stateMachine.ChangeState(stateMachine.idlingState);
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
      base.RemoveInputActionsCallbacks();

      stateMachine.player.Input.playerActions.Movement.started -= OnMovementStarted;
    }


    #endregion

    #region input methods

    private void OnMovementStarted(InputAction.CallbackContext context)
    {
      OnMove();
    }
    #endregion
  }
}
