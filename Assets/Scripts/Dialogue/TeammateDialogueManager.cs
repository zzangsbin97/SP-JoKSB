using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TeammateDialogueManager : MonoBehaviour {

	public static TeammateDialogueManager Instance;

	public GameObject dialoguePanel; // 대화 패널
	public TMP_Text talkText; // 대화 텍스트 UI
	public DialogueText dialogueText; // 대사 데이터
	public GameObject talkingObject; // 현재 대화 중인 팀원
	public int textIndex = 0; // 대사 인덱스
	private bool isTalking = false;

	// public TeammateManager tempTeammateManager;


	public void ProgressDialogue(GameObject TalkingObject) {

		if (this.talkingObject != TalkingObject) {
			// 새로운 팀원과 대화 시작
			this.talkingObject = TalkingObject;
			// Teammate tempTeammate = talkingTeammate.GetComponent<Teammate>();
			// tempTeammateManager.teammates.Add(tempTeammate);
			textIndex = 0; // 인덱스 초기화
			dialoguePanel.SetActive(true); // 대화 패널 활성화

		}

		Id teammateId = TalkingObject.GetComponent<Id>();
		if (teammateId == null) {
			Debug.LogError("Teammate에 Id 컴포넌트가 없습니다!");
			return;
		}

		ShowDialogue(teammateId.objectId);
	}

	public void ShowDialogue(int id) {
		string dialogueTemp = dialogueText.GetText(id, textIndex);

		if (dialogueTemp == null) {
			// 대화 종료
			EndDialogue();
			return;
		}

		// 다음 대사 출력
		talkText.text = dialogueTemp;
		textIndex++;
	}

	void EndDialogue() {
		dialoguePanel.SetActive(false); // 패널 비활성화
		textIndex = 0; // 인덱스 초기화
		talkingObject.SetActive(false); // 상호작용 대상 비활성화
		talkingObject = null; // 상태 초기화
	}

	public void Awake() {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않음
		} else {
			Destroy(gameObject); // 중복된 인스턴스 방지
		}
	}
}
