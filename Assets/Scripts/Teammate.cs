using System.Collections.Generic;
using UnityEngine;

public class Teammate : MonoBehaviour
{
    // public int id;
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
    public bool stun = false; //���� ����
    public bool usedSkill = false; //��ų ��� ����

    public bool isDead = false;



    /*void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("�ϴ��浹��...");
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
            "�����",
            new TeammateData(220, 100, 200, 10.0, new List<Skill>
            {
                new Skill("�������� �̵�", 1.1, 0.0, 0,20),
                new Skill("������ ��ġ", 1.8, 0.0, 35),
                new Skill("��Ÿ �÷�Ƽ�� ����", 4.3, 0.0, 100)
            })
        },
        {
            "������",
            new TeammateData(190, 120, 120, 10.0, new List<Skill>
            {
                new Skill("ȭ����", 0.9, 0.0, 15),
                new Skill("�Ҳ��� �ϰ�", 2.3, 0.0, 30),
                new Skill("������ �Ҳ�", 4.9, 0.0, 100)
            })
        },
        {
            "������",
            new TeammateData(270, 90, 90, 10.0, new List<Skill>
            {
                new Skill("������ ����", 0, 20.0, 10),
                new Skill("������ ����", 0, 0.0, 25),
                new Skill("������ �г�", 2.8, 0.0, 100)
            })
        },
        {
            "�����",
            new TeammateData(200, 80, 180, 10.0, new List<Skill>
            {
                new Skill("���� ���", 1.2, 0.0, 15),
                new Skill("���� ��ȭ", 0, 0.0, 20),
                new Skill("õ���� ����", 3.5, 0.0, 100)
            })
        },
        {
            "�ֵ��� & ����",
            new TeammateData(180, 80, 120, 10.0, new List<Skill>
            {
                new Skill("ġ���� �ٶ�", 0, 0.0, 20),
                new Skill("�ٶ��� �⵵", 1, 0.0, 40),
                new Skill("ȸ���� ��ǳ", 0, 0.0, 100)
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
            bool usedSkill = false;
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
