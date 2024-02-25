
using UnityEngine;

namespace XFramework.FSM
{

  public abstract class StateMachineBase
  {
    protected IState _currentState;
    public void ChangeState(IState newState)
    {
      _currentState?.Exit();

      _currentState = newState;
      _currentState?.Enter();
    }
    public void HandleInput()
    {
      _currentState?.HandleInput();
    }

    public void Update()
    {
      _currentState?.Update();
    }
    public void PhysicsUpdate()
    {
      _currentState?.PhysicsUpdate();
    }

    public void OnAnimationEnterEvent()
    {

      _currentState?.OnAnimationEnterEvent();
    }
    public void OnAnimationExitEvent()
    {
      _currentState?.OnAnimationExitEvent();
    }
    public void OnAnimationTransitionEvent()
    {
      _currentState?.OnAnimationTransitionEvent();
    }
    public void OnTriggerEnter(Collider Other)
    {
      _currentState.OnTriggerEnter(Other);
    }

    public void OnTriggerExit(Collider other)
    {
      _currentState.OnTriggerExit(other);
    }
  }
}
