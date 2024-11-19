using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TeammateDialogueManager : MonoBehaviour {
	public TMP_Text talkText; // Inspector에서 할당
	public GameObject talkingTeammate;

	public void Talk(GameObject talkingTeammate, string teammateName) {
		if (talkText == null) {
			Debug.LogError("talkText가 Inspector에서 할당되지 않았습니다!");
			return;
		}

		if (talkingTeammate == null) {
			Debug.LogError("talkingTeammate가 null입니다! 유효한 GameObject를 전달하세요.");
			return;
		}

		this.talkingTeammate = talkingTeammate;
		talkText.text = "말하는 동료의 이름은 " + teammateName;
	}
}
