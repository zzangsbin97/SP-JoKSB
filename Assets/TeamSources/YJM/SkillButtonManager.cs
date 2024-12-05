using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillButtonManager : MonoBehaviour
{
    public List<Button> skillButtons = new List<Button>(); // 이미 생성된 버튼 리스트
    public BattleManager battleManager;
    public Teammate ActiveTeammate;
    public Monster monster;
    public Skill skill;
    private PriorityQueue actionQueue = new PriorityQueue(); // 우선순위 큐

    void Start()
    {
        battleManager = BattleManager.Instance;
        if (battleManager == null)
        {
            Debug.LogError("BattleManager가 초기화되지 않았습니다!");
        }

        // 시작 시 모든 버튼 비활성화
        DisableAllButtons();
    }

    public void DisableAllButtons()
    {
        foreach (var button in skillButtons)
        {
            button.gameObject.SetActive(false); // 버튼 비활성화
        }
    }

    public void ActivateSkillButtons(Teammate teammate)
    {
        if (Object.ReferenceEquals(teammate, null))
        {
            Debug.LogError("전달된 Teammate는 Unity에서 삭제되었습니다.");
            DisableAllButtons();
            return;
        }

        ActiveTeammate = teammate;

        Debug.Log($"ActivateSkillButtons 호출: 이름 = {ActiveTeammate.teammateName}, 체력 = {ActiveTeammate.maxHP}, 스킬 개수 = {ActiveTeammate.skills?.Count ?? 0}");

        if (ActiveTeammate.skills == null || ActiveTeammate.skills.Count == 0)
        {
            Debug.LogWarning($"{ActiveTeammate.teammateName}에게 스킬이 없습니다.");
            DisableAllButtons();
            return;
        }

        for (int i = 0; i < skillButtons.Count; i++)
        {
            if (i < ActiveTeammate.skills.Count)
            {
                Button button = skillButtons[i];
                button.gameObject.SetActive(true);

                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    buttonText.text = ActiveTeammate.skills[i].skillName;
                }

                
                int capturedIndex = i; // 클로저 문제 방지
                
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => OnSkillButtonClicked(ActiveTeammate,ActiveTeammate.skills[capturedIndex]));

                Debug.Log($"버튼 {i + 1} 활성화: {ActiveTeammate.skills[i].skillName}");
            }
            else
            {
                skillButtons[i].gameObject.SetActive(false);
                Debug.Log($"버튼 {i + 1} 비활성화");
            }
        }
    }
    public void OnSkillButtonClicked(Teammate Teammate, Skill skill)
    {
        Debug.Log($"OnSkillButtonClicked호출: 이름 = {Teammate.teammateName}, 체력 = {Teammate.maxHP}, 스킬 개수 = {Teammate.skills?.Count ?? 0}");
        if (!Teammate.usedSkill)
        {
            if (!Teammate.stun)
            {
                Debug.Log($"스킬 '{skill.skillName}' 버튼 클릭됨");
                Debug.Log($"스킬의 정보 : 이름 : {skill.skillName}, 공격력 : {skill.attackDamage}, 방어력 증가 : {skill.defensePercent}, 버프 : {skill.buffConst} ");
                /*if (Teammate == null)
                {
                    Debug.LogError("SkillButtonManager에서 전달된 teammate이 null입니다!");
                }*/

                if (skill == null)
                {
                    Debug.LogError("SkillButtonManager에서 전달된 skill이 null입니다!");
                }

                if (Teammate.standGauge - skill.usingStandGauge >= 0)
                {
                    Teammate.usedSkill = true;
                    actionQueue.Enqueue(new ActionData(Teammate, skill));
                    DisableAllButtons();
                    CheckAndExecuteQueue();
                }
                else
                {
                    Debug.Log($"{Teammate.teammateName}의 스탠드 게이지가 부족합니다!!");
                }
            }
            else
            {
                Debug.Log($"{Teammate.teammateName}가 기절상태입니다!");
            }
        }
        else
        {
            Debug.Log($"{Teammate.teammateName}는 이미 스킬을 사용했습니다.");
        }



    }

    public class ActionData //큐에 넣을거임
    {
        public Teammate teammate;
        public Skill skill;
        public int speed;

        public ActionData(Teammate teammate, Skill skill)
        {
            this.teammate = teammate;
            this.skill = skill;
            this.speed = teammate.speed;
        }
    }

    public class PriorityQueue//우선순위 큐
    {
        private List<ActionData> actions = new List<ActionData>();

        public void Enqueue(ActionData action)
        {
            actions.Add(action);
            actions.Sort((a, b) => b.speed.CompareTo(a.speed)); // 스피드가 높은 순서대로 정렬
        }

        public ActionData Dequeue()
        {
            if (actions.Count == 0) return null;
            var action = actions[0];
            actions.RemoveAt(0);
            return action;
        }

        public int Count => actions.Count;

        public void Clear()
        {
            actions.Clear();
        }
    }

    
    private void CheckAndExecuteQueue()
    {

        // 모든 행동이 입력되었는지 확인 (예: 행동 리스트가 특정 크기에 도달)
        if (actionQueue.Count >= battleManager.GetExpectedActionsCount())
        {
            StartCoroutine(ExecuteActions());
        }
    }

    private System.Collections.IEnumerator ExecuteActions()
    {
        while (actionQueue.Count > 0)
        {
            ActionData action = actionQueue.Dequeue();
            Debug.Log($"{action.teammate.teammateName}이(가) {action.skill.skillName}을(를) 실행합니다.");
            battleManager.ApplySkillDamage(action.teammate, action.skill);

            if ( action.teammate.speed < battleManager.battleMonster.speed && !battleManager.battleMonster.usedSkill)
            {
                
                System.Random rand = new System.Random();
                int randSkill = rand.Next(0, battleManager.battleMonster.skills.Count);
                battleManager.battleMonster.usedSkill = true;
                battleManager.MonsterSkillUse(battleManager.battleMonster, battleManager.battleMonster.skills[randSkill]);
            }
            // 행동 간 딜레이 추가
            yield return new WaitForSeconds(1.0f);
        }
        

    }
    
}
