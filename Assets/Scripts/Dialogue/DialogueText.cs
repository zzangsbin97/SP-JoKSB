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
		// ���� ��ȭ ������
		talkText.Add(1, new string[]{ "�����\n����, ȥ�ڼ� �ο�� �ִ� �ž�? �ϳ� ���� ���̴µ�", "������\n������? ���⼭ �� �ϰ� �ִ� ����?",
				"�����\n�� �ϻ걹�����п������� ������̾�. �� �Ҳ� �ɷ�, ������ �λ����̱�.", "������\n��, �λ����̶��? ��" +
				"�� �ɷδ� ���� �������� �ʾ�. ���� �� ��Ĵ�� �ο�� ���̰ŵ�.", "�����\n�׷�? �׷� ���� ����� ���� �ο����� �� �? ���� ��" +
				"�� ����� �����Ϸ��� ���� ���ᰡ �ʿ��� �ž�.", "������\n��, ��... �׷��Ա��� ���� �ʿ��ϴٸ��.... ����. �� �Ƿµ� ���� �����ϱ� ���� �ο� ����."});
		talkText.Add(2, new string[] { "�����\n��, �� �� �ϳ� �������� ��ġ�µ�. �� ���Ⱑ �帣�� �� ���� ������, �峭 �ƴϱ�.", "�����\n�� �����, ������ �ƹ�! �� ��" +
			"�� �ӿ��� ������ �� ���� ����. �������� �۶߸��� ��⸦ ����������!", "�����\n����. �׷� ��������� �츮 ���� ���̾�. ���� �߿��� ���� ����� �츮 ��" +
			"�� �������ָ� �?", "�����\nYo, �ο��� ���� ���� ����! �װ� �� �翡 ���� ������ �帣��. �� ��Ʈ�� ����� �� ��ȭ�� �״� �غ���!", "�����\n���� ��Ʈ��" +
			"�� ���屺. �����, �Բ��ϸ� ���� ���� ���� ���� �ž�.", "�����\n��~ Yeah! ���� �� �̲���! ���·°� ��" +
			"�ǵ� ��� Boom up! �츮 ���� ���������� ������ �� �� ��!" });
	}

	public string GetText(int id, int textIndex) {
		if (!talkText.ContainsKey(id)) {
			Debug.LogError($"ID {id}�� �ش��ϴ� ��簡 �����ϴ�.");
			return null;
		}

		if (textIndex >= talkText[id].Length) {
			return null; // ��ȭ ��
		}

		return talkText[id][textIndex];
	}
}