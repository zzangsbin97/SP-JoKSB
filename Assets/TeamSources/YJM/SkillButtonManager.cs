using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillButtonManager : MonoBehaviour
{
    public List<Button> buttons; // 이미 생성된 버튼 리스트

    void Start()
    {
        // 시작 시 모든 버튼 비활성화
        DisableAllButtons();
    }

    public void DisableAllButtons()
    {
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(false); // 버튼 비활성화
        }
    }

    public void ActivateSkillButtons(Teammate teammate)
    {
        Debug.Log("ActivateSkillButtons() 호출됨");

        if (teammate == null)
        {
            Debug.LogWarning("해당 동료가 존재하지 않습니다.");
            DisableAllButtons();
            return;
        }

        // 스킬이 없는 경우 초기화
        if (teammate.skills.Count == 0)
        {
            switch (teammate.teammateName)
            {
                case "kimsubin":
                    teammate.skills.Add(new Skill("번개같은 이동", 110, 0.0, 0));
                    teammate.skills.Add(new Skill("강력한 펀치", 180, 0.0, 0));
                    teammate.skills.Add(new Skill("스타 플래티넘 러쉬", 430, 0.0, 0));
                    break;
            }
        }

        // 스킬 버튼 설정
        for (int i = 0; i < buttons.Count; i++)
        {
            if (i < teammate.skills.Count)
            {
                Button button = buttons[i];
                button.gameObject.SetActive(true);

                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    buttonText.text = teammate.skills[i].skillName;
                }

                int skillIndex = i; // 클로저 문제 방지
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => OnSkillButtonClicked(teammate.skills[skillIndex]));

                Debug.Log($"버튼 {i + 1} 활성화: {teammate.skills[i].skillName}");
            }
            else
            {
                buttons[i].gameObject.SetActive(false);
                Debug.Log($"버튼 {i + 1} 비활성화");
            }
        }
    }

    private void OnSkillButtonClicked(Skill skill)
    {
        Debug.Log($"스킬 '{skill.skillName}' 버튼 클릭됨");
        // 스킬 사용 로직 구현
    }
}
