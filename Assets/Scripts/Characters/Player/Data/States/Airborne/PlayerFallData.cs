using System;
using UnityEngine;

namespace XFramework.FSM
{
  [Serializable]
  public class PlayerFallData
  {
    [field: Tooltip("Having higher numbers might not read collisions with shallow colliders correctly.")]
    [field: SerializeField][field: Range(1f, 15f)] public float FallSpeedLimit { get; private set; } = 15f;
    [field: SerializeField][field: Range(0f, 100f)] public float MinimumDistanceToBeConsideredHardFall { get; private set; } = 3f;
  }
}