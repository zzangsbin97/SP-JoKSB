using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FieldMonster_After : MonoBehaviour {
	public int maxHP = 420;
	public int speed = 90;
	public int standGauge = 50;
	public int attackPower = 40;
	public int currentHP;
	public double defensePercent = 10;
	public string MonsterName;
	public List<Skill> skills = new List<Skill>();
	public bool skillsInitialized = false;

	// Start is called before the first frame update
	void Start() {
		currentHP = maxHP;
	}

	private static readonly Dictionary<string, MonsterData_After> MonsterDataDict = new Dictionary<string, MonsterData_After> {
		{ "외계인 정예병",
			new MonsterData_After(800, 100, 150, 15, new List<Skill> {
				new Skill("고속 돌진", 190, 0.0, 0)

			})
		}

	};

	// Update is called once per frame
	void Update() {

	}

	public void InitializeMonster(string name) {
		if (MonsterDataDict.TryGetValue(name, out MonsterData_After data)) {
			MonsterName = name;
			maxHP = data.MaxHP;
			attackPower = data.AttackPower;
			speed = data.Speed;
			defensePercent = data.DefensePercent;
			skills = new List<Skill>(data.Skills);
			skillsInitialized = true;
		} else {
			Debug.LogError($"Teammate data for {name} not found!");
		}
	}

	private class MonsterData_After {
		public int MaxHP { get; }
		public int AttackPower { get; }
		public int Speed { get; }
		public double DefensePercent { get; }
		public List<Skill> Skills { get; }

		public MonsterData_After(int maxHP, int attackPower, int speed, double defensePercent, List<Skill> skills) {
			MaxHP = maxHP;
			AttackPower = attackPower;
			Speed = speed;
			DefensePercent = defensePercent;
			Skills = skills;
		}
	}
}
