using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XFramework.FSM
{
  public class PlayerHardStoppingState : PlayerStoppingState
  {
    public PlayerHardStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }
  }
}
