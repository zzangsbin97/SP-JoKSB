using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillButtonManager : MonoBehaviour
{
    public List<Button> buttons; // SkillPanel의 버튼 리스트

    void Start()
    {
        // 시작 시 모든 버튼 비활성화
        DisableAllButtons();
    }

    public void DisableAllButtons()
    {
        foreach (var button in buttons)
        {
            button.gameObject.SetActive(false); // 모든 버튼 비활성화
        }
    }

    public void ActivateSkillButtons()
    {
        Debug.Log("ActivateSkillButtons() 호출됨");

        for (int i = 0; i < buttons.Count; i++)
        {
            Button button = buttons[i];
            Debug.Log($"버튼 활성화: {button.name}");
            button.gameObject.SetActive(true);

            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = $"Skill {i + 1}";
            }

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => OnSkillButtonClicked(i + 1));
        }
    }


    private void OnSkillButtonClicked(int skillNumber)
    {
        Debug.Log($"Skill {skillNumber} 버튼 클릭됨");
    }
}
