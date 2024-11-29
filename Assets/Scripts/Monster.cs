using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    public int maxHP;
    public int speed;
    public int standGauge = 50;
    public int attackPower;
    public int currentHP;
    public double defensePercent;
    public string MonsterName;
    public List<Skill> skills = new List<Skill>();
    public bool skillsInitialized = false;
    public bool stun = false; //���� ����

    private static readonly Dictionary<string, MonsterData> MonsterDataDict = new Dictionary<string, MonsterData>
    {
        
        {
            "�ܰ��� �ϱ�����",
            new MonsterData(420, 40, 90, 10.0, new List<Skill>
            {
                new Skill("�����ϰ� �ֵθ���", 120, 0.0, 0,15),
                new Skill("������ ��ħ", 0, 0.0, 0,25)

            })
        },
        {
            "�ܰ��� ô�ĺ�",
            new MonsterData(800, 100, 150, 15.0, new List<Skill>
            {
                new Skill("������ ����", 80, 0.0, 0,30),
                new Skill("���� ����", 180, 0.0, 0,50),
                new Skill("���ڽ� ����",120,0.0,0,100)
            })
        },
        {
            "�ܰ��� ������",
            new MonsterData(1200, 80, 100, 15.0, new List<Skill>
            {
                new Skill("�����", 150, 0.0, 0,30),
                new Skill("���� ����", 0, 0.0, 0,50),
                new Skill("������",170,0.0,0,100)
            })
        }
    };
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }

    public void InitializeMonster(string name)
    {
        if (MonsterDataDict.TryGetValue(name, out MonsterData data))
        {
            MonsterName = name;
            maxHP = data.MaxHP;
            attackPower = data.AttackPower;
            speed = data.Speed;
            defensePercent = data.DefensePercent;
            skills = new List<Skill>(data.Skills);
            skillsInitialized = true;
            stun = false;
        }
        else
        {
            Debug.LogError($"Teammate data for {name} not found!");
        }
    }

    private class MonsterData
    {
        public int MaxHP { get; }
        public int AttackPower { get; }
        public int Speed { get; }
        public double DefensePercent { get; }
        public List<Skill> Skills { get; }

        public MonsterData(int maxHP, int attackPower, int speed, double defensePercent, List<Skill> skills)
        {
            MaxHP = maxHP;
            AttackPower = attackPower;
            Speed = speed;
            DefensePercent = defensePercent;
            Skills = skills;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {


        DontDestroyOnLoad(gameObject);


    }
}