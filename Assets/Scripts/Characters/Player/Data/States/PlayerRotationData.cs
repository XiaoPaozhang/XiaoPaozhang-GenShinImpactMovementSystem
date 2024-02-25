using System;
using UnityEngine;

namespace GenshinImpactMovementSystem
{
  [Serializable]
  public class PlayerRotationData
  {
    [field: Header("到达目标旋转的时间,只填y轴")]
    [field: SerializeField] public Vector3 TargetRotationReachTime { get; private set; }
  }
}
