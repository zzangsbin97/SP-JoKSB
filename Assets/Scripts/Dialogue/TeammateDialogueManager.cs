using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TeammateDialogueManager : MonoBehaviour {
	public static TeammateDialogueManager Instance; // 싱글톤 인스턴스

	public GameObject dialoguePanel; // 대화 패널
	public TMP_Text talkText; // 대화 텍스트 UI
	public DialogueText dialogueText; // 대사 데이터
	public GameObject talkingObject; // 현재 대화 중인 객체 (Monster/Teammate)
	public int textIndex = 0; // 대사 인덱스
	public bool isTalking = false; // 대화 중 상태 플래그

	private bool canProceed = true; // 대사 입력 가능 여부

	private void Awake() {
		// 싱글톤 초기화
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않음
		} else {
			Destroy(gameObject); // 중복된 인스턴스 방지
		}
	}

	// 대화 시작 메서드
	public void ProgressDialogue(GameObject TalkingObject) {
		if (this.talkingObject != TalkingObject) {
			// 새로운 대화 시작
			this.talkingObject = TalkingObject;
			textIndex = 0; // 인덱스 초기화
			dialoguePanel.SetActive(true); // 대화 패널 활성화
			isTalking = true; // 대화 중 상태 설정
		}

		Id objectId = TalkingObject.GetComponent<Id>();
		if (objectId == null) {
			Debug.LogError("대상에 Id 컴포넌트가 없습니다!");
			return;
		}

		ShowDialogue(objectId.objectId); // 첫 번째 대사 자동 출력
	}

	private void Update() {
		// 대화 중일 때만 입력을 처리
		if (isTalking && canProceed && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))) {
			if (talkingObject != null) {
				Id objectId = talkingObject.GetComponent<Id>();
				ShowDialogue(objectId.objectId); // 대사 진행
			}
		}
	}

	// 대사 출력
	public void ShowDialogue(int id) {
		string dialogueTemp = dialogueText.GetText(id, textIndex);

		if (dialogueTemp == null) {
			EndDialogue(); // 대화 종료
			return;
		}

		// 다음 대사 출력
		talkText.text = dialogueTemp;
		textIndex++;

		// 다음 입력을 일시적으로 차단
		StartCoroutine(WaitBeforeNextDialogue());
	}

	// 입력 차단 시간 설정
	private IEnumerator WaitBeforeNextDialogue() {
		canProceed = false;
		yield return new WaitForSeconds(0.1f); // 0.1초 후 입력 가능
		canProceed = true;
	}

	// 대화 종료 처리
	public void EndDialogue() {
		dialoguePanel.SetActive(false); // 패널 비활성화
		textIndex = 0; // 인덱스 초기화
		talkingObject.SetActive(false); // 상호작용 대상 비활성화
		isTalking = false; // 대화 중 상태 해제

		// 대화 대상이 Monster인지 확인
		if (talkingObject != null && talkingObject.GetComponent<Monster>() != null) {
			Debug.Log("Monster와 대화 종료 -> Battle 씬으로 전환");
			SceneManager.LoadScene("Battle"); // Battle 씬 전환
		}

		talkingObject = null; // 대화 대상 초기화
	}
}
