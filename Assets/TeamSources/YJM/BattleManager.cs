using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }
    public TeammateManager teammateManager; // Unity Editor에서 연결 가능
    public List<Teammate> battleTeammates = new List<Teammate>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 싱글턴 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // TeammateManager가 연결되지 않았다면 FindObjectOfType로 찾음
        if (teammateManager == null)
        {
            teammateManager = FindObjectOfType<TeammateManager>();
        }

        if (teammateManager == null)
        {
            Debug.LogError("TeammateManager를 찾을 수 없습니다! 씬에 TeammateManager를 추가했는지 확인하세요.");
            return;
        }

        // TeammateManager 초기화가 완료될 때까지 기다림
        StartCoroutine(InitializeBattleTeammatesAfterManagerReady());
    }

    private System.Collections.IEnumerator InitializeBattleTeammatesAfterManagerReady()
    {
        // TeammateManager가 초기화될 때까지 기다림
        while (teammateManager.teammates.Count == 0)
        {
            Debug.Log("TeammateManager 초기화 대기 중...");
            yield return null; // 다음 프레임까지 대기
        }

        // Teammates 데이터를 BattleTeammates로 복사
        InitializeBattleTeammates();
        PrintBattleTeammates();

        Debug.Log("BattleTeammates in BattleManager:");
        foreach (var teammate in battleTeammates)
        {
            Debug.Log(teammate.teammateName);
        }
    }

    private void InitializeBattleTeammates()
    {
        battleTeammates = new List<Teammate>(teammateManager.teammates);
        Debug.Log($"이름: {battleTeammates[0].teammateName}, 체력: {battleTeammates[0].maxHP}, 공격력: {battleTeammates[0].attackPower}, 스킬: {battleTeammates[0].skills}");
        Debug.Log("BattleTeammates가 초기화되었습니다.");
    }

    private void PrintBattleTeammates()
    {
        if (battleTeammates.Count == 0)
        {
            Debug.LogWarning("배틀에 참여할 동료가 없습니다!");
            return;
        }

        foreach (Teammate teammate in battleTeammates)
        {
            Debug.Log($"배틀에 참여한 동료: {teammate.teammateName}");
        }
    }
}
