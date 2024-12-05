using UnityEngine;

public class Portal : MonoBehaviour
{
    public Vector3 targetPosition = new Vector3(10.77f, 0.35f, 0.0007217824f);  // 목표 위치
    public string targetSceneName;    // 씬 전환을 사용할 경우 씬 이름

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // "Player" 태그가 있는 객체만 반응하도록 함
        if (collision.CompareTag("Player"))
        {
            // 목표 위치로 이동
            collision.transform.position = targetPosition;
            Debug.Log("포탈을 통해 이동했습니다: " + targetPosition);
        }
    }
}
