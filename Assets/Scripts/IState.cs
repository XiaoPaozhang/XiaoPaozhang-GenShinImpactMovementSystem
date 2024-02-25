using UnityEngine;

namespace XFramework.FSM
{
  public interface IState
  {
    void Enter();
    void Exit();
    void HandleInput();
    void Update();
    void PhysicsUpdate();
    void OnAnimationEnterEvent();
    void OnAnimationExitEvent();
    void OnAnimationTransitionEvent();
    void OnTriggerEnter(Collider other);
    void OnTriggerExit(Collider other);
  }
}