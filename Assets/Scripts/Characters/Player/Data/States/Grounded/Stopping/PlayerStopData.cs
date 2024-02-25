using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshinImpactMovementSystem
{
  [Serializable]
  public class PlayerStopData
  {
    [field: Header("轻停止减速力度")]
    [field: SerializeField][field: Range(1f, 2f)] public float LightDecelerationForce { get; private set; } = 5f;

    [field: Header("中停止减速力度")]
    [field: SerializeField][field: Range(1f, 2f)] public float MediumDecelerationForce { get; private set; } = 6.5f;

    [field: Header("重停止减速力度")]
    [field: SerializeField][field: Range(1f, 2f)] public float HardDecelerationForce { get; private set; } = 5f;
  }
}
