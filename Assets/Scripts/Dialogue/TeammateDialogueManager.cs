using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TeammateDialogueManager : MonoBehaviour {
	public static TeammateDialogueManager Instance;

	public GameObject dialoguePanel;
	public TMP_Text talkText;
	public DialogueText dialogueText;
	public GameObject talkingObject;
	public int textIndex = 0;
	private bool isTalking = false;

	private void Awake() {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}

	public void ProgressDialogue(GameObject TalkingObject) {
		if (this.talkingObject != TalkingObject) {
			this.talkingObject = TalkingObject;
			textIndex = 0;
			dialoguePanel.SetActive(true);
			isTalking = true;
		}

		Id teammateId = TalkingObject.GetComponent<Id>();
		if (teammateId == null) {
			Debug.LogError("Teammate에 Id 컴포넌트가 없습니다!");
			return;
		}

		ShowDialogue(teammateId.objectId);
	}

	private void Update() {
		if (isTalking && Input.GetKeyDown(KeyCode.E)) {
			if (talkingObject != null) {
				Id teammateId = talkingObject.GetComponent<Id>();
				ShowDialogue(teammateId.objectId);
			}
		}
	}

	public void ShowDialogue(int id) {
		string dialogueTemp = dialogueText.GetText(id, textIndex);

		if (dialogueTemp == null) {
			EndDialogue();
			return;
		}

		talkText.text = dialogueTemp;
		textIndex++;
	}

	void EndDialogue() {
		dialoguePanel.SetActive(false);
		textIndex = 0;
		talkingObject.SetActive(false);
		talkingObject = null;
		isTalking = false;

		SceneManager.LoadScene("Battle");
	}
}
