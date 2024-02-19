using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XFramework.FSM
{
  public class PlayerSprintingState : PlayerMovingState
  {
    public PlayerSprintingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

  }
}
