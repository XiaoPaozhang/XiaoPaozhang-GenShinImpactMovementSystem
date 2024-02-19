using System;
using UnityEngine;

namespace XFramework.FSM
{
  [Serializable]
  public class PlayerLayerData
  {
    [field: SerializeField] public LayerMask GroundLayer { get; private set; }
  }
}
