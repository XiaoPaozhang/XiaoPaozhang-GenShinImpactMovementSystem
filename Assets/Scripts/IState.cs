using System.Collections;
using System.Collections.Generic;
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
  }
}