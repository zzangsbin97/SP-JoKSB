using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    public List<Button> buttons; // TeammatePanel 버튼 리스트
    private BattleManager battleManager;


    [SerializeField] private SkillButtonManager skillButtonManager; // SkillButtonManager 참조

    private void Start()
    {
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

        UpdateButtons();
    }
    public void UpdateButtons()
    {
        List<Teammate> teammates = battleManager.battleTeammates;

        if (teammates == null || teammates.Count == 0)
        {
            Debug.LogWarning("BattleManager에 동료 데이터가 없습니다.");
            foreach (var button in buttons)
            {
                button.gameObject.SetActive(false);
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
                buttonText.text = teammate.teammateName;
            }

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => OnTeammateButtonClicked(teammate));

            buttonIndex++;
        }

        for (int i = buttonIndex; i < buttons.Count; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }

        Canvas.ForceUpdateCanvases();
    }

    private void OnTeammateButtonClicked(Teammate teammate)
    {
        Debug.Log($"{teammate.teammateName} 버튼 클릭됨");

        if (skillButtonManager != null)
        {
            Debug.Log("SkillButtonManager가 정상적으로 연결되었습니다.");
            skillButtonManager.ActivateSkillButtons();
        }
        else
        {
            Debug.LogError("SkillButtonManager가 연결되지 않았습니다!");
        }
    }
}
