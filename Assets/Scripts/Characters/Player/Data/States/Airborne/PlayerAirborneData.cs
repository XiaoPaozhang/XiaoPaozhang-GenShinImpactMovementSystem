using System;
using UnityEngine;

namespace XFramework.FSM
{
  [Serializable]
  public class PlayerAirborneData
  {
    [field: Header("跳跃数据")]
    [field: SerializeField] public PlayerJumpData jumpData { get; protected set; }

    [field: Header("坠落数据")]
    [field: SerializeField] public PlayerFallData fallData { get; protected set; }
  }
}
