using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
	public InputAction interAction;   // ��ȣ�ۿ� Ű
	private Rigidbody2D rigidbody2d;
	public AudioSource audioSrc;      // �߼Ҹ� AudioSource
	public TeammateDialogueManager teammateDialogueManager;
	public GameObject scanObject;    // ���� ��ȣ�ۿ� ���

	private Vector2 moveInput;

	void Start() {
		rigidbody2d = GetComponent<Rigidbody2D>();
		audioSrc = GetComponent<AudioSource>();

		interAction.Enable();
		interAction.performed += OnInterAction;
	}

	void Update() {
		FindTeammate();    // �ֺ� ���� ����
		HandleMovement();  // �̵� ó��
	}

	void HandleMovement() {
		// ����Ű �Է� ó��
		moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		// �̵� �� �߼Ҹ� ���
		if (moveInput.magnitude > 0 && !audioSrc.isPlaying) {
			audioSrc.Play();  // �߼Ҹ� ���
		} else if (moveInput.magnitude == 0 && audioSrc.isPlaying) {
			audioSrc.Stop();  // ���� �� �߼Ҹ� ����
		}

		// �̵� �ӵ� ����
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
