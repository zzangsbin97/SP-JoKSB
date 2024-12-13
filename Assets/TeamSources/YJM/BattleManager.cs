    using System.Collections.Generic;
    using UnityEngine;
    using System;
    using UnityEngine.SceneManagement;

    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance { get; private set; }
        public TeammateManager teammateManager; // Unity Editor에서 연결 가능
        public Teammate teammates;
        public MonsterManager monsterManager; // Unity Editor에서 연결 가능
        public List<Teammate> battleTeammates = new List<Teammate>();
        public Monster battleMonster; // 배틀에 등장하는 몬스터
        public int turn;

        public GameObject teammateUIPrefab;
        public Transform uiContainer;

    

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

        void InitializeHpUI() {
		foreach (var teammate in battleTeammates) {
			GameObject uiElement = Instantiate(teammateUIPrefab, uiContainer);
			HpUI uiScript = uiElement.GetComponent<HpUI>();
			uiScript.Initialize(teammate);
		}
	}

        void Start()
        {

            InitializeHpUI();

            Screen.SetResolution(1080, 1920, true);
            Screen.SetResolution(Screen.width, (Screen.width * 16) / 9, true); // 화면 비율 고정
            teammateManager = FindObjectOfType<TeammateManager>();
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

            turn = 1;
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
            if (skill.skillName == "번개같은 이동")
            {
                teammate.standGauge -= skill.usingStandGauge;
                double Damage = RandomDamage(110, 140, 100);
                
                if (teammate.isDead) {
                    Damage = 0;
                 }

                battleMonster.currentHP -= Mathf.RoundToInt((float)Damage);
                Debug.Log($"{teammate.teammateName}의 현재 스탠드게이지는 {teammate.standGauge}입니다.");
                Debug.Log($"{teammate.teammateName}이(가) {skill.skillName}을(를) 사용했습니다!");
                Debug.Log($"몬스터 {battleMonster.MonsterName}에게 {Damage}의 데미지를 입혔습니다!");
                Debug.Log($"몬스터 남은 체력: {battleMonster.currentHP}/{battleMonster.maxHP}");

                if (battleMonster.currentHP <= 0) {
                    Debug.Log($"몬스터 {battleMonster.MonsterName}를 쓰러뜨렸습니다. 배틀을 종료합니다.");
                    EndBattleAndReturnToTilemap();

                }

            }
            if (skill.skillName == "강력한 펀치")
            {
                teammate.standGauge -= skill.usingStandGauge;
                double Damage = RandomDamage(180, 220, 100); 
            
                if (teammate.isDead) {
				    Damage = 0;
			    }

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
                if (battleMonster.currentHP <= 0)
                {
                    Debug.Log($"몬스터 {battleMonster.MonsterName}를 쓰러뜨렸습니다. 배틀을 종료합니다.");
                    EndBattleAndReturnToTilemap();

                }
            }
            if (skill.skillName == "스타 플래티넘 러쉬")
            {
                teammate.standGauge -= skill.usingStandGauge;
                double Damage = RandomDamage(430, 470, 100);

			    if (teammate.isDead) {
				    Damage = 0;
			    }

			    battleMonster.currentHP -= Mathf.RoundToInt((float)Damage);
                Debug.Log($"{teammate.teammateName}의 현재 스탠드게이지는 {teammate.standGauge}입니다.");
                Debug.Log($"{teammate.teammateName}이(가) {skill.skillName}을(를) 사용했습니다!");
                Debug.Log($"몬스터 {battleMonster.MonsterName}에게 {Damage}의 데미지를 입혔습니다!");
                Debug.Log($"몬스터 남은 체력: {battleMonster.currentHP}/{battleMonster.maxHP}");
                if (battleMonster.currentHP <= 0)
                {
                    Debug.Log($"몬스터 {battleMonster.MonsterName}를 쓰러뜨렸습니다. 배틀을 종료합니다.");
                    EndBattleAndReturnToTilemap();

                }
            }
            if (skill.skillName == "화염구")
            {
                teammate.standGauge -= skill.usingStandGauge;
                double Damage = RandomDamage(90, 120, 120);

                if (teammate.isDead) {
				    Damage = 0;
			    }

                battleMonster.currentHP -= Mathf.RoundToInt((float)Damage);
                Debug.Log($"{teammate.teammateName}의 현재 스탠드게이지는 {teammate.standGauge}입니다.");
                Debug.Log($"{teammate.teammateName}이(가) {skill.skillName}을(를) 사용했습니다!");
                Debug.Log($"몬스터 {battleMonster.MonsterName}에게 {Damage}의 데미지를 입혔습니다!");
                Debug.Log($"몬스터 남은 체력: {battleMonster.currentHP}/{battleMonster.maxHP}");
                if (battleMonster.currentHP <= 0)
                {
                    Debug.Log($"몬스터 {battleMonster.MonsterName}를 쓰러뜨렸습니다. 배틀을 종료합니다.");
                    EndBattleAndReturnToTilemap();

                }
            }
            if (skill.skillName == "불꽃의 일격")
            {
                teammate.standGauge -= skill.usingStandGauge;
                double Damage = RandomDamage(230, 250, 120);

                if (teammate.isDead) {
				    Damage = 0;
			    }

                battleMonster.currentHP -= Mathf.RoundToInt((float)Damage);
                Debug.Log($"{teammate.teammateName}의 현재 스탠드게이지는 {teammate.standGauge}입니다.");
                Debug.Log($"{teammate.teammateName}이(가) {skill.skillName}을(를) 사용했습니다!");
                Debug.Log($"몬스터 {battleMonster.MonsterName}에게 {Damage}의 데미지를 입혔습니다!");
                Debug.Log($"몬스터 남은 체력: {battleMonster.currentHP}/{battleMonster.maxHP}");
                if (battleMonster.currentHP <= 0)
                {
                    Debug.Log($"몬스터 {battleMonster.MonsterName}를 쓰러뜨렸습니다. 배틀을 종료합니다.");
                    EndBattleAndReturnToTilemap();

                }
            }
            if (skill.skillName == "지옥의 불꽃")
            {
                teammate.standGauge -= skill.usingStandGauge;
                double Damage = RandomDamage(490, 510, 120);

                 if (teammate.isDead) {
				    Damage = 0;
		         }
            
                battleMonster.currentHP -= Mathf.RoundToInt((float)Damage);
                Debug.Log($"{teammate.teammateName}의 현재 스탠드게이지는 {teammate.standGauge}입니다.");
                Debug.Log($"{teammate.teammateName}이(가) {skill.skillName}을(를) 사용했습니다!");
                Debug.Log($"몬스터 {battleMonster.MonsterName}에게 {Damage}의 데미지를 입혔습니다!");
                Debug.Log($"몬스터 남은 체력: {battleMonster.currentHP}/{battleMonster.maxHP}");
                if (battleMonster.currentHP <= 0)
                {
                    Debug.Log($"몬스터 {battleMonster.MonsterName}를 쓰러뜨렸습니다. 배틀을 종료합니다.");
                    EndBattleAndReturnToTilemap();

                }
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
                if (battleMonster.currentHP <= 0)
                {
                    Debug.Log($"몬스터 {battleMonster.MonsterName}를 쓰러뜨렸습니다. 배틀을 종료합니다.");
                    EndBattleAndReturnToTilemap();

                }
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
                if (battleMonster.currentHP <= 0)
                {
                    Debug.Log($"몬스터 {battleMonster.MonsterName}를 쓰러뜨렸습니다. 배틀을 종료합니다.");
                    EndBattleAndReturnToTilemap();

                }
            }
            if (skill.skillName == "대지의 분노")
            {
                teammate.standGauge -= skill.usingStandGauge;
                double Damage = RandomDamage(280, 310, 90);

                if (teammate.isDead) {
				    Damage = 0;
			    }

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
                if (battleMonster.currentHP <= 0)
                {
                    Debug.Log($"몬스터 {battleMonster.MonsterName}를 쓰러뜨렸습니다. 배틀을 종료합니다.");
                    EndBattleAndReturnToTilemap();
                }
            }
            if (skill.skillName == "전기 충격")
            {
                teammate.standGauge -= skill.usingStandGauge;
                double Damage = RandomDamage(120, 120, 80);

               if (teammate.isDead) {
				    Damage = 0;
			   }
            
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
                if (battleMonster.currentHP <= 0)
                {
                    Debug.Log($"몬스터 {battleMonster.MonsterName}를 쓰러뜨렸습니다. 배틀을 종료합니다.");
                    EndBattleAndReturnToTilemap();
                }
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

                if (teammate.isDead) {
				    Damage = 0;
			    }

                battleMonster.currentHP -= Mathf.RoundToInt((float)Damage);
                Debug.Log($"{teammate.teammateName}의 현재 스탠드게이지는 {teammate.standGauge}입니다.");
                Debug.Log($"{teammate.teammateName}이(가) {skill.skillName}을(를) 사용했습니다!");
                Debug.Log($"몬스터 {battleMonster.MonsterName}에게 {Damage}의 데미지를 입혔습니다!");
                Debug.Log($"몬스터 남은 체력: {battleMonster.currentHP}/{battleMonster.maxHP}");
                if (battleMonster.currentHP <= 0)
                {
                    Debug.Log($"몬스터 {battleMonster.MonsterName}를 쓰러뜨렸습니다. 배틀을 종료합니다.");
                    EndBattleAndReturnToTilemap();

                }
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

                if (teammate.isDead) {
				    Damage = 0;
			    }

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

        public void MonsterSkillUse(Monster monster, Skill skill)
        {
            if (skill.skillName == "강렬하게 휘두르기")
            {
                Debug.Log($"{monster.MonsterName}이 {skill.skillName}을 사용했습니다.");
                monster.standGauge -= skill.usingStandGauge;
                double Damage = RandomDamage(100, 120, 40);
                System.Random rand = new System.Random();
                int randTeammate = rand.Next(0, battleTeammates.Count - 1);
                battleTeammates[randTeammate].currentHP -= Mathf.RoundToInt((float)Damage);
                Debug.Log($"{battleTeammates[randTeammate].teammateName}의 현재 체력은 {battleTeammates[randTeammate].currentHP}입니다.");
            }

            if (skill.skillName == "공포의 외침")
            {
                Debug.Log($"{monster.MonsterName}이 {skill.skillName}을 사용했습니다.");
                monster.standGauge -= skill.usingStandGauge;

                foreach (Teammate battleteammate in battleTeammates)
                {
                    double decrease = battleteammate.attackPercent * 0.3;
                    battleteammate.attackPercent -= Mathf.RoundToInt((float)decrease);
                    Debug.Log($"{battleteammate.teammateName}의 공격력이 {battleteammate.attackPercent}입니다.");
                }
            }
            if (skill.skillName == "방해의 광선")
            {
                Debug.Log($"{monster.MonsterName}이 {skill.skillName}을 사용했습니다.");
                monster.standGauge -= skill.usingStandGauge;

                double Damage = RandomDamage(80, 80, 100);
                System.Random rand = new System.Random();
                int randTeammate = rand.Next(0, battleTeammates.Count - 1);
                battleTeammates[randTeammate].currentHP -= Mathf.RoundToInt((float)Damage);
                battleTeammates[randTeammate].defensePercentTeammate -= 5;
                Debug.Log($"{battleTeammates[randTeammate].teammateName}의 현재 체력은 {battleTeammates[randTeammate].currentHP}입니다.");
            }
            if (skill.skillName == "고속 돌진")
            {
                Debug.Log($"{monster.MonsterName}이 {skill.skillName}을 사용했습니다.");
                monster.standGauge -= skill.usingStandGauge;
                double Damage = RandomDamage(180, 200, 100);
                System.Random rand = new System.Random();
                int randTeammate = rand.Next(0, battleTeammates.Count - 1);
                battleTeammates[randTeammate].currentHP -= Mathf.RoundToInt((float)Damage);
                Debug.Log($"{battleTeammates[randTeammate].teammateName}의 현재 체력은 {battleTeammates[randTeammate].currentHP}입니다.");
            }
            if (skill.skillName == "스텔스 어택")
            {
                Debug.Log($"{monster.MonsterName}이 {skill.skillName}을 사용했습니다.");
                monster.standGauge -= skill.usingStandGauge;
                double Damage = RandomDamage(120, 150, 100);
                System.Random rand = new System.Random();
                foreach (Teammate battleteammate in battleTeammates)
                {
                    battleteammate.currentHP -= Mathf.RoundToInt((float)Damage);
                    Debug.Log($"{battleteammate.teammateName}의 현재 체력은 {battleteammate.currentHP}입니다.");
                }
            }

            if (skill.skillName == "충격파")
            {
                Debug.Log($"{monster.MonsterName}이 {skill.skillName}을 사용했습니다.");
                monster.standGauge -= skill.usingStandGauge;
                double Damage = RandomDamage(150, 150, 80);
                System.Random rand = new System.Random();
                int randTeammate = rand.Next(0, battleTeammates.Count - 1);
                battleTeammates[randTeammate].currentHP -= Mathf.RoundToInt((float)Damage);
                Debug.Log($"{battleTeammates[randTeammate].teammateName}의 현재 체력은 {battleTeammates[randTeammate].currentHP}입니다.");
            }

            if (skill.skillName == "광역 방해")
            {
                Debug.Log($"{monster.MonsterName}이 {skill.skillName}을 사용했습니다.");
                monster.standGauge -= skill.usingStandGauge;
                foreach (Teammate battleteammate in battleTeammates)
                {
                    double decrease = battleteammate.attackPercent * 0.2;
                    battleteammate.attackPercent -= Mathf.RoundToInt((float)decrease);
                    battleteammate.standGauge -= 10;
                    Debug.Log($"{battleteammate.teammateName}의 공격력이 {battleteammate.attackPercent}입니다.");
                    Debug.Log($"{battleteammate.teammateName}의 스탠드게이지가 {battleteammate.standGauge}입니다.");
                }
            }
            if (skill.skillName == "대폭발")
            {
                Debug.Log($"{monster.MonsterName}이 {skill.skillName}을 사용했습니다.");
                monster.standGauge -= skill.usingStandGauge;
                double Damage = RandomDamage(170, 200, 80);
                System.Random rand = new System.Random();
                foreach (Teammate battleteammate in battleTeammates)
                {
                    battleteammate.currentHP -= Mathf.RoundToInt((float)Damage);
                    Debug.Log($"{battleteammate.teammateName}의 현재 체력은 {battleteammate.currentHP}입니다.");
                }
            }

        }



        public void InitializeBattleTeammates()
        {
            // TeammateManager를 통해 동료 데이터를 가져옴
            if (teammateManager == null)
            {
                teammateManager = TeammateManager.Instance; // 싱글톤 참조
                if (teammateManager == null)
                {
                    Debug.LogError("TeammateManager를 찾을 수 없습니다!");
                }
            }

            if (teammateManager.teammates == null || teammateManager.teammates.Count == 0)
            {
                Debug.LogError("BattleManager: TeammateManager에 동료 데이터가 없습니다!");
                return;
            }

            // 기존 데이터를 초기화
            battleTeammates.Clear();

            // TeammateManager의 데이터를 복사
            //battleTeammates.AddRange(teammateManager.teammates);
            battleTeammates = new List<Teammate>(teammateManager.teammates);

            if (battleTeammates.Count == 0)
            {
                Debug.LogWarning("BattleManager: battleTeammates에 추가된 동료가 없습니다.");
                return;
            }

            if (battleTeammates.Count > 0)
            {
                Debug.Log("BattleManager: 이미 동료 데이터를 초기화했습니다.");
                return; // 중복 초기화 방지
            }

            // 디버그 메시지로 동료 정보 출력
            Debug.Log($"BattleManager: {battleTeammates.Count}명의 동료가 초기화되었습니다.");
            foreach (Teammate teammate in battleTeammates)
            {
                Debug.Log($"BattleManager: 동료 이름 - {teammate.teammateName}, 체력: {teammate.currentHP}, 공격력: {teammate.attackPercent}, 스탠드게이지 : {teammate.standGauge}");
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

        public double RandomDamage(int startPercent, int endPercent, int baseAttackPower)
        {
            System.Random rand = new System.Random();
            double randPercent = ((float) rand.Next(startPercent, endPercent)) / 100;

            return randPercent * ((double) baseAttackPower);

        }

        public int GetExpectedActionsCount()
        {
            return battleTeammates.Count; // 예: 팀원 수 + 몬스터 1개
        }
        public void EndTurn()
        {
            turn++;
            Debug.Log($"턴 {turn}이(가) 시작되었습니다.");
            UpdateBattleState();
        }
        public void EndBattleAndReturnToTilemap()
        {
            foreach (Teammate battleteammate in battleTeammates)
            {
                battleteammate.usedSkill = false;
                battleteammate.standGauge += 20;
                foreach(Teammate teammates in teammateManager.teammates)
                {
                    if(battleteammate.teammateName == teammates.name)
                    {
                        teammates.standGauge = battleteammate.standGauge;
                        teammates.currentHP = battleteammate.currentHP;
                    }
                }
            
            }
            if (teammateManager != null)
            {
                Debug.Log("BattleManager: TeammateManager에 현재 팀 데이터를 업데이트합니다.");
                teammateManager.UpdateTeammates(battleTeammates);
            }
            else
            {
                Debug.LogError("BattleManager: TeammateManager가 null입니다. 데이터 업데이트 실패.");
            }
        

            // Tilemap 씬으로 이동
            UnityEngine.SceneManagement.SceneManager.LoadScene("Tilemap");
        }
        private void UpdateBattleState()
        {

            foreach (Teammate battleteammate in battleTeammates)
            {
                battleteammate.usedSkill = false;
                battleteammate.standGauge += 20;

                if (battleteammate.currentHP <= 0){
                    battleteammate.isDead = true;
                }
            }


            battleMonster.usedSkill = false;

            // 추가 상태 업데이트 로직 작성
            Debug.Log("배틀 상태가 업데이트되었습니다.");
        }
        void Update()
        {

        }
    }
