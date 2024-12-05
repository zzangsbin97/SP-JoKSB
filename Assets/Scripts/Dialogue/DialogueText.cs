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
		talkText.Add(1, new string[]{ "김수빈\n저기, 혼자서 싸우고 있는 거야? 꽤나 강해 보이는데", "오정훈\n누구야? 여기서 뭘 하고 있는 거지?",
				"김수빈\n난 일산국제과학연구소장 김수빈이야. 네 불꽃 능력, 굉장히 인상적이군.", "오정훈\n흥, 인상적이라고? 그" +
				"딴 걸로는 내가 움직이지 않아. 나도 내 방식대로 싸우는 중이거든.", "김수빈\n그래? 그럼 나랑 손잡고 같이 싸워보는 건 어때? 너의 힘" +
				"을 제대로 발휘하려면 강한 동료가 필요할 거야.", "오정훈\n흥, 뭐... 그렇게까지 내가 필요하다면야.... 좋아. 네 실력도 보고 싶으니까 같이 싸워 주지."});
		talkText.Add(2, new string[] { "김수빈\n오, 네 랩 꽤나 에너지가 넘치는데. 이 전기가 흐르는 것 같은 분위기, 장난 아니군.", "유재민\n난 유재민, 전기의 맥박! 이 리" +
			"듬 속에서 누구도 날 막지 못해. 에너지를 퍼뜨리고 사기를 복돋아주지!", "김수빈\n좋아. 그런 에너지라면 우리 팀에 딱이야. 전투 중에도 랩과 전기로 우리 팀" +
			"을 지원해주면 어때?", "유재민\nYo, 싸움은 내가 즐기는 무대! 네가 내 곁에 서면 전율이 흐르지. 내 비트와 전기로 널 강화할 테니 준비해!", "김수빈\n멋진 파트너" +
			"가 생겼군. 유재민, 함께하면 강한 적도 문제 없을 거야.", "유재민\nAh~ Yeah! 내가 널 이끌게! 공력력과 스" +
			"피드 모두 Boom up! 우리 팀의 스테이지는 누구도 못 끊 어!" });
		talkText.Add(-1, new string[] { "외계인 척후병\n크큭... 인간 따위가 감히 나를 상대하려 드는군.", "외계인 척후병\n지나가고 싶다면 날 쓰러뜨러야 할거야.", "외계인 척후병\n...할 수 있다면 말이지... 큭. 큭. 큭...ww(쏯)"});

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
