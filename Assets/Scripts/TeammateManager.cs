using System.Collections.Generic;
using UnityEngine;

public class TeammateManager : MonoBehaviour
{
    public List<Teammate> teammates = new List<Teammate>(); // 현재 팀에 있는 동료 목록

    void Start()
    {
        // 새로운 Teammate 생성
        GameObject teammateObject = new GameObject("kimsubin");
        Teammate kimsubin = teammateObject.AddComponent<Teammate>();

        // Teammate 초기화
        kimsubin.InitializeTeammate("kimsubin");

        // 팀 목록에 추가
        AddTeammate(kimsubin);

        Debug.Log($"이름: {kimsubin.teammateName}, 체력: {kimsubin.maxHP}, 공격력: {kimsubin.attackPower}, 스킬: {kimsubin.skills}");

        Debug.Log("Teammates in TeammateManager:");
        foreach (var teammate in teammates)
        {
            Debug.Log(teammate.teammateName);
        }

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
