
using UnityEditor;

namespace XFramework.FSM
{
  public class PlayerMovementStateMachine : StateMachineBase
  {
    public Player player { get; }
    public PlayerStateReusableData ReusableData { get; }
    public PlayerIdlingState idlingState { get; }
    public PlayerWalkingState walkingState { get; }
    public PlayerRunningState runningState { get; }
    public PlayerSprintingState sprintingState { get; }

    public PlayerMovementStateMachine(Player player)
    {
      this.player = player;
      ReusableData = new PlayerStateReusableData();

      idlingState = new PlayerIdlingState(this);
      walkingState = new PlayerWalkingState(this);
      runningState = new PlayerRunningState(this);
      sprintingState = new PlayerSprintingState(this);

    }
  }
}