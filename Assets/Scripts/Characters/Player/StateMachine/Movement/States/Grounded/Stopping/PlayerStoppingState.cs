using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XFramework.FSM
{
  public class PlayerStoppingState : PlayerGroundedState
  {
    public PlayerStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }
  }
}
