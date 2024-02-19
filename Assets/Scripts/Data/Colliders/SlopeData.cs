using System;
using UnityEngine;

namespace XFramework.FSM
{
  [Serializable]
  public class SlopeData
  {
    // 表示步行高度的百分比，范围在0到1之间，默认值为0.25。
    // 用于确定角色能够步行的斜坡的最大高度。
    [field: SerializeField][field: Range(0f, 1f)] public float StepHeightPercentage { get; private set; } = 0.25f;

    // 表示浮动射线的距离，范围在0到5之间，默认值为2。不需要多高,只要略高于碰撞体高度就行
    // 用于检测角色下方的地面距离，以帮助处理角色的跳跃和落地。
    [field: SerializeField][field: Range(0f, 5f)] public float FloatRayDistance { get; private set; } = 2f;

    // 表示步行到达力量，范围在0到50之间，默认值为25。
    // 用于当角色试图步行上斜坡时，给角色添加额外的力量。
    [field: SerializeField][field: Range(0f, 50f)] public float StepReachForce { get; private set; } = 25f;
  }
}
