using System.Collections.Generic;
using UnityEngine;

public class TeammateManager : MonoBehaviour
{
    public List<Teammate> teammates = new List<Teammate>(); // 현재 팀에 있는 동료 목록

    void Start()
    {

        // 새로운 Teammate 생성
        GameObject teammateObject = new GameObject("김수빈");
        Teammate Teammate = teammateObject.AddComponent<Teammate>();

        // Teammate 초기화
        Teammate.InitializeTeammate("김수빈");


        // 팀 목록에 추가
        AddTeammate(Teammate);

        teammateObject = new GameObject("오정훈");
        Teammate = teammateObject.AddComponent<Teammate>();
        Teammate.InitializeTeammate("오정훈");
        AddTeammate(Teammate);

        teammateObject = new GameObject("유재민");
        Teammate = teammateObject.AddComponent<Teammate>();
        Teammate.InitializeTeammate("유재민");
        AddTeammate(Teammate);
    }

     void Update()
     {
            

     }
     void Awake()
     {


        DontDestroyOnLoad(gameObject);


     }
    public void AddTeammate(Teammate teammate)
    {
        // 이미 팀에 추가된 동료인지 확인
        if (teammates.Exists(t => t.teammateName == teammate.teammateName))
        {
            Debug.LogWarning($"{teammate.teammateName}은(는) 이미 팀에 추가되어 있습니다.");
            return;
        }

        // 팀 목록에 추가
        teammates.Add(teammate);

        Debug.Log($"{teammate.teammateName}이(가) 팀에 추가되었습니다.");
    }
}
    

