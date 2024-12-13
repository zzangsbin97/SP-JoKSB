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

		// Monster ���̾�� �浹 �� ó��
		if (other.gameObject.TryGetComponent<Monster>(out Monster collidedMonster)) {
			// MonsterManager�� currentMonster ����
			MonsterManager.Instance.SetCurrentMonster(collidedMonster);

			Debug.Log($"�浹�� ����: {collidedMonster.MonsterName}, ���ݷ�: {collidedMonster.attackPower}");

			if (Math.Abs(collidedMonster.monsterCategory) != 3) {
				// ��ũ��Ʈ�� �����ϱ� ��ȭ ����
				TeammateDialogueManager.Instance.ProgressDialogue(collidedMonster.gameObject); // GameObject ����
			} else {
				// ����� �ٷ� Battle ������ ��ȯ
				SceneManager.LoadScene("Battle");
			}
		}

		/*
		// FieldMonster_b�� �浹�� ��� ó��
		if (other.gameObject.layer == LayerMask.NameToLayer("FieldMonster_b")) {
			Monster collidedMonster = other.gameObject.GetComponent<Monster>();
			if (collidedMonster != null) {
				MonsterManager.Instance.currentMonster = collidedMonster; 
				Debug.Log($"�浹�� ����: {collidedMonster.MonsterName}, ���ݷ�: {collidedMonster.attackPower}");
				SceneManager.LoadScene("Battle"); 
			}
		}

		// FieldMonster_a�� �浹�� ��� ó��
		if (other.gameObject.layer == LayerMask.NameToLayer("FieldMonster_a")) {
			Monster collidedMonster = other.gameObject.GetComponent<Monster>();
			if (collidedMonster != null) {
				MonsterManager.Instance.currentMonster = collidedMonster; 
				Debug.Log($"�浹�� ����: {collidedMonster.MonsterName}, ���ݷ�: {collidedMonster.attackPower}");
				SceneManager.LoadScene("Battle"); 
			}
		}

		// Boss�� �浹�� ��� ó��
		if (other.gameObject.layer == LayerMask.NameToLayer("Boss")) {
			Monster collidedBoss = other.gameObject.GetComponent<Monster>();
			if (collidedBoss != null) {
				MonsterManager.Instance.currentMonster = collidedBoss; 
				Debug.Log($"������ �浹: {collidedBoss.MonsterName}, ���ݷ�: {collidedBoss.attackPower}");
				SceneManager.LoadScene("Battle"); 
			}
		}
		*/
	}
}
