using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    public int maxHP;
    public int speed;
    public int standGauge = 50;
    public int attackPower;
    public double defensePercent;
    public string MonsterName;
    public List<Skill> skills = new List<Skill>();
    public bool skillsInitialized = false;

    private static readonly Dictionary<string, MonsterData> MonsterDataDict = new Dictionary<string, MonsterData>
    {
        {
            "김규석",
            new MonsterData(1200, 150, 130, 10.0, new List<Skill>
            {
                new Skill("얼음 창", 80, 0.0, 0),
                new Skill("혹한의 바람", 60, 0.0, 0),
                new Skill("절대 영도", 120, 0.0, 0)
            })
        }
    };
    // Start is called before the first frame update
    void Start()
    {
        
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
