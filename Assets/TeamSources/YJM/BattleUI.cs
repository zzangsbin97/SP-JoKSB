using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUI : MonoBehaviour
{
    public GameObject buttonPrefab;      // 버튼의 프리팹
    public Transform buttonContainer;    // 버튼들을 배치할 부모 객체
    public TextMeshProUGUI battleStatusText; // 배틀 상태 텍스트

    private BattleManager battleManager; // BattleManager에 접근

    void Start()
    {
        // BattleManager의 Singleton 인스턴스를 사용
        battleManager = BattleManager.Instance;

        if (battleManager != null)
        {
            Debug.Log("BattleManager를 찾았습니다.");
            // 동료 정보에 맞게 버튼을 동적으로 생성
            GenerateTeammateButtons();
        }
        else
        {
            Debug.LogError("BattleManager를 찾을 수 없습니다.");
        }
    }

    // 동료들의 정보를 바탕으로 버튼을 생성하는 메서드
    void GenerateTeammateButtons()
    {
        // 버튼을 생성할 부모 객체 초기화 (이전 버튼들은 삭제)
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);  // 이전 버튼 삭제
        }

        // 배틀에 참여하는 동료들 목록 가져오기
        if (battleManager != null && battleManager.battleTeammates.Count > 0)
        {
            Debug.Log("배틀에 참여하는 동료 수: " + battleManager.battleTeammates.Count); // 동료 수 확인
            foreach (Teammate teammate in battleManager.battleTeammates)
            {
                // 버튼 생성
                GameObject newButton = Instantiate(buttonPrefab, buttonContainer);

                // 버튼의 텍스트 업데이트 (동료 이름)
                TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();  // TextMeshProUGUI로 변경
                if (buttonText != null)
                {
                    buttonText.text = teammate.teammateName;  // 동료 이름을 텍스트로 설정
                }
                else
                {
                    Debug.LogError("버튼에 TextMeshProUGUI 컴포넌트가 없습니다.");
                }

                // 버튼에 클릭 이벤트 추가
                Button button = newButton.GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.AddListener(() => OnTeammateButtonClick(teammate));
                    Debug.Log("버튼 클릭 이벤트 등록됨: " + teammate.teammateName);
                }
                else
                {
                    Debug.LogError("버튼에 Button 컴포넌트가 없습니다.");
                }
            }

            // 배틀 상태 텍스트 업데이트
            battleStatusText.text = "배틀 준비 중...";
        }
        else
        {
            Debug.LogError("배틀에 참여하는 동료가 없습니다.");
        }
    }

    // 동료 버튼 클릭 시 호출되는 메서드
    void OnTeammateButtonClick(Teammate clickedTeammate)
    {
        Debug.Log(clickedTeammate.teammateName + "의 버튼이 클릭되었습니다.");
        // 클릭된 동료에 대한 추가 로직 (예: 스킬 사용, 공격 등)
        StartBattleWithTeammate(clickedTeammate);
    }

    // 예시: 클릭된 동료로 배틀 시작
    void StartBattleWithTeammate(Teammate teammate)
    {
        // 배틀 시작 로직 (동료에 따른 행동 추가)
        Debug.Log(teammate.teammateName + "로 배틀을 시작합니다.");
    }
}
