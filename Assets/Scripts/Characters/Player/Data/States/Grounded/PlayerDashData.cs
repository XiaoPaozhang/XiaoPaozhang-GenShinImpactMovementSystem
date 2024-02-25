using System;
using UnityEngine;

namespace GenshinImpactMovementSystem
{
  [Serializable]
  public class PlayerDashData
  {
    // 速度修正因子，在dash状态时会乘以这个值
    [field: SerializeField]
    [field: Range(1f, 3f)]
    public float SpeedModifier { get; private set; } = 2f;

    // 旋转数据
    [field: SerializeField]
    public PlayerRotationData RotationData { get; private set; }

    // 连续dash的判定时间，如果两次dash的间隔小于这个值，就会被认为是连续的
    [field: SerializeField]
    [field: Range(0f, 2f)]
    public float TimeToBeConsideredConsecutive { get; private set; } = 1f;

    // 连续dash的最大次数，超过这个值后就不能再dash
    [field: SerializeField]
    [field: Range(1, 10)]
    public int ConsecutiveDashesLimitAmount { get; private set; } = 2;

    // 达到连续dash次数限制后的冷却时间，这段时间内不能dash
    [field: SerializeField]
    [field: Range(0f, 5f)]
    public float DashLimitReachedCoolDown { get; private set; } = 1.75f;
  }
}
