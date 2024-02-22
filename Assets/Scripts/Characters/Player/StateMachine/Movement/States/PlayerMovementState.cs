using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace XFramework.FSM
{
  //玩家移动状态
  public class PlayerMovementState : IState
  {
    //玩家移动状态机
    public PlayerMovementStateMachine stateMachine;

    protected PlayerGroundedData movementData;

    protected float baseSpeed = 5f;//速度
    public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
    {
      stateMachine = playerMovementStateMachine;

      movementData = stateMachine.player.data.groundedData;

      InitializeData();
    }

    private void InitializeData()
    {
      SetBaseRotationData();
    }

    #region IState 方法
    public virtual void Enter()
    {
      Debug.Log("State:" + GetType().Name);

      AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
      RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
      ReadMovementInput();
    }


    public virtual void Update()
    {

    }
    public virtual void PhysicsUpdate()
    {
      Move();
    }
    public virtual void OnAnimationEnterEvent()
    {

    }

    public virtual void OnAnimationExitEvent()
    {

    }

    public virtual void OnAnimationTransitionEvent()
    {

    }
    #endregion

    #region 主要方法
    //读取移动输入
    private void ReadMovementInput()
    {
      stateMachine.ReusableData.MovementInput = stateMachine.player.Input.playerActions.Movement.ReadValue<Vector2>();
    }
    private void Move()
    {
      if (stateMachine.ReusableData.MovementInput == Vector2.zero || stateMachine.ReusableData.MovementSpeedModifier == 0f)
        return;

      Vector3 movementDirection = GetMovementInputDirection();
      float targetRotationYAngle = Rotate(movementDirection);

      Vector3 targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);
      float movementSpeed = GetMovementSpeed();
      //获取玩家当前水平速度,用于每次叠加力时消减它,因为是采用的刚体addForce的方式
      Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();
      stateMachine.player.rb.AddForce(targetRotationDirection * movementSpeed - currentPlayerHorizontalVelocity, ForceMode.VelocityChange);
    }

    // 获取玩家将要旋转的方向
    private float Rotate(Vector3 direction)
    {
      float angle = UpdateTargetRotation(direction);

      RotateTowardsTargetRotation();

      return angle;
    }

    private float AddCameraRotationToAngle(float angle)
    {
      angle += stateMachine.player.MainCameraTransform.eulerAngles.y;

      if (angle > 360f)
      {
        angle -= 360f;
      }

      return angle;
    }

    private static float Getangle(Vector3 direction)
    {
      //输入方向相对于x轴的角度,在这里传入的参数导致x轴正方向为0度
      float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

      //禁止负数角度
      if (angle <= 0f)
      {
        angle += 360f;
      }

      return angle;
    }



    #endregion

    #region  Reusable Methods 可重用的方法
    protected void SetBaseRotationData()
    {
      stateMachine.ReusableData.RotationData = movementData.BaseRotationData;
      stateMachine.ReusableData.TimeToReachTargetRotation = stateMachine.ReusableData.RotationData.TargetRotationReachTime;
    }

    //获取移动输入方向
    protected Vector3 GetMovementInputDirection()
    {
      return new Vector3(stateMachine.ReusableData.MovementInput.x, 0, stateMachine.ReusableData.MovementInput.y);
    }


    //获取移动速度
    protected float GetMovementSpeed()
    {
      //移动速度 * 移动速度系数 * 在斜坡上时的速度系数
      return movementData.BaseSpeed
      * stateMachine.ReusableData.MovementSpeedModifier
      * stateMachine.ReusableData.MovementOnSlopesSpeedModifier;
    }
    //获取玩家水平速度
    protected Vector3 GetPlayerHorizontalVelocity()
    {
      Vector3 playerHorizontalVelocity = stateMachine.player.rb.velocity;

      playerHorizontalVelocity.y = 0f;

      return playerHorizontalVelocity;
    }
    protected Vector3 GetPlayerVerticalVelocity()
    {
      return new Vector3(0f, stateMachine.player.rb.velocity.y, 0f);
    }

    // 将玩家的旋转平滑地转向一个目标旋转角度
    protected void RotateTowardsTargetRotation()
    {
      // 获取当前玩家绕y轴旋转的角度
      float currentYAngle = stateMachine.player.rb.rotation.eulerAngles.y;


      // 如果旋转到目标角度了就不再旋转
      // stateMachine.ReusableData.CurrentTargetRotation.y 目标的y轴旋转角度
      if (currentYAngle == stateMachine.ReusableData.CurrentTargetRotation.y)
        return;

      // 使用Mathf.SmoothDampAngle平滑地计算出新的y轴旋转角度
      // smoothedYAngle 平滑处理后的y轴旋转角度
      // 当前角度, 目标角度, ref 当前角度y, 平滑所需时间
      float smoothedYAngle = Mathf.SmoothDampAngle(
        currentYAngle,
        stateMachine.ReusableData.CurrentTargetRotation.y,
        ref stateMachine.ReusableData.DampedTargetRotationCurrentVelocity.y,
        stateMachine.ReusableData.TimeToReachTargetRotation.y - stateMachine.ReusableData.DampedTargetRotationPassedTime.y
       );

      // 增加已经过去的时间
      stateMachine.ReusableData.DampedTargetRotationPassedTime.y += Time.deltaTime;

      // 创建一个新的旋转，其中y轴的旋转角度为smoothedYAngle
      // 仅旋转y轴
      Quaternion targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);

      // 将玩家的旋转设置为targetRotation
      stateMachine.player.rb.MoveRotation(targetRotation);
    }

    protected float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotation = true)
    {
      //获取度数
      float angle = Getangle(direction);

      //是否考虑摄像机
      if (shouldConsiderCameraRotation)
      {
        angle = AddCameraRotationToAngle(angle);
      }

      if (angle != stateMachine.ReusableData.CurrentTargetRotation.y)
      {
        UpdateTargetRotationData(angle);
      }

      return angle;
    }

    private void UpdateTargetRotationData(float targetAngle)
    {
      stateMachine.ReusableData.CurrentTargetRotation.y = targetAngle;

      stateMachine.ReusableData.DampedTargetRotationPassedTime.y = 0f;
    }

    protected Vector3 GetTargetRotationDirection(float targetAngle)
    {
      return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
    }

    //消除玩家刚体的速度
    protected void ResetVelocity()
    {
      stateMachine.player.rb.velocity = Vector3.zero;
    }

    protected virtual void AddInputActionsCallbacks()
    {
      stateMachine.player.Input.playerActions.WalkToggle.started += OnWalkToggleStarted;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
      stateMachine.player.Input.playerActions.WalkToggle.started -= OnWalkToggleStarted;
    }

    //减速
    protected void DecelerateHorizontally()
    {
      Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();

      stateMachine.player.rb.AddForce(
        -playerHorizontalVelocity
        * stateMachine.ReusableData.MovementOnDecelerationForce
        ,
        ForceMode.Acceleration);
    }

    //用于修复因为刚体移动导致的小量位移
    protected bool IsMovingHorizontally(float minimumMagnitude = 0.1f)
    {
      //获取玩家水平移速
      Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();

      //转为二维向量
      Vector2 playerHorizontalMovement = new Vector2(
        playerHorizontalVelocity.x,
        playerHorizontalVelocity.z
      );

      //判断移动向量模长是否大于最小模长,大于不需要修复
      return playerHorizontalMovement.magnitude > minimumMagnitude;
    }
    #endregion

    #region input methods

    protected virtual void OnWalkToggleStarted(InputAction.CallbackContext context)
    {
      stateMachine.ReusableData.ShouldWalk = !stateMachine.ReusableData.ShouldWalk;
    }

    #endregion
  }
}
