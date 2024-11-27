using System.Collections.Generic;
using UnityEngine;
using System;

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
        PrintBattleTeammates();


    }


    public void ApplySkillDamage(Teammate teammate, Skill skill)
    {
        if (battleMonster == null)
        {
            Debug.LogWarning("battleMonster가 null입니다. InitializeBattleMonster()를 다시 호출합니다.");
            InitializeBattleMonster();

            if (battleMonster == null)
            {
                Debug.LogError("InitializeBattleMonster 호출 후에도 battleMonster가 null입니다!");
                return;
            }
        }

        /*if (teammate == null)
        {
            Debug.LogError("Teammate null입니다!");
            return;
        }*/
        if (skill == null)
        {
            Debug.LogError("Skill이 null입니다!");
            return;
        }
        if(skill.skillName == "번개같은 이동")
        {
            teammate.standGauge -= skill.usingStandGauge;
            double Damage = RandomDamage(110, 140, 100);
            battleMonster.currentHP -= Mathf.RoundToInt((float)Damage);
            Debug.Log($"{teammate.teammateName}의 현재 스탠드게이지는 {teammate.standGauge}입니다.");
            Debug.Log($"{teammate.teammateName}이(가) {skill.skillName}을(를) 사용했습니다!");
            Debug.Log($"몬스터 {battleMonster.MonsterName}에게 {Damage}의 데미지를 입혔습니다!");
            Debug.Log($"몬스터 남은 체력: {battleMonster.currentHP}/{battleMonster.maxHP}");

        }
        if(skill.skillName == "강력한 펀치")
        {
            teammate.standGauge -= skill.usingStandGauge;
            double Damage = RandomDamage(180, 220, 100);
            battleMonster.currentHP -= Mathf.RoundToInt((float)Damage);
            //스턴 구현 필요 
            //턴 관리 코드 필요
            battleMonster.stun = true;
            Debug.Log($"{teammate.teammateName}의 현재 스탠드게이지는 {teammate.standGauge}입니다.");
            Debug.Log($"{teammate.teammateName}이(가) {skill.skillName}을(를) 사용했습니다!");
            if (battleMonster.stun)
            {
                Debug.Log($"몬스터 {battleMonster.MonsterName}가 기절했습니다!");
            }
            Debug.Log($"몬스터 {battleMonster.MonsterName}에게 {Damage}의 데미지를 입혔습니다!");
            Debug.Log($"몬스터 남은 체력: {battleMonster.currentHP}/{battleMonster.maxHP}");
        }
        if(skill.skillName == "스타 플래티넘 러쉬")
        {
            teammate.standGauge -= skill.usingStandGauge;
            double Damage = RandomDamage(430, 470, 100);
            battleMonster.currentHP -= Mathf.RoundToInt((float)Damage);
            Debug.Log($"{teammate.teammateName}의 현재 스탠드게이지는 {teammate.standGauge}입니다.");
            Debug.Log($"{teammate.teammateName}이(가) {skill.skillName}을(를) 사용했습니다!");
            Debug.Log($"몬스터 {battleMonster.MonsterName}에게 {Damage}의 데미지를 입혔습니다!");
            Debug.Log($"몬스터 남은 체력: {battleMonster.currentHP}/{battleMonster.maxHP}");
        }
        if(skill.skillName == "화염구")
        {
            teammate.standGauge -= skill.usingStandGauge;
            double Damage = RandomDamage(90, 120, 120);
            battleMonster.currentHP -= Mathf.RoundToInt((float)Damage);
            Debug.Log($"{teammate.teammateName}의 현재 스탠드게이지는 {teammate.standGauge}입니다.");
            Debug.Log($"{teammate.teammateName}이(가) {skill.skillName}을(를) 사용했습니다!");
            Debug.Log($"몬스터 {battleMonster.MonsterName}에게 {Damage}의 데미지를 입혔습니다!");
            Debug.Log($"몬스터 남은 체력: {battleMonster.currentHP}/{battleMonster.maxHP}");
        }
        if (skill.skillName == "불꽃의 일격")
        {
            teammate.standGauge -= skill.usingStandGauge;
            double Damage = RandomDamage(230, 250, 120);
            battleMonster.currentHP -= Mathf.RoundToInt((float)Damage);
            Debug.Log($"{teammate.teammateName}의 현재 스탠드게이지는 {teammate.standGauge}입니다.");
            Debug.Log($"{teammate.teammateName}이(가) {skill.skillName}을(를) 사용했습니다!");
            Debug.Log($"몬스터 {battleMonster.MonsterName}에게 {Damage}의 데미지를 입혔습니다!");
            Debug.Log($"몬스터 남은 체력: {battleMonster.currentHP}/{battleMonster.maxHP}");
        }
        if (skill.skillName == "지옥의 불꽃")
        {
            teammate.standGauge -= skill.usingStandGauge;
            double Damage = RandomDamage(490, 510, 120);
            battleMonster.currentHP -= Mathf.RoundToInt((float)Damage);
            Debug.Log($"{teammate.teammateName}의 현재 스탠드게이지는 {teammate.standGauge}입니다.");
            Debug.Log($"{teammate.teammateName}이(가) {skill.skillName}을(를) 사용했습니다!");
            Debug.Log($"몬스터 {battleMonster.MonsterName}에게 {Damage}의 데미지를 입혔습니다!");
            Debug.Log($"몬스터 남은 체력: {battleMonster.currentHP}/{battleMonster.maxHP}");
        }
        if (skill.skillName == "대지의 결의")
        {
            teammate.standGauge -= skill.usingStandGauge;
            teammate.defensePercentTeammate += 20;
            //도발 구현 필요
            //턴 관리 코드 필요
            Debug.Log($"{teammate.teammateName}의 현재 스탠드게이지는 {teammate.standGauge}입니다.");
            Debug.Log($"{teammate.teammateName}이(가) {skill.skillName}을(를) 사용했습니다!");
            Debug.Log($"{teammate.teammateName}이(가) {teammate.defensePercentTeammate}의 방어력을 가집니다.");
        }
        if (skill.skillName == "강인한 의지")
        {
            teammate.standGauge -= skill.usingStandGauge;
            double health = teammate.maxHP * 0.15;
            teammate.currentHP += Mathf.RoundToInt((float)health);
            teammate.defensePercentTeammate += 20;
            //턴 관리 코드 필요
            Debug.Log($"{teammate.teammateName}의 현재 스탠드게이지는 {teammate.standGauge}입니다.");
            Debug.Log($"{teammate.teammateName}이(가) {skill.skillName}을(를) 사용했습니다!");
            Debug.Log($"{teammate.teammateName}이(가) {health}만큼 회복했습니다!");
            Debug.Log($"{teammate.teammateName}이(가) {teammate.defensePercentTeammate}의 방어력을 가집니다.");
        }
        if (skill.skillName == "대지의 분노")
        {
            teammate.standGauge -= skill.usingStandGauge;
            double Damage = RandomDamage(280, 310, 90);
            battleMonster.currentHP -= Mathf.RoundToInt((float)Damage);
            battleMonster.stun = true;
            //기절 코드 구현 필요
            //턴 관리 코드 필요
            Debug.Log($"{teammate.teammateName}의 현재 스탠드게이지는 {teammate.standGauge}입니다.");
            Debug.Log($"{teammate.teammateName}이(가) {skill.skillName}을(를) 사용했습니다!");
            if (battleMonster.stun)
            {
                Debug.Log($"몬스터 {battleMonster.MonsterName}가 기절했습니다!");
            }
            Debug.Log($"몬스터 {battleMonster.MonsterName}에게 {Damage}의 데미지를 입혔습니다!");
            Debug.Log($"몬스터 남은 체력: {battleMonster.currentHP}/{battleMonster.maxHP}");
        }
        if (skill.skillName == "전기 충격")
        {
            teammate.standGauge -= skill.usingStandGauge;
            double Damage = RandomDamage(120, 120, 80);
            battleMonster.currentHP -= Mathf.RoundToInt((float)Damage);
            //기절 코드 구현 필요
            //턴 관리 코드 필요

            battleMonster.stun = true;
            Debug.Log($"{teammate.teammateName}의 현재 스탠드게이지는 {teammate.standGauge}입니다.");
            Debug.Log($"{teammate.teammateName}이(가) {skill.skillName}을(를) 사용했습니다!");
            if (battleMonster.stun)
            {
                Debug.Log($"몬스터 {battleMonster.MonsterName}가 기절했습니다!");
            }
            Debug.Log($"몬스터 {battleMonster.MonsterName}에게 {Damage}의 데미지를 입혔습니다!");
            Debug.Log($"몬스터 남은 체력: {battleMonster.currentHP}/{battleMonster.maxHP}");
        }
        if (skill.skillName == "전기 강화")
        {
            teammate.standGauge -= skill.usingStandGauge;
            foreach (Teammate battleteammate in battleTeammates)
            {
                battleteammate.attackPercent *= 1.2;
                battleteammate.defensePercentTeammate += 20;
                Debug.Log($"{battleteammate.teammateName}의 공격력이 {battleteammate.attackPercent}입니다.");
                Debug.Log($"{battleteammate.teammateName}의 방어력이 {battleteammate.defensePercentTeammate}입니다.");
            }
            //턴 관리 코드 필요
            Debug.Log($"{teammate.teammateName}이(가) {skill.skillName}을(를) 사용했습니다!");
        }
        if (skill.skillName == "천둥의 심판")
        {
            teammate.standGauge -= skill.usingStandGauge;
            double Damage = RandomDamage(350, 400, 80);
            battleMonster.currentHP -= Mathf.RoundToInt((float)Damage);
            Debug.Log($"{teammate.teammateName}의 현재 스탠드게이지는 {teammate.standGauge}입니다.");
            Debug.Log($"{teammate.teammateName}이(가) {skill.skillName}을(를) 사용했습니다!");
            Debug.Log($"몬스터 {battleMonster.MonsterName}에게 {Damage}의 데미지를 입혔습니다!");
            Debug.Log($"몬스터 남은 체력: {battleMonster.currentHP}/{battleMonster.maxHP}");
        }
        if (skill.skillName == "치유의 바람")
        {
            teammate.standGauge -= skill.usingStandGauge;
            Debug.Log($"{teammate.teammateName}의 현재 스탠드게이지는 {teammate.standGauge}입니다.");
            Debug.Log($"{teammate.teammateName}이(가) {skill.skillName}을(를) 사용했습니다!");
            foreach (Teammate battleteammate in battleTeammates)
            {
                battleteammate.currentHP += 40;
                Debug.Log($"{battleteammate.teammateName}의 체력이 {battleteammate.currentHP}입니다.");
            }
            

        }

        if (skill.skillName == "바람의 쇄도")
        {
            //턴 관리 코드 필요
            teammate.standGauge -= skill.usingStandGauge;
            double Damage = RandomDamage(100, 120, 80);
            battleMonster.currentHP -= Mathf.RoundToInt((float)Damage);
            double decrease = battleMonster.attackPower * 0.2;
            battleMonster.attackPower -= Mathf.RoundToInt((float)decrease);
            Debug.Log($"{teammate.teammateName}의 현재 스탠드게이지는 {teammate.standGauge}입니다.");
            Debug.Log($"{teammate.teammateName}이(가) {skill.skillName}을(를) 사용했습니다!");
            Debug.Log($"몬스터 {battleMonster.MonsterName}에게 {Damage}의 데미지를 입혔습니다!");
            Debug.Log($"몬스터 {battleMonster.MonsterName}의 현재 공격력은 {battleMonster.attackPower}입니다.");
            Debug.Log($"몬스터 남은 체력: {battleMonster.currentHP}/{battleMonster.maxHP}");
        }

        if (skill.skillName == "회복의 신풍")
        {
            //턴 관리 코드 필요
            teammate.standGauge -= skill.usingStandGauge;
            foreach (Teammate battleteammate in battleTeammates)
            {
                double health = teammate.maxHP * 0.35;
                teammate.currentHP += Mathf.RoundToInt((float)health);
                teammate.defensePercentTeammate += 15;
                Debug.Log($"{battleteammate.teammateName}의 방어력이 {battleteammate.defensePercentTeammate}입니다.");
            }
            Debug.Log($"{teammate.teammateName}의 현재 스탠드게이지는 {teammate.standGauge}입니다.");
            Debug.Log($"{teammate.teammateName}이(가) {skill.skillName}을(를) 사용했습니다!");

        }


        // 스킬 데미지 계산
        /*double damage = (skill.attackDamage * teammate.attackPercent);
        battleMonster.currentHP -= Mathf.RoundToInt((float)damage);

        Debug.Log($"{teammate.teammateName}이(가) {skill.skillName}을(를) 사용했습니다!");
        Debug.Log($"몬스터 {battleMonster.MonsterName}에게 {damage}의 데미지를 입혔습니다!");
        Debug.Log($"몬스터 남은 체력: {battleMonster.currentHP}/{battleMonster.maxHP}");
        if(battleMonster.currentHP <= 0)
        {
            Debug.Log($"{battleMonster.MonsterName}이 죽었습니다.");
        }*/
    }



    private void InitializeBattleTeammates()
    {
        battleTeammates = new List<Teammate>(teammateManager.teammates);
        Debug.Log($"Teammates initialized: {battleTeammates.Count}명");
        foreach (var teammate in battleTeammates)
        {
            Debug.Log($"동료: {teammate.teammateName}, HP: {teammate.maxHP}, 공격력: {teammate.attackPercent}");
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

        battleMonster = monsterManager.currentMonster;

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

    public double RandomDamage(int startPercent, int endPercent, int baseAttackPower) {
        System.Random rand = new System.Random();
        double randPercent = rand.Next(startPercent, endPercent) / 100;

        return randPercent * baseAttackPower;

    }

    void Update()
    {
        
    }
}
