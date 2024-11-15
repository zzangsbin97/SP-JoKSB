using System.Collections;
using System.Collections.Generic;
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
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster")){
            Debug.Log("Monster류와 충돌 -> Battle 씬으로 이동");
            SceneManager.LoadScene("Battle"); // Battle 씬으로 전환
        }
	}
}
