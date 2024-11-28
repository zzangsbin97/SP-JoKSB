using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
	public int id;
	public int playerHP;
	public int playerStandGauge;
	public double deffensePercent;
	public bool isExist;

	public InputAction interAction; // 상호작용키
	Rigidbody2D rigidbody2d;
	private TeammateManager teammateManager;
	public TeammateDialogueManager teammateDialogueManager;


	void Start() {

		Screen.SetResolution(1080, 1920, true);
		Screen.SetResolution(Screen.width, (Screen.width * 16) / 9, true); // 화면 비율 고정

		rigidbody2d = GetComponent<Rigidbody2D>();
		interAction.Enable();
		interAction.performed += FindTeammate;
		teammateManager = FindObjectOfType<TeammateManager>(); // TeammateManager 가져오기
	}

	void FindTeammate(InputAction.CallbackContext context) {
		// 네 방향으로 Raycast 수행
		RaycastHit2D[] hits = {
			Physics2D.Raycast(rigidbody2d.position, Vector2.right, 1.5f, LayerMask.GetMask("Teammate")),
			Physics2D.Raycast(rigidbody2d.position, Vector2.left, 1.5f, LayerMask.GetMask("Teammate")),
			Physics2D.Raycast(rigidbody2d.position, Vector2.up, 1.5f, LayerMask.GetMask("Teammate")),
			Physics2D.Raycast(rigidbody2d.position, Vector2.down, 1.5f, LayerMask.GetMask("Teammate"))
		};

		foreach (var hit in hits) {
			if (hit.collider != null) {
				this.isExist = true;
				Teammate teammate = hit.collider.gameObject.GetComponent<Teammate>();
				if (teammate != null && !teammate.IsInMyTeam) // Teammate인지 확인하고 팀에 추가되지 않은 경우
				{
					Debug.Log(teammate.teammateName + "을(를) 찾았습니다!");
					teammate.IsInMyTeam = true; // 팀에 추가 표시
					//teammateManager.AddTeammate(teammate); // TeammateManager에 추가
					teammateDialogueManager.Talk(hit.collider.gameObject, teammate.teammateName);
					hit.collider.gameObject.SetActive(false); // 상호작용한 "Teammate" 객체 비활성화
				}
			}
		}
	}

	void Update() {

		if ( (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Space)) &&  isExist) {
			//teammateDialogueManager.Talk();
		}
	}
}
