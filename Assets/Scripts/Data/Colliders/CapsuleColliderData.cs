using UnityEngine;

namespace XFramework.FSM
{
  public class CapsuleColliderData
  {
    public CapsuleCollider Collider { get; private set; }
    public Vector3 ColliderCenterInLocalSpace { get; private set; }

    // 初始化碰撞体引用(单例)
    public void Initialize(GameObject gameObject)
    {
      if (Collider != null)
        return;
      Collider = gameObject.GetComponent<CapsuleCollider>();
      UpdateColliderData();
    }
    // 更新碰撞体数据
    public void UpdateColliderData()
    {
      ColliderCenterInLocalSpace = Collider.center;
    }
  }
}