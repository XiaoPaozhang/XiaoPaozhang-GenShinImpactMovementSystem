using System;
using UnityEngine;

namespace XFramework.FSM
{
  [Serializable]
  public class PlayerRotationData
  {
    [field: SerializeField] public Vector3 TargetRotationRechTime { get; private set; }
  }
}
