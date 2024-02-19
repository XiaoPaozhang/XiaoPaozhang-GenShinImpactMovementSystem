using System;
using UnityEngine;


namespace XFramework.FSM
{
  /// <summary>
  /// 胶囊碰撞体类
  /// 在player类中初始化
  /// 
  /// 用来存储:
  /// 1. 胶囊碰撞体引用
  /// 2. 斜率数据
  /// </summary>
  [Serializable]
  public class CapsuleColliderUtility
  {
    public CapsuleColliderData CapsuleColliderData { get; private set; }
    [field: SerializeField] public DefaultColliderData DefaultColliderData { get; private set; }
    [field: SerializeField] public SlopeData SlopeData { get; private set; }

    // 初始化碰撞体数据
    public void Initialize(GameObject gameObject)
    {
      if (CapsuleColliderData != null)
        return;
      CapsuleColliderData = new CapsuleColliderData();

      CapsuleColliderData.Initialize(gameObject);
    }

    // 计算并设置胶囊碰撞器各个属性
    public void CalculateCapsuleColliderDimensions()
    {
      //设置 碰撞体半径
      SetCapsuleColliderRadius(DefaultColliderData.Radius);

      //设置 碰撞体高度
      SetCapsuleColliderHeight(DefaultColliderData.Height * (1f - SlopeData.StepHeightPercentage));

      //重新计算并设置 碰撞体中心点
      RecalculateCapsuleColliderCenter();

      //计算碰撞体高度的一半
      float halfColliderHeight = CapsuleColliderData.Collider.height / 2f;
      //如果它小于半径就把高度值设置为半径
      //胶囊体最多缩短成一个球,高度小于半径的时候,再缩就出现奇怪的问题
      //这里希望它缩成球的时候,慢慢变小,也就是缩小半径
      //但是centerY还是一直在减小,所以它能保持顶端位置不变
      if (halfColliderHeight < CapsuleColliderData.Collider.radius)
      {
        SetCapsuleColliderRadius(halfColliderHeight);
      }
      //最后,更新碰撞体数据
      CapsuleColliderData.UpdateColliderData();
    }

    private void SetCapsuleColliderRadius(float radius)
    {
      CapsuleColliderData.Collider.radius = radius;
    }

    private void SetCapsuleColliderHeight(float height)
    {
      CapsuleColliderData.Collider.height = height;
    }

    private void RecalculateCapsuleColliderCenter()
    {
      float colliderHeightDifference = DefaultColliderData.Height - CapsuleColliderData.Collider.height;

      Vector3 newColliderCenter = new Vector3(0f, DefaultColliderData.centerY + (colliderHeightDifference / 2f), 0f);
      CapsuleColliderData.Collider.center = newColliderCenter;
    }
  }
}
