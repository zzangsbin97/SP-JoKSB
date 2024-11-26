using System.Collections.Generic;
using UnityEngine;
using System;

public class FieldMonster_Before : MonoBehaviour {
	public int maxHP;
	public int speed;
	public int standGauge;
	public int attackPower;
	public int currentHP;
	public double defensePercent;
	public string MonsterName;
	public List<Skill> skills = new List<Skill>();
	public bool skillsInitialized = false;

	private Dictionary<string, MonsterData_Before> monsterDataDict;

	public double RandomDamage(int startPercent, int endPercent, int baseAttackPower) {
		System.Random rand = new System.Random();
		double randPercent = rand.Next(startPercent, endPercent) / 100.0;
		return randPercent * baseAttackPower;
	}

	void Start() {
		currentHP = maxHP;
		InitializeMonsterData(); // 동적 초기화
		InitializeMonster("외계인 하급전사");
	}

	private void InitializeMonsterData() {
		monsterDataDict = new Dictionary<string, MonsterData_Before> {
			{
				"외계인 하급전사",
				new MonsterData_Before(
					maxHP: 420,
					attackPower: 40, // 인스턴스 메서드 호출
                    speed: 90,
					defensePercent: 10,
					skills: new List<Skill> {
						new Skill("강렬하게 휘두르기", (int)RandomDamage(80, 120, this.attackPower), 0.0, 0)
					}
				)
			}
		};
	}

	public void InitializeMonster(string name) {
		if (monsterDataDict.TryGetValue(name, out MonsterData_Before data)) {
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

	private class MonsterData_Before {
		public int MaxHP { get; }
		public int AttackPower { get; }
		public int Speed { get; }
		public double DefensePercent { get; }
		public List<Skill> Skills { get; }

		public MonsterData_Before(int maxHP, int attackPower, int speed, double defensePercent, List<Skill> skills) {
			MaxHP = maxHP;
			AttackPower = attackPower;
			Speed = speed;
			DefensePercent = defensePercent;
			Skills = skills;
		}
	}
}
