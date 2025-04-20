using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform target;                  // 要跟随的目标对象
    public float smoothSpeed = 0.125f;       // 跟随平滑度（值越小越平滑）
    public Vector3 offset = new Vector3(0, 2, -5); // 摄像机相对目标的偏移量
    public float lookUpOffset = 1f;          // 视线向上偏移量

    private Vector3 velocity = Vector3.zero; // 用于SmoothDamp的速率变量

    void Start()
    {
        // 如果未手动设置偏移量，则自动计算初始偏移
        if (offset == Vector3.zero)
        {
            offset = transform.position - target.position;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 计算目标位置（目标位置 + 偏移量）
        Vector3 targetPosition = target.position + offset;

        // 使用SmoothDamp平滑移动摄像机
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothSpeed
        );

        // 让摄像机看向目标（带垂直偏移）
        Vector3 lookAtPoint = target.position + Vector3.up * lookUpOffset;
        transform.LookAt(lookAtPoint);
    }
}