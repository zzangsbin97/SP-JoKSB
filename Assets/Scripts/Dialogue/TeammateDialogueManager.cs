using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TeammateDialogueManager : MonoBehaviour {
	public GameObject dialoguePanel; // 대화 패널
	public TMP_Text talkText; // 대화 텍스트 UI
	public DialogueText dialogueText; // 대사 데이터
	public GameObject talkingTeammate; // 현재 대화 중인 팀원
	public int textIndex = 0; // 대사 인덱스

	public void ProgressDialogue(GameObject talkingTeammate) {
		if (this.talkingTeammate != talkingTeammate) {
			// 새로운 팀원과 대화 시작
			this.talkingTeammate = talkingTeammate;
			textIndex = 0; // 인덱스 초기화
			dialoguePanel.SetActive(true); // 대화 패널 활성화
		}

		Id teammateId = talkingTeammate.GetComponent<Id>();
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
		talkingTeammate.SetActive(false); // 상호작용 대상 비활성화
		talkingTeammate = null; // 상태 초기화
	}
}
