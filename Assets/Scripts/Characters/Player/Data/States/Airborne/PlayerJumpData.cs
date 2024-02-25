using System;
using UnityEngine;

namespace XFramework.FSM
{
  [Serializable]
  public class PlayerJumpData
  {

    [field: SerializeField] public PlayerRotationData rotationData { get; private set; }

    [field: SerializeField][field: Range(0f, 5f)] public float JumpToGroundRayDistance { get; private set; } = 2f;
    [field: SerializeField] public AnimationCurve JumpForceModifierOnSlopeUpwards { get; private set; }
    [field: SerializeField]
    public AnimationCurve JumpForceModifierOnSlopeDownwards { get; private set; }

    [field: Header("静止力度")]
    [field: Tooltip("站立状态跳起")]
    [field: SerializeField] public Vector3 StationaryForce { get; private set; }

    [field: Header("弱力度")]
    [field: Tooltip("行走,轻停止状态跳起")]
    [field: SerializeField] public Vector3 WeakForce { get; private set; }

    [field: Header("中力度")]
    [field: Tooltip("跑步,中停止状态跳起")]
    [field: SerializeField] public Vector3 MediumForce { get; private set; }

    [field: Header("重力度")]
    [field: Tooltip("疾跑,冲刺,重停止状态跳起")]
    [field: SerializeField] public Vector3 StrongForce { get; private set; }

    [field: SerializeField][field: Range(0f, 10f)] public float DecelerationForce { get; private set; } = 1.5f;

  }
}
