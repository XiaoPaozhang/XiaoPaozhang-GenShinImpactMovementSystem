using UnityEngine;

namespace XFramework.FSM
{
  public class PlayerStateReusableData
  {
    public Vector2 MovementInput { get; set; }
    public float MovementSpeedModifier { get; set; } = 1f;
    public float MovementOnSlopesSpeedModifier { get; set; } = 1f;
    public float MovementOnDecelerationForce { get; set; } = 1f;
    public bool ShouldWalk { get; set; }

    private Vector3 currentTargetRotation;
    private Vector3 timeToReachTargetRotation;
    private Vector3 dampedTargetRotationCurrentVelocity;
    private Vector3 dampedTargetRotationPassedTime;

    public ref Vector3 CurrentTargetRotation => ref currentTargetRotation;
    public ref Vector3 TimeToReachTargetRotation => ref timeToReachTargetRotation;
    public ref Vector3 DampedTargetRotationCurrentVelocity => ref dampedTargetRotationCurrentVelocity;
    public ref Vector3 DampedTargetRotationPassedTime => ref dampedTargetRotationPassedTime;

    public PlayerRotationData RotationData { get; set; }
  }
}
