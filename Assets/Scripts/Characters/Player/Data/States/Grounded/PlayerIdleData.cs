using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XFramework.FSM
{
  [Serializable]
  public class PlayerIdleData
  {
    [field: SerializeField] public List<PlayerCameraRecenteringData> BackwardsCameraRecenteringData { get; private set; }

  }
}
