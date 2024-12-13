using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
	public InputAction interAction;   // 상호작용 키
	private Rigidbody2D rigidbody2d;
	public AudioSource audioSrc;      // 발소리 AudioSource
	public TeammateDialogueManager teammateDialogueManager;
	public GameObject scanObject;    // 현재 상호작용 대상

	private Vector2 moveInput;

	void Start() {
		rigidbody2d = GetComponent<Rigidbody2D>();
		audioSrc = GetComponent<AudioSource>();

		interAction.Enable();
		interAction.performed += OnInterAction;
	}

	void Update() {
		FindTeammate();    // 주변 팀원 감지
		HandleMovement();  // 이동 처리
	}

	void HandleMovement() {
		// 방향키 입력 처리
		moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		// 이동 시 발소리 재생
		if (moveInput.magnitude > 0 && !audioSrc.isPlaying) {
			audioSrc.Play();  // 발소리 재생
		} else if (moveInput.magnitude == 0 && audioSrc.isPlaying) {
			audioSrc.Stop();  // 멈출 때 발소리 정지
		}

		// 이동 속도 적용
		// rigidbody2d.velocity = moveInput.normalized * 5f;
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
		scanObject = null;
	}

	void OnInterAction(InputAction.CallbackContext context) {
		if (scanObject != null) {
			teammateDialogueManager.ProgressDialogue(scanObject);
		}
	}
}
