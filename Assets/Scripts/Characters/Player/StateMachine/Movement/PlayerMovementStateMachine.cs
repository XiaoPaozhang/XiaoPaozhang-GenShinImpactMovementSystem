
using System.Net.Http.Headers;
using UnityEditor;

namespace XFramework.FSM
{
  public class PlayerMovementStateMachine : StateMachineBase
  {
    public Player player { get; }
    public PlayerStateReusableData ReusableData { get; }

    public PlayerIdlingState idlingState { get; }
    public PlayerDashingState dashingState { get; }

    public PlayerWalkingState walkingState { get; }
    public PlayerRunningState runningState { get; }
    public PlayerSprintingState sprintingState { get; }

    public PlayerStoppingState stoppingState { get; }
    public PlayerLightStoppingState lightStoppingState { get; }
    public PlayerMediumStoppingState mediumStoppingState { get; }
    public PlayerHardStoppingState hardStoppingState { get; }

    public PlayerMovementStateMachine(Player player)
    {
      this.player = player;
      ReusableData = new PlayerStateReusableData();

      idlingState = new PlayerIdlingState(this);
      dashingState = new PlayerDashingState(this);

      walkingState = new PlayerWalkingState(this);
      runningState = new PlayerRunningState(this);
      sprintingState = new PlayerSprintingState(this);

      stoppingState = new PlayerStoppingState(this);
      lightStoppingState = new PlayerLightStoppingState(this);
      mediumStoppingState = new PlayerMediumStoppingState(this);
      hardStoppingState = new PlayerHardStoppingState(this);

    }
  }
}
