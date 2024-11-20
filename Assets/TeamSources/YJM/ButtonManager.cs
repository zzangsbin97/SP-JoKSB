using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    public List<Button> buttons; // TeammatePanel 버튼 리스트
    private BattleManager battleManager;

    public List<Teammate> teammates = new List<Teammate>();

    [SerializeField] private SkillButtonManager skillButtonManager; // SkillButtonManager 참조

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
                Debug.LogError("SkillButtonManager를 찾을 수 없습니다! Unity 에디터에서 수동으로 연결하거나 스크립트를 확인하세요.");
                return;
            }
        }

        UpdateButtons(); // 동료 데이터를 기반으로 버튼 업데이트
    }

    public void UpdateButtons()
    {
        // BattleManager에서 동료 데이터를 가져옴
        List<Teammate> teammates = new List<Teammate>(battleManager.battleTeammates);
        Debug.Log($"이름: {teammates[0].teammateName}, 체력: {teammates[0].maxHP}, 공격력: {teammates[0].attackPower}, 스킬: {teammates[0].skills}");
        if (teammates == null || teammates.Count == 0)
        {
            Debug.LogWarning("BattleManager에 동료 데이터가 없습니다.");
            foreach (var button in buttons)
            {
                button.gameObject.SetActive(false); // 버튼 비활성화
            }
            return;
        }

        int buttonIndex = 0;
        foreach (var teammate in teammates)
        {
            if (buttonIndex >= buttons.Count)
            {
                Debug.LogWarning("TeammatePanel 버튼이 부족합니다.");
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

            buttonIndex++;
        }

        // 남은 버튼 비활성화
        for (int i = buttonIndex; i < buttons.Count; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }

        Canvas.ForceUpdateCanvases(); // UI 갱신
    }

    private void OnTeammateButtonClicked(Teammate teammate)
    {
        Debug.Log($"{teammate.teammateName} 버튼 클릭됨");

        if (skillButtonManager != null)
        {
            Debug.Log("SkillButtonManager가 정상적으로 연결되었습니다.");
            skillButtonManager.ActivateSkillButtons(teammate); // 동료 정보 전달
        }
        else
        {
            Debug.LogError("SkillButtonManager가 연결되지 않았습니다!");
        }
    }
}
