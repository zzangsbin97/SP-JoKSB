using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int maxHP;
    public int speed;
    public int standGauge = 50;
    public int attackPower;
    public int currentHP;
    public double defensePercent;
    public string bossName;
    public List<Skill> skills = new List<Skill>();
    public bool skillsInitialized = false;
    public bool stun = false; //기절 유무

    private static readonly Dictionary<string, BossData> BossDataDict = new Dictionary<string, BossData>
    {
        {
            "김규석",
            new BossData(1800, 150, 130, 10.0, new List<Skill>
            {
                new Skill("얼음 창", 80, 0.0, 0,40),
                new Skill("혹한의 바람", 60, 0.0, 0,60),
                new Skill("절대 영도", 120, 0.0, 0,100)
            })
        }
    };
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }

    public void InitializeBoss(string name)
    {
        if (BossDataDict.TryGetValue(name, out BossData data))
        {
            bossName = name;
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

    private class BossData
    {
        public int MaxHP { get; }
        public int AttackPower { get; }
        public int Speed { get; }
        public double DefensePercent { get; }
        public List<Skill> Skills { get; }

        public BossData(int maxHP, int attackPower, int speed, double defensePercent, List<Skill> skills)
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
