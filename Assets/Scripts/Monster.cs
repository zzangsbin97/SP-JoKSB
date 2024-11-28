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
    public bool stun = false; //기절 유무

    private static readonly Dictionary<string, MonsterData> MonsterDataDict = new Dictionary<string, MonsterData>
    {
        {
            "김규석",
            new MonsterData(1800, 150, 130, 10.0, new List<Skill>
            {
                new Skill("얼음 창", 80, 0.0, 0),
                new Skill("혹한의 바람", 60, 0.0, 0),
                new Skill("절대 영도", 120, 0.0, 0)
            })
        },
        {
            "외계인 하급전사",
            new MonsterData(420, 40, 90, 10.0, new List<Skill>
            {
                new Skill("강렬하게 휘두르기", 120, 0.0, 0),
                new Skill("공포의 외침", 0, 0.0, 0)

            })
        },
        {
            "외계인 척후병",
            new MonsterData(800, 100, 150, 15.0, new List<Skill>
            {
                new Skill("방해의 광선", 80, 0.0, 0),
                new Skill("고속 돌진", 180, 0.0, 0),
                new Skill("스텔스 어택",120,0.0,0)
            })
        },
        {
            "외계인 정예병",
            new MonsterData(1200, 80, 100, 15.0, new List<Skill>
            {
                new Skill("충격파", 150, 0.0, 0),
                new Skill("광역 방해", 0, 0.0, 0),
                new Skill("대폭발",170,0.0,0)
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
