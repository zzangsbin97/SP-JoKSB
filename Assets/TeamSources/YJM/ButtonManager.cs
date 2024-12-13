using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    public List<Button> buttons; // ���� ��ư ����Ʈ (�̸� ������ ��ư ����)
    private BattleManager battleManager;

    [SerializeField] private SkillButtonManager skillButtonManager;

    private void Start()
    {
        // BattleManager�� �̱��� �ν��Ͻ� ��������
        battleManager = BattleManager.Instance;

        if (battleManager == null)
        {
            Debug.LogError("BattleManager�� ã�� �� �����ϴ�!");
            return;
        }

        // SkillButtonManager �ڵ� �˻�
        if (skillButtonManager == null)
        {
            skillButtonManager = FindObjectOfType<SkillButtonManager>();
            if (skillButtonManager == null)
            {
                Debug.LogError("SkillButtonManager�� ã�� �� �����ϴ�!");
                return;
            }
        }

        UpdateButtons(); // ��ư �ʱ�ȭ
    }

    public void UpdateButtons()
    {
        List<Teammate> teammates = battleManager.battleTeammates;

        if (teammates == null || teammates.Count == 0)
        {
            Debug.LogWarning("BattleManager�� ���� �����Ͱ� �����ϴ�.");
            DisableAllButtons();
            return;
        }

        Debug.Log($"ButtonManager: �� {teammates.Count}���� ���� �����͸� ����.");
       
        foreach (Teammate teammate in teammates)
        {
            Debug.Log($"({teammate.teammateName}");
        }

        int buttonIndex = 0;

        foreach (var teammate in teammates)
        {
            if (buttonIndex > buttons.Count)
            {
                Debug.LogWarning("ButtonManager: ��ϵ� ��ư�� �����մϴ�.");
                break;
            }

			Button button = buttons[buttonIndex];

			if (!teammate.isDead) {
                
                button.gameObject.SetActive(true);
            }

            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = teammate.teammateName; // ��ư �ؽ�Ʈ ������Ʈ
            }

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => OnTeammateButtonClicked(teammate)); // Ŭ�� �̺�Ʈ ����

            Debug.Log($"ButtonManager: {teammate.teammateName} ��ư Ȱ��ȭ �Ϸ�.");
            buttonIndex++;

            Slider teammateHpSlider = button.GetComponentInChildren<Slider>();

            if (teammateHpSlider != null) {
                teammateHpSlider.maxValue = teammate.maxHP;
                teammateHpSlider.value = teammate.currentHP;
            }
        }

        DisableExtraButtons(buttonIndex);
    }

    private void OnTeammateButtonClicked(Teammate teammate)
    {
        Debug.Log($"{teammate.teammateName} ��ư Ŭ����");

        if (skillButtonManager != null)
        {
            skillButtonManager.ActivateSkillButtons(teammate); // ��ų ��ư Ȱ��ȭ
        }
        else
        {
            Debug.LogError("SkillButtonManager�� ������� �ʾҽ��ϴ�!");
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
