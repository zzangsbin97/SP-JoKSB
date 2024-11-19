using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUI : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform buttonContainer;    // 버튼 부모 객체
    //public TextMeshProUGUI battleStatusText; // 배틀 상태 텍스트

    private BattleManager battleManager;

    void Start()
    {
        battleManager = BattleManager.Instance;

        if (battleManager != null)
        {
            Debug.Log("BattleManager를 찾았습니다.");
            // 동료 정보에 맞게 버튼 동적 생성
            GenerateTeammateButtons();
        }
        else
        {
            Debug.LogError("BattleManager를 찾을 수 없습니다.");
        }
    }

    // 정보를 바탕으로 버튼을 생성하는 메서드
    void GenerateTeammateButtons()
    {
        Debug.Log("GenerateTeammateButtons 시작");

        // 부모 객체 확인
        if (buttonContainer == null)
        {
            Debug.LogError("buttonContainer가 null입니다. Inspector에서 할당되었는지 확인하세요.");
            return;
        }


        // 부모 객체 초기화 (이전 버튼 삭제)
        foreach (Transform child in buttonContainer)
        {
            Debug.Log("기존 버튼 삭제 중: " + child.name);
            Destroy(child.gameObject);
        }

        // BattleManager와 동료 목록 확인
        if (battleManager == null || battleManager.battleTeammates == null)
        {
            Debug.LogError("battleManager 또는 battleTeammates가 null입니다.");
            return;
        }

        if (battleManager.battleTeammates.Count == 0)
        {
            Debug.LogError("배틀에 참여하는 동료가 없습니다.");
            return;
        }

        Debug.Log("배틀 동료 수: " + battleManager.battleTeammates.Count);

		// 버튼 생성 여부 확인 플래그
		bool buttonCreated = false;


		// 동료 정보에 따라 버튼 생성
		foreach (Teammate teammate in battleManager.battleTeammates)
        {
            Debug.Log("동료 버튼 생성 중: " + (teammate.teammateName ?? "이름 없음"));

            if (buttonPrefab == null)
            {
                Debug.LogError("buttonPrefab이 null입니다. Inspector에서 할당되었는지 확인하세요.");
                return;
            }

            // 버튼 생성
            GameObject newButton = Instantiate(buttonPrefab, buttonContainer);
            
            // 버튼 텍스트 설정
            TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();

            if (buttonText != null)
            {
                buttonText.text = string.IsNullOrEmpty(teammate.teammateName) ? "이름 없음" : teammate.teammateName;
            }
            else
            {
                Debug.LogError("buttonPrefab에 TextMeshProUGUI 컴포넌트가 없습니다.");
            }

            // 버튼 클릭 이벤트 설정
            Button button = newButton.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => OnTeammateButtonClick(teammate));
                Debug.Log("버튼 클릭 이벤트 등록됨: " + teammate.teammateName);
            }
            else
            {
                Debug.LogError("buttonPrefab에 Button 컴포넌트가 없습니다.");
            }

			// 버튼 좌표 출력
			if (newButton != null) {
				buttonCreated = true; // 버튼이 생성되었음을 확인
				Vector3 buttonPosition = newButton.transform.position;
				Debug.Log($"버튼 생성됨: {teammate.teammateName}, 위치: {buttonPosition}");
			}

		}

		// 버튼 생성 여부 확인
		if (!buttonCreated) {
			Debug.LogError("버튼 생성 안됨");
		}

		Debug.Log("GenerateTeammateButtons 완료");

		// 배틀 상태 텍스트 업데이트
		/*if (battleStatusText != null)
        {
            battleStatusText.text = "배틀 준비 중...";
        }
        else
        {
            Debug.LogError("battleStatusText가 null입니다. Inspector에서 할당되었는지 확인하세요.");
        }

        Debug.Log("GenerateTeammateButtons 완료");*/
	}

	// 버튼 클릭 시 호출되는 메서드
	void OnTeammateButtonClick(Teammate clickedTeammate)
    {
        Debug.Log(clickedTeammate.teammateName + "의 버튼이 클릭되었습니다.");
        // 클릭 후 추가 로직 (예: 스킬 사용, 공격 등)
        StartBattleWithTeammate(clickedTeammate);
    }

    // 예시: 배틀 시작 로직
    void StartBattleWithTeammate(Teammate teammate)
    {
        Debug.Log(teammate.teammateName + "로 배틀을 시작합니다.");
    }
}
