using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;

public class BattleEnter : MonoBehaviour {
	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	private void OnTriggerEnter2D(Collider2D other) {

		// Monster 레이어와 충돌 시 처리
		if (other.gameObject.TryGetComponent<Monster>(out Monster collidedMonster)) {
			// MonsterManager에 currentMonster 설정
			MonsterManager.Instance.SetCurrentMonster(collidedMonster);

			Debug.Log($"충돌한 몬스터: {collidedMonster.MonsterName}, 공격력: {collidedMonster.attackPower}");

			if (Math.Abs(collidedMonster.monsterCategory) != 1) {
				// 스크립트가 있으니까 대화 진행
				TeammateDialogueManager.Instance.ProgressDialogue(collidedMonster.gameObject); // GameObject 전달
			} else {
				// 잡몹은 바로 Battle 씬으로 전환
				SceneManager.LoadScene("Battle");
			}
		}

		/*
		// FieldMonster_b와 충돌한 경우 처리
		if (other.gameObject.layer == LayerMask.NameToLayer("FieldMonster_b")) {
			Monster collidedMonster = other.gameObject.GetComponent<Monster>();
			if (collidedMonster != null) {
				MonsterManager.Instance.currentMonster = collidedMonster; 
				Debug.Log($"충돌한 몬스터: {collidedMonster.MonsterName}, 공격력: {collidedMonster.attackPower}");
				SceneManager.LoadScene("Battle"); 
			}
		}

		// FieldMonster_a와 충돌한 경우 처리
		if (other.gameObject.layer == LayerMask.NameToLayer("FieldMonster_a")) {
			Monster collidedMonster = other.gameObject.GetComponent<Monster>();
			if (collidedMonster != null) {
				MonsterManager.Instance.currentMonster = collidedMonster; 
				Debug.Log($"충돌한 몬스터: {collidedMonster.MonsterName}, 공격력: {collidedMonster.attackPower}");
				SceneManager.LoadScene("Battle"); 
			}
		}

		// Boss와 충돌한 경우 처리
		if (other.gameObject.layer == LayerMask.NameToLayer("Boss")) {
			Monster collidedBoss = other.gameObject.GetComponent<Monster>();
			if (collidedBoss != null) {
				MonsterManager.Instance.currentMonster = collidedBoss; 
				Debug.Log($"보스와 충돌: {collidedBoss.MonsterName}, 공격력: {collidedBoss.attackPower}");
				SceneManager.LoadScene("Battle"); 
			}
		}
		*/
	}
}
