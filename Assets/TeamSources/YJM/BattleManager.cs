using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    
    public static BattleManager Instance { get; private set; }

    public List<Teammate> battleTeammates = new List<Teammate>();

    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // 씬 전환 시 BattleManager를 유지
        }
        else
        {
            Destroy(gameObject); // 이미 존재하면 새로운 인스턴스는 삭제
        }
    }

    // 예시: 동료 추가 메서드
    public void AddTeammate(Teammate teammate)
    {
        if (!battleTeammates.Contains(teammate))
        {
            battleTeammates.Add(teammate);
            Debug.Log(teammate.teammateName + "가 팀에 추가되었습니다.");
        }
    }

    // 예시: 동료 추가 로직
    void Start()
    {
        // 임시 동료 객체 추가 (테스트용)
        AddTestTeammates();

        // 배틀에 참여하는 동료들 로그 출력
        foreach (Teammate teammate in battleTeammates)
        {
            Debug.Log("배틀에 참여한 동료: " + teammate.teammateName);
        }
    }

    // 임시 동료 추가 메서드
    void AddTestTeammates()
    {
        Teammate newTeammate1 = new Teammate();
        newTeammate1.teammateName = "Teammate 1";
        AddTeammate(newTeammate1);

        Teammate newTeammate2 = new Teammate();
        newTeammate2.teammateName = "Teammate 2";
        AddTeammate(newTeammate2);
    }
}
