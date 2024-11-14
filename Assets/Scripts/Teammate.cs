using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teammate : MonoBehaviour
{
    public int maxHP; // 이것도 몇으로 할지
    public int standGauge; // 못정했삼
    public double defensePercentTeammate = 10.0f; // 방어력을 10%로 초기화

    // 각 동료별 이름 만들어서...
    public string teammateName;

    // 스킬 넣을 리스트 생성
    public List<Skill> skills = new List<Skill>();

    public bool IsInMyTeam = false;
    private TeammateManager teammateManager;


    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("일단충돌함...");
        Player player = other.GetComponent<Player>();

        if (player != null) {
            Debug.Log(teammateName);
            if (this.gameObject.activeSelf != false) {
                IsInMyTeam = true;
                teammateManager.AddTeammate(this);
            }
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        teammateManager = FindObjectOfType<TeammateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
