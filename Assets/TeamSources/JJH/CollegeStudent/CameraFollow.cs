using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 카메라가 따라갈 대상 (캐릭터)
    public Vector3 offset; // 카메라와 대상 사이의 거리
    public float smoothSpeed = 0.125f; // 카메라 이동 속도 (부드럽게 이동)

    private void LateUpdate()
    {
        if (target != null)
        {
            // 타겟 위치에 오프셋을 추가한 위치로 카메라 이동
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}


