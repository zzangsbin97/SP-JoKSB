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
            // DontDestroyOnLoad(gameObject); // 필요 시 활성화
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // TeammateManager에서 정보를 가져와 초기화
        InitializeBattleTeammates();

        // 디버그 출력
        foreach (Teammate teammate in battleTeammates)
        {
            Debug.Log("배틀에 참여한 동료: " + teammate.teammateName);
        }
    }

    private void InitializeBattleTeammates()
    {
        TeammateManager teammateManager = FindObjectOfType<TeammateManager>();
        if (teammateManager != null)
        {
            // TeammateManager에서 활성화된 동료 목록 가져오기
            battleTeammates = new List<Teammate>(teammateManager.teammates);
        }
        else
        {
            Debug.LogError("TeammateManager를 찾을 수 없습니다!");
        }
    }
}
