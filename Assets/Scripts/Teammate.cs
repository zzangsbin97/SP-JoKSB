using System.Collections.Generic;
using UnityEngine;

public class Teammate : MonoBehaviour
{
    public int maxHP;
    public int speed;
    public int standGauge = 50;
    public int attackPower;
    public double defensePercentTeammate;
    public string teammateName;
    public List<Skill> skills = new List<Skill>();
    public bool skillsInitialized = false;
    public bool IsInMyTeam = false;



    /*void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("일단충돌함...");
        Player player = other.GetComponent<Player>();

        if (player != null) {
            Debug.Log(teammateName);
            (this.gameObject.activeSelf != false) {
                IsInMyTeam = true;
                teammateManager.AddTeammate(this);
            } */
 


    private static readonly Dictionary<string, TeammateData> teammateDataDict = new Dictionary<string, TeammateData>
    {
        {
            "김수빈",
            new TeammateData(220, 100, 200, 10.0, new List<Skill>
            {
                new Skill("번개같은 이동", 110, 0.0, 0),
                new Skill("강력한 펀치", 180, 0.0, 0),
                new Skill("스타 플래티넘 러쉬", 430, 0.0, 0)
            })
        },
        {
            "오정훈",
            new TeammateData(190, 120, 120, 10.0, new List<Skill>
            {
                new Skill("화염구", 90, 0.0, 0),
                new Skill("불꽃의 일격", 230, 0.0, 0),
                new Skill("지옥의 불꽃", 490, 0.0, 0)
            })
        },
        {
            "조정훈",
            new TeammateData(270, 90, 90, 10.0, new List<Skill>
            {
                new Skill("대지의 결의", 0, 20.0, 0),
                new Skill("강인한 의지", 0, 0.0, 0),
                new Skill("대지의 분노", 280, 0.0, 0)
            })
        },
        {
            "유재민",
            new TeammateData(200, 80, 180, 10.0, new List<Skill>
            {
                new Skill("전기 충격", 120, 0.0, 0),
                new Skill("전기 강화", 0, 0.0, 0),
                new Skill("천둥의 심판", 350, 0.0, 0)
            })
        },
        {
            "최동욱 & 나비",
            new TeammateData(180, 80, 120, 10.0, new List<Skill>
            {
                new Skill("치유의 바람", 0, 0.0, 0),
                new Skill("바람의 쇄도", 100, 0.0, 0),
                new Skill("회복의 신풍", 0, 0.0, 0)
            })
        }
    };

    void Start()
    {
        InitializeTeammate(teammateName);
    }

    public void InitializeTeammate(string name)
    {
        if (teammateDataDict.TryGetValue(name, out TeammateData data))
        {
            teammateName = name;
            maxHP = data.MaxHP;
            attackPower = data.AttackPower;
            speed = data.Speed;
            defensePercentTeammate = data.DefensePercent;
            skills = new List<Skill>(data.Skills);
            skillsInitialized = true;
        }
        else
        {
            Debug.LogError($"Teammate data for {name} not found!");
        }
    }

    private class TeammateData
    {
        public int MaxHP { get; }
        public int AttackPower { get; }
        public int Speed { get; }
        public double DefensePercent { get; }
        public List<Skill> Skills { get; }

        public TeammateData(int maxHP, int attackPower, int speed, double defensePercent, List<Skill> skills)
        {
            MaxHP = maxHP;
            AttackPower = attackPower;
            Speed = speed;
            DefensePercent = defensePercent;
            Skills = skills;
        }
    }
}
