using System.ComponentModel;
using System.Collections.Generic;
using UnityEngine;

namespace XFramework.FSM
{
  public class PlayerStateReusableData
  {
    public Vector2 MovementInput { get; set; }
    public float MovementSpeedModifier { get; set; } = 1f;
    public float MovementSlopesSpeedModifier { get; set; } = 1f;
    public float MovementDecelerationForce { get; set; } = 1f;

    public List<PlayerCameraRecenteringData> SidewaysCameraRecenteringData { get; set; }
    public List<PlayerCameraRecenteringData> BackWardsCameraRecenteringData { get; set; }

    public bool ShouldWalk { get; set; }
    public bool ShouldSprint { get; set; }

    private Vector3 currentTargetRotation;
    private Vector3 timeToReachTargetRotation;
    private Vector3 dampedTargetRotationCurrentVelocity;
    private Vector3 dampedTargetRotationPassedTime;

    public ref Vector3 CurrentTargetRotation => ref currentTargetRotation;
    public ref Vector3 TimeToReachTargetRotation => ref timeToReachTargetRotation;
    public ref Vector3 DampedTargetRotationCurrentVelocity => ref dampedTargetRotationCurrentVelocity;
    public ref Vector3 DampedTargetRotationPassedTime => ref dampedTargetRotationPassedTime;

    [field: Header("当前跳跃力度")]
    public Vector3 CurrentJumpForce { get; set; }

    public PlayerRotationData RotationData { get; set; }
  }
}
