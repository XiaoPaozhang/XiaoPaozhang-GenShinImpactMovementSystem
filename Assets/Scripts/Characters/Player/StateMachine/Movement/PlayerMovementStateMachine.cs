

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

    public PlayerJumpingState jumpingState { get; }
    public PlayerFallingState fallingState { get; }


    public PlayerLightLandingState lightLandingState { get; }
    public PlayerRollingLandingState rollingLandingState { get; }
    public PlayerHardLandingState hardLandingState { get; }

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

      jumpingState = new PlayerJumpingState(this);
      fallingState = new PlayerFallingState(this);

      lightLandingState = new PlayerLightLandingState(this);
      hardLandingState = new PlayerHardLandingState(this);
      rollingLandingState = new PlayerRollingLandingState(this);

    }
  }
}
