using System;
using UnityEngine;
using XFramework.FSM;

[CreateAssetMenu(fileName = "Plyer", menuName = "自定义/角色/玩家")]
public class PlayerSO : ScriptableObject
{
  public PlayerGroundedData groundedData;
}
