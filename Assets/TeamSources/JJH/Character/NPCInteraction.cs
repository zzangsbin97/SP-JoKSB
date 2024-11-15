using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCInteraction : MonoBehaviour
{
    public string battleSceneName = "BattleScene"; // 전환할 배틀 씬 이름 설정
    private bool isPlayerInRange = false; // 플레이어가 NPC와 상호작용 범위 내에 있는지 여부

    // 매 프레임마다 호출되는 Update 함수
    private void Update()
    {
        // 플레이어가 범위 내에 있고 'E' 키를 눌렀을 때 씬 전환
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(battleSceneName);
        }
    }

    // 플레이어가 NPC와 충돌할 때 호출되는 함수
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            // 상호작용 메시지 UI를 활성화할 수 있음 (예: "Press E to start battle")
        }
    }

    // 플레이어가 범위를 벗어났을 때 호출되는 함수
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            // 상호작용 메시지 UI를 비활성화할 수 있음
        }
    }
}
