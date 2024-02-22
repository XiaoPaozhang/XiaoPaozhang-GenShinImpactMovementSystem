using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XFramework.FSM
{
  public class PlayerLightStoppingState : PlayerStoppingState
  {
    public PlayerLightStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    #region IState methods
    public override void Enter()
    {
      base.Enter();

      stateMachine.ReusableData.MovementOnDecelerationForce = movementData.StopData.LightDecelerationForce;
    }

    #endregion
  }
}
