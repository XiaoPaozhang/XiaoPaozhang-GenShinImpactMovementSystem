using System;
using UnityEngine;

namespace GenshinImpactMovementSystem
{
  [Serializable]
  public class PlayerSprintData
  {
    [field: SerializeField][field: Range(1f, 3f)] public float SpeedModifier { get; private set; } = 1.7f;

    //不输入方向的情况下,可以在疾跑状态逗留多久,
    [field: SerializeField][field: Range(0f, 5f)] public float SprintToRunTime { get; private set; } = 1f;

    //不输入方向的情况下,可以在跑步状态逗留的时间
    [field: SerializeField][field: Range(0f, 5f)] public float RunToWalkTime { get; private set; } = 0.5f;
  }
}
