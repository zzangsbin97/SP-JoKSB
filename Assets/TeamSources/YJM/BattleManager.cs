using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }
    public TeammateManager teammateManager; // Unity Editor에서 연결 가능
    public MonsterManager monsterManager; // Unity Editor에서 연결 가능
    public List<Teammate> battleTeammates = new List<Teammate>();
    public Monster battleMonster; // 배틀에 등장하는 몬스터

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

        if (monsterManager == null)
        {
            monsterManager = FindObjectOfType<MonsterManager>();
        }

        if (teammateManager == null)
        {
            Debug.LogError("TeammateManager를 찾을 수 없습니다! 씬에 TeammateManager를 추가했는지 확인하세요.");
            return;
        }

        if (monsterManager == null)
        {
            Debug.LogError("MonsterManager를 찾을 수 없습니다! 씬에 MonsterManager를 추가했는지 확인하세요.");
            return;
        }
        // Teammates와 Monster 초기화
        InitializeBattleTeammates();
        InitializeBattleMonster();

    }





    private void InitializeBattleTeammates()
    {
        battleTeammates = new List<Teammate>(teammateManager.teammates);
        Debug.Log($"Teammates initialized: {battleTeammates.Count}명");
        foreach (var teammate in battleTeammates)
        {
            Debug.Log($"동료: {teammate.teammateName}, HP: {teammate.maxHP}, 공격력: {teammate.attackPower}");
        }
    }

    private void InitializeBattleMonster()
    {
        if (monsterManager == null)
        {
            Debug.LogError("monsterManager가 null입니다!");
            return;
        }

        if (monsterManager.currentMonster == null)
        {
            Debug.LogError("monsterManager의 currentMonster가 null입니다!");
            return;
        }

        Monster battleMonster = monsterManager.currentMonster;

        if (battleMonster != null)
        {
            Debug.Log($"배틀에 등장한 몬스터: {battleMonster.MonsterName}");
            Debug.Log($"HP: {battleMonster.maxHP}, 공격력: {battleMonster.attackPower}, 스킬 개수: {battleMonster.skills.Count}");
        }
        else
        {
            Debug.LogWarning("현재 배틀에 사용할 몬스터가 없습니다!");
        }
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
    private void PrintBattleMonster()
    {


        Debug.Log($"배틀에 참여한 몬스터: {battleMonster.MonsterName}");

    }

    void Update()
    {
        
    }
}
