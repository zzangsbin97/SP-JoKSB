using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTextManager : MonoBehaviour
{

    // 블레오(오정훈) Id 1, 농담곰(유재민) Id 2
    // 오정훈 조정훈 유재민 최동욱(나비)

    Dictionary<int, string[]> talkText;

	private void Awake() {
		talkText = new Dictionary<int, string[]>();
		GenerateText();
	}

	void GenerateText() {

		// 김수빈: 0~2 오정훈 스크립트, 3~5 유재민 스크립트
		talkText.Add(0, new string[] { "저기, 혼자서 싸우고 있는 거야? 꽤나 강해 보이는데", "난 일산국제과학연구소장 김수빈이야. 네 불꽃 능력, 굉장히 인상적이군.", "그래? 그럼 나랑 손잡고 같이 싸워보는 건 어때? 너의 힘을 제대로 발휘하려면 강한 동료가 필요할 거야.", "오, 네 랩 꽤나 에너지가 넘치는데. 이 전기가 흐르는 것 같은 분위기 장난 아니군.", "좋아. 그런 에너지라면 우리 팀에 딱이야. 전투 중에도 랩과 전기로 우리 팀을 지원해주면 어때?", "멋진 파트너가 생겼군. 유재민, 우리가 함께라면 아무리 강한 적도 문제 없을 거야."});
		
		//오정훈
		talkText.Add(1, new string[] { "누구야? 여기서 뭘 하고 있는 거지?", "흥, 인상적이라고? 그딴 걸로는 내가 움직이지 않아. 나도 내 방식대로 싸우는 중이거든.", "흥. 뭐... 그렇게까지 내가 필요하다면야. 좋아. 네 실력도 보고 싶으니까 같이 싸워주지" });
		
		// 유재민
		talkText.Add(2, new string[] { "난 유재민, 전기의 맥박! 이 리듬 속에서 누구도 날 막지 못해! 에너지를 퍼뜨리고 사기를 복돋아주지!", "Yo, 싸움은 내가 즐기는 무대! 네가 내 곁에 서면 전율이 흐르지. 내 비트와 전기로 널 강화할 테니 준비해!", "아~ Yeah~ 내가 널 이끌게! 공격력과 스피드 모두 Boom Up! 우리 팀의 스테이지는 누구도 못 끊 어!"});
	}



}
