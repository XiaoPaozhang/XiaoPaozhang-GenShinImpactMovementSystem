using System;
using UnityEngine;

namespace XFramework.FSM
{
  [Serializable]
  public class DefaultColliderData
  {
    [field: SerializeField] public float Height { get; private set; } = 1.8f;
    [field: SerializeField] public float centerY { get; private set; } = 0.9f;
    [field: SerializeField] public float Radius { get; private set; } = 0.2f;
  }
}
