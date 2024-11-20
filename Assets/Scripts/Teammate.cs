using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teammate : MonoBehaviour
{
    public int maxHP; // 이것도 몇으로 할지
    public int speed;
    public int standGauge = 50; // 못정했삼
    public int attackPower;
    public double defensePercentTeammate = 10.0f; // 방어력을 10%로 초기화
    public bool skillsInitialized = false;
    // 각 동료별 이름 만들어서...
    public string teammateName;

    // 스킬 넣을 리스트 생성
    public List<Skill> skills = new List<Skill>();

    public bool IsInMyTeam = false;
    private TeammateManager teammateManager;

    /*void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("일단충돌함...");
        Player player = other.GetComponent<Player>();

        if (player != null) {
            Debug.Log(teammateName);
            (this.gameObject.activeSelf != false) {
                IsInMyTeam = true;
                teammateManager.AddTeammate(this);
            } 
        }
    }*/

    // Start is called before the first frame update
    void Start()
    {
        teammateManager = FindObjectOfType<TeammateManager>();
    }

    public void InitializeTeammate(string name)
    {
        

        teammateName = name;
        switch (teammateName)
        {
            case "kimsubin":
                maxHP = 220;
                attackPower = 100;
                speed = 200;
                defensePercentTeammate = 10.0f;
                skills.Add(new Skill("번개같은 이동", 110, 0.0, 0));
                skills.Add(new Skill("강력한 펀치", 180, 0.0, 0));
                skills.Add(new Skill("스타 플래티넘 러쉬", 430, 0.0, 0));
                break;
            case "오정훈":
                maxHP = 190;
                attackPower = 120;
                speed = 120;
                defensePercentTeammate = 10.0f;
                skills.Add(new Skill("화염구", 90, 0.0, 0));
                skills.Add(new Skill("불꽃의 일격", 230, 0.0, 0));
                skills.Add(new Skill("지옥의 불꽃", 490, 0.0, 0));
                break;
            case "조정훈":
                maxHP = 270;
                attackPower = 90;
                speed = 90;
                defensePercentTeammate = 10.0f;
                skills.Add(new Skill("대지의 결의", 0, 20.0, 0));
                skills.Add(new Skill("강인한 의지", 0, 0.0, 0));
                skills.Add(new Skill("대지의 분노", 280, 0.0, 0));
                break;
            case "유재민":
                maxHP = 200;
                attackPower = 80;
                speed = 180;
                defensePercentTeammate = 10.0f;
                skills.Add(new Skill("전기 충격", 120, 0.0, 0));
                skills.Add(new Skill("전기 강화", 0, 0.0, 0));
                skills.Add(new Skill("천둥의 심판", 350, 0.0, 0));
                break;
            case "최동욱 & 나비":
                maxHP = 180;
                attackPower = 80;
                speed = 120;
                defensePercentTeammate = 10.0f;
                skills.Add(new Skill("치유의 바람", 0, 0.0, 0));
                skills.Add(new Skill("바람의 쇄도", 100, 0.0, 0));
                skills.Add(new Skill("회복의 신풍", 0, 0.0, 0));
                break;
        }
        skillsInitialized = true;
    }
}


