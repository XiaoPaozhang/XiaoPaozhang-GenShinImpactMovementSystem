using Cinemachine;
using UnityEngine;

namespace GenshinImpactMovementSystem
{
  public class CameraZoom : MonoBehaviour
  {
    // 默认的摄像机距离
    [SerializeField][Range(0, 10f)] private float defaultDistance = 6f;
    // 摄像机的最小距离
    [SerializeField][Range(0, 10f)] private float minimumDistance = 1f;
    // 摄像机的最大距离
    [SerializeField][Range(0, 10f)] private float maximumDistance = 6f;
    // 平滑度，用于控制摄像机缩放的速度
    [SerializeField][Range(0, 10f)] private float smoothing = 4f;
    // 缩放灵敏度，用于控制摄像机缩放的灵敏度
    [SerializeField][Range(0, 10f)] private float zoomSensitivity = 6f;

    private CinemachineFramingTransposer framingTransposer; // Cinemachine的成帧转调器组件
    private CinemachineInputProvider inputProvider; // Cinemachine的输入插件

    private float currentTargetDistance; // 当前目标距离

    private void Awake()
    {
      // 获取CinemachineFramingTransposer和CinemachineInputProvider组件
      framingTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
      inputProvider = GetComponent<CinemachineInputProvider>();

      // 设置当前目标距离为默认距离
      currentTargetDistance = defaultDistance;
    }

    private void Update()
    {
      // 在每一帧更新时，进行摄像机的缩放
      Zoom();
    }

    private void Zoom()
    {
      // 获取摄像机的z轴值（也就是缩放值）
      float zoomValue = inputProvider.GetAxisValue(2) * zoomSensitivity;

      // 更新当前目标距离
      currentTargetDistance = Mathf.Clamp(currentTargetDistance + zoomValue, minimumDistance, maximumDistance);

      // 获取当前摄像机的距离
      float currentDistance = framingTransposer.m_CameraDistance;

      // 如果当前距离和目标距离相同，那么就不进行缩放
      if (currentDistance == currentTargetDistance)
        return;

      // 使用Mathf.Lerp方法来平滑地改变摄像机的距离
      float lerpedZoomValue = Mathf.Lerp(currentDistance, currentTargetDistance, smoothing * Time.deltaTime);

      // 设置摄像机的距离
      framingTransposer.m_CameraDistance = lerpedZoomValue;
    }
  }
}
