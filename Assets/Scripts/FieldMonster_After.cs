using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FieldMonster_After : MonoBehaviour {
	public int maxHP;
	public int speed;
	public int standGauge;
	public int attackPower;
	public int currentHP;
	public double defensePercent;
	public string MonsterName;
	public List<Skill> skills = new List<Skill>();
	public bool skillsInitialized = false;


	private Dictionary<string, MonsterData_After> monsterDataDict;

	public double RandomDamage(int startPercent, int endPercent, int baseAttackPower) {
		System.Random rand = new System.Random();
		double randPercent = rand.Next(startPercent, endPercent) / 100.0;
		return randPercent * baseAttackPower;
	}

	// Start is called before the first frame update
	void Start() {
		FieldMonster_After fieldmonster_after = GetComponent<FieldMonster_After>();
		currentHP = maxHP;
		InitializeMonsterData();
		InitializeMonster("외계인 정예병");

	}

	private void InitializeMonsterData() {
		monsterDataDict = new Dictionary<string, MonsterData_After> {
			{
				"외계인 정예병",
				new MonsterData_After(
					maxHP: 1200,
					attackPower: 80, // 인스턴스 메서드 호출
                    speed: 90,
					defensePercent: 10,
					skills: new List<Skill> {
						new Skill("충격파", (int)RandomDamage(130, 150, this.attackPower), 0.0, 0)
					}
				)
			}
		};
	}

	public void InitializeMonster(string name) {
		if (monsterDataDict.TryGetValue(name, out MonsterData_After data)) {
			MonsterName = name;
			maxHP = data.MaxHP;
			attackPower = data.AttackPower;
			speed = data.Speed;
			defensePercent = data.DefensePercent;
			skills = new List<Skill>(data.Skills);
			skillsInitialized = true;
		} else {
			Debug.LogError($"Monster data for {name} not found!");
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
