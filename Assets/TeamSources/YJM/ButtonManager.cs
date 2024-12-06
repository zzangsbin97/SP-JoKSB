using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    public List<Button> buttons; // 동료 버튼 리스트 (미리 생성된 버튼 연결)
    private BattleManager battleManager;

    [SerializeField] private SkillButtonManager skillButtonManager;

    private void Start()
    {
        // BattleManager의 싱글톤 인스턴스 가져오기
        battleManager = BattleManager.Instance;

        if (battleManager == null)
        {
            Debug.LogError("BattleManager를 찾을 수 없습니다!");
            return;
        }

        // SkillButtonManager 자동 검색
        if (skillButtonManager == null)
        {
            skillButtonManager = FindObjectOfType<SkillButtonManager>();
            if (skillButtonManager == null)
            {
                Debug.LogError("SkillButtonManager를 찾을 수 없습니다!");
                return;
            }
        }

        UpdateButtons(); // 버튼 초기화
    }

    public void UpdateButtons()
    {
        List<Teammate> teammates = battleManager.battleTeammates;

        if (teammates == null || teammates.Count == 0)
        {
            Debug.LogWarning("BattleManager에 동료 데이터가 없습니다.");
            DisableAllButtons();
            return;
        }

        Debug.Log($"ButtonManager: 총 {teammates.Count}명의 동료 데이터를 수신.");
        foreach (Teammate teammate in teammates)
        {
            Debug.Log($"({teammate.teammateName}");
        }

        int buttonIndex = 0;
        foreach (var teammate in teammates)
        {
            if (buttonIndex > buttons.Count)
            {
                Debug.LogWarning("ButtonManager: 등록된 버튼이 부족합니다.");
                break;
            }

            Button button = buttons[buttonIndex];
            button.gameObject.SetActive(true);

            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = teammate.teammateName; // 버튼 텍스트 업데이트
            }

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => OnTeammateButtonClicked(teammate)); // 클릭 이벤트 설정

            Debug.Log($"ButtonManager: {teammate.teammateName} 버튼 활성화 완료.");
            buttonIndex++;
        }

        DisableExtraButtons(buttonIndex);
    }

    private void OnTeammateButtonClicked(Teammate teammate)
    {
        Debug.Log($"{teammate.teammateName} 버튼 클릭됨");

        if (skillButtonManager != null)
        {
            skillButtonManager.ActivateSkillButtons(teammate); // 스킬 버튼 활성화
        }
        else
        {
            Debug.LogError("SkillButtonManager가 연결되지 않았습니다!");
        }
    }

    private void DisableAllButtons()
    {
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }

    private void DisableExtraButtons(int activeCount)
    {
        for (int i = activeCount; i < buttons.Count; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
    }
}
