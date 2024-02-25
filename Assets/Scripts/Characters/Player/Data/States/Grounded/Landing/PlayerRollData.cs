using System;
using UnityEngine;

namespace XFramework.FSM
{
  [Serializable]
  public class PlayerRollData
  {
    [field: SerializeField][field: Range(0f, 3f)] public float SpeedModifier { get; private set; }
  }
}
