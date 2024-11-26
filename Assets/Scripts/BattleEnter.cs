using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleEnter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D other) {
        // if (other.CompareTag("Monster"))
        if (other.gameObject.layer == LayerMask.NameToLayer("FieldMonster_b")){
            FieldMonster_Before monster_b = other.gameObject.GetComponent<FieldMonster_Before>();
            Debug.Log(monster_b.MonsterName + " " + monster_b.attackPower);

			// Debug.Log(monster.name);
			Debug.Log("Monster류와 충돌 -> Battle 씬으로 이동");
            SceneManager.LoadScene("Battle"); // Battle 씬으로 전환
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("FieldMonster_a")) {
			FieldMonster_After monster_a = other.gameObject.GetComponent<FieldMonster_After>();
			Debug.Log(monster_a.MonsterName + " " + monster_a.attackPower);

			// Debug.Log(monster.name);
			Debug.Log("Monster류와 충돌 -> Battle 씬으로 이동");
			SceneManager.LoadScene("Battle"); // Battle 씬으로 전환
		}



       if (other.gameObject.layer == LayerMask.NameToLayer("Boss")){
			Boss boss = other.gameObject.GetComponent<Boss>();
			Debug.Log("아무튼보스랑충돌함...");
		}
	}
}
