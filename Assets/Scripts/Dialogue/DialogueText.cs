using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueText : MonoBehaviour {
	Dictionary<int, string[]> talkText;

	private void Awake() {
		talkText = new Dictionary<int, string[]>();
		GenerateText();
	}

	void GenerateText() {
		// 예제 대화 데이터
		talkText.Add(1, new string[] { "누구야? 여기서 뭘 하고 있는 거지?", "흥, 인상적이라고? 그딴 걸로는 내가 움직이지 않아.", "흥. 뭐... 그렇게까지 내가 필요하다면야. 좋아." });
		talkText.Add(2, new string[] { "난 유재민, 전기의 맥박!", "Yo, 싸움은 내가 즐기는 무대!", "아~ Yeah~ 내가 널 이끌게!" });
	}

	public string GetText(int id, int textIndex) {
		if (!talkText.ContainsKey(id)) {
			Debug.LogError($"ID {id}에 해당하는 대사가 없습니다.");
			return null;
		}

		if (textIndex >= talkText[id].Length) {
			return null; // 대화 끝
		}

		return talkText[id][textIndex];
	}
}
