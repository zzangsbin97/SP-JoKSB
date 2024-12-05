using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
	public InputAction interAction; // 상호작용 키
	Rigidbody2D rigidbody2d;
	public TeammateDialogueManager teammateDialogueManager; // 대화 매니저
	public GameObject scanObject; // 현재 상호작용 대상

	void Start() {
		rigidbody2d = GetComponent<Rigidbody2D>();
		interAction.Enable(); // 입력 활성화
		interAction.performed += OnInterAction; // 상호작용 키 입력 연결
	}

	void Update() {
		FindTeammate(); // 매 프레임마다 주변 팀원 감지
	}

	void FindTeammate() {
		RaycastHit2D[] hits = {
			Physics2D.Raycast(rigidbody2d.position, Vector2.right, 1.5f, LayerMask.GetMask("Teammate")),
			Physics2D.Raycast(rigidbody2d.position, Vector2.left, 1.5f, LayerMask.GetMask("Teammate")),
			Physics2D.Raycast(rigidbody2d.position, Vector2.up, 1.5f, LayerMask.GetMask("Teammate")),
			Physics2D.Raycast(rigidbody2d.position, Vector2.down, 1.5f, LayerMask.GetMask("Teammate"))
		};

		foreach (var hit in hits) {
			if (hit.collider != null) {
				scanObject = hit.collider.gameObject;
				return;
			}
		}
		scanObject = null; // 감지된 팀원이 없으면 초기화
	}

	void OnInterAction(InputAction.CallbackContext context) {
		if (scanObject != null) {
			teammateDialogueManager.ProgressDialogue(scanObject);
		}
	}
}
