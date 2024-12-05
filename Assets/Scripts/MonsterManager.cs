using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager Instance;
    public Monster currentMonster; // 현재 등장하는 몬스터

    // Start is called before the first frame update
    void Start()
    {
        /*
        GameObject MonsterObject = new GameObject("외계인 정예병");
        Monster monster = MonsterObject.AddComponent<Monster>();

        monster.InitializeMonster("외계인 정예병");
        */

        // currentMonster = monster;
        Debug.Log($"배틀에 등장한 몬스터: {currentMonster.MonsterName}");
        Debug.Log($"HP: {currentMonster.maxHP}, 공격력: {currentMonster.attackPower}, 스킬 개수: {currentMonster.skills.Count}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	private void Awake() {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않음
		} else {
			Destroy(gameObject); // 중복된 매니저가 생성되지 않도록 방지
		}
	}

	public void SetCurrentMonster(Monster monster) {
		if (monster != null) {
			currentMonster = monster; // 현재 몬스터 설정
			currentMonster.InitializeMonster(currentMonster.MonsterName); // MonsterName으로 데이터 초기화
			Debug.Log($"MonsterManager에 설정된 몬스터: {currentMonster.MonsterName}");
		} else {
			Debug.LogError("전달된 Monster 객체가 null입니다.");
		}
	}
}
