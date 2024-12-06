using System.Collections.Generic;
using UnityEngine;

public class TeammateManager : MonoBehaviour
{
    public static TeammateManager Instance { get; private set; }
    public List<Teammate> teammates = new List<Teammate>(); // 현재 팀에 있는 동료 목록

    void Start()
    {
        // 새로운 Teammate 생성
        GameObject teammateObject = new GameObject("김수빈");
        Teammate Teammate = teammateObject.AddComponent<Teammate>();

        // Teammate 초기화
        Teammate.InitializeTeammate("김수빈");
        DontDestroyOnLoad(Teammate);

        // 팀 목록에 추가
        AddTeammate(Teammate);


        teammateObject = new GameObject("오정훈");
        Teammate = teammateObject.AddComponent<Teammate>();
        Teammate.InitializeTeammate("오정훈");
        AddTeammate(Teammate);
        DontDestroyOnLoad(Teammate);

        teammateObject = new GameObject("조정훈");
        Teammate = teammateObject.AddComponent<Teammate>();
        Teammate.InitializeTeammate("조정훈");
        AddTeammate(Teammate);
        DontDestroyOnLoad(Teammate);


        teammateObject = new GameObject("유재민");
        Teammate = teammateObject.AddComponent<Teammate>();
        Teammate.InitializeTeammate("유재민");
        AddTeammate(Teammate);
        DontDestroyOnLoad(Teammate);

        teammateObject = new GameObject("최동욱 & 나비");
        Teammate = teammateObject.AddComponent<Teammate>();
        Teammate.InitializeTeammate("최동욱 & 나비");
        AddTeammate(Teammate);


        DontDestroyOnLoad(Teammate);

    }
    public void UpdateTeammates(List<Teammate> updatedTeammates)
    {
        if (updatedTeammates == null || updatedTeammates.Count == 0)
        {
            Debug.LogWarning("TeammateManager: 전달된 팀원이 비어 있습니다.");
            return;
        }

        teammates.Clear();
        teammates.AddRange(updatedTeammates);

        Debug.Log($"TeammateManager: {updatedTeammates.Count}명의 동료 데이터를 업데이트했습니다.");
    }

    void Update()
     {
            

     }
     void Awake()
     {


        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


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
    

