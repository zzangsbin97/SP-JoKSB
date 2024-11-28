using System.Collections.Generic;
using UnityEngine;

public class Teammate : MonoBehaviour
{
    public int maxHP;
    public int currentHP;
    public int speed;
    public int standGauge = 50;
    public double attackPercent;
    public double defensePercentTeammate;
    public string teammateName;
    public List<Skill> skills = new List<Skill>();
    public bool skillsInitialized = false;
    public bool IsInMyTeam = false;
    public bool stun = false; //기절 유무



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
                new Skill("번개같은 이동", 1.1, 0.0, 0,20),
                new Skill("강력한 펀치", 1.8, 0.0, 35),
                new Skill("스타 플래티넘 러쉬", 4.3, 0.0, 100)
            })
        },
        {
            "오정훈",
            new TeammateData(190, 120, 120, 10.0, new List<Skill>
            {
                new Skill("화염구", 0.9, 0.0, 15),
                new Skill("불꽃의 일격", 2.3, 0.0, 30),
                new Skill("지옥의 불꽃", 4.9, 0.0, 100)
            })
        },
        {
            "조정훈",
            new TeammateData(270, 90, 90, 10.0, new List<Skill>
            {
                new Skill("대지의 결의", 0, 20.0, 10),
                new Skill("강인한 의지", 0, 0.0, 25),
                new Skill("대지의 분노", 2.8, 0.0, 100)
            })
        },
        {
            "유재민",
            new TeammateData(200, 80, 180, 10.0, new List<Skill>
            {
                new Skill("전기 충격", 1.2, 0.0, 15),
                new Skill("전기 강화", 0, 0.0, 20),
                new Skill("천둥의 심판", 3.5, 0.0, 100)
            })
        },
        {
            "최동욱 & 나비",
            new TeammateData(180, 80, 120, 10.0, new List<Skill>
            {
                new Skill("치유의 바람", 0, 0.0, 20),
                new Skill("바람의 쇄도", 1, 0.0, 40),
                new Skill("회복의 신풍", 0, 0.0, 100)
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
            currentHP = maxHP;
            attackPercent = data.AttackPercent;
            speed = data.Speed;
            defensePercentTeammate = data.DefensePercent;
            skills = new List<Skill>(data.Skills);
            skillsInitialized = true;
            stun = false;
        }
        else
        {
            Debug.LogError($"Teammate data for {name} not found!");
        }
    }

    private class TeammateData
    {
        public int MaxHP { get; }
        public int CurrentHP { get; }
        public double AttackPercent { get; }
        public int Speed { get; }
        public double DefensePercent { get; }
        public List<Skill> Skills { get; }

        public TeammateData(int maxHP, double attackPercent, int speed, double defensePercent, List<Skill> skills)
        {
            MaxHP = maxHP;
            AttackPercent = attackPercent;
            Speed = speed;
            DefensePercent = defensePercent;
            Skills = skills;
        }
    }
}
