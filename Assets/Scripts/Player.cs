using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public int playerHP; // 몇으로 할지
    public int playerStandGauge; // 못정했삼
    public double deffensePercent;

    public InputAction interAction;
    Rigidbody2D rigidbody2d;

	// Vector2 rightDirection = new Vector2(1, 0);


	// Start is called before the first frame update
	void Start(){

        rigidbody2d = GetComponent<Rigidbody2D>();
        interAction.Enable();
        interAction.performed += FindTeammate;
    }

	// Update is called once per frame
	void Update() {

	}

	void FindTeammate(InputAction.CallbackContext context) {
		// RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.40f, moveDirection, 1.5f, LayerMask.GetMask("Teammate"));
		RaycastHit2D hitR = Physics2D.Raycast(rigidbody2d.position, Vector2.right, 1.5f, LayerMask.GetMask("Teammate"));
		RaycastHit2D hitL = Physics2D.Raycast(rigidbody2d.position, Vector2.left, 1.5f, LayerMask.GetMask("Teammate"));
		RaycastHit2D hitU = Physics2D.Raycast(rigidbody2d.position, Vector2.up, 1.5f, LayerMask.GetMask("Teammate"));
		RaycastHit2D hitD = Physics2D.Raycast(rigidbody2d.position, Vector2.down, 1.5f, LayerMask.GetMask("Teammate"));


		if (hitR.collider != null) {
            Debug.Log("Raycast has hit the object " + hitR.collider.gameObject);


			// 여기에 대화 창 뜨게 하기
			hitR.collider.gameObject.SetActive(false); // 상호작용한 "Teammate" 레이어 객체 사라지게 하기
		}

		if (hitL.collider != null) {
			Debug.Log("Raycast has hit the object " + hitL.collider.gameObject);


			// 여기에 대화 창 뜨게 하기
			hitL.collider.gameObject.SetActive(false); // 상호작용한 "Teammate" 레이어 객체 사라지게 하기
		}

		if (hitU.collider != null) {
			Debug.Log("Raycast has hit the object " + hitU.collider.gameObject);


			// 여기에 대화 창 뜨게 하기
			hitU.collider.gameObject.SetActive(false); // 상호작용한 "Teammate" 레이어 객체 사라지게 하기
		}

		if (hitD.collider != null) {
			Debug.Log("Raycast has hit the object " + hitD.collider.gameObject);


			// 여기에 대화 창 뜨게 하기
			hitD.collider.gameObject.SetActive(false); // 상호작용한 "Teammate" 레이어 객체 사라지게 하기
		}
	}

    
}
