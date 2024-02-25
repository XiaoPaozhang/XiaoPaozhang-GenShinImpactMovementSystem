using System;
using UnityEngine;
using GenshinImpactMovementSystem;

[CreateAssetMenu(fileName = "Plyer", menuName = "自定义/角色/玩家")]
public class PlayerSO : ScriptableObject
{
  [Header("地面参数")]
  public PlayerGroundedData groundedData;

  [Header("滞空参数")]
  public PlayerAirborneData airborneData;
}
