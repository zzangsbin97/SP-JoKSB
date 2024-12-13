    using System.Collections.Generic;
    using UnityEngine;
    using System;
    using UnityEngine.SceneManagement;

    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance { get; private set; }
        public TeammateManager teammateManager; // Unity Editor���� ���� ����
        public Teammate teammates;
        public MonsterManager monsterManager; // Unity Editor���� ���� ����
        public List<Teammate> battleTeammates = new List<Teammate>();
        public Monster battleMonster; // ��Ʋ�� �����ϴ� ����
        public int turn;

        public GameObject teammateUIPrefab;
        public Transform uiContainer;

    

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // �̱��� ����
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
            Screen.SetResolution(Screen.width, (Screen.width * 16) / 9, true); // ȭ�� ���� ����
            teammateManager = FindObjectOfType<TeammateManager>();
            // TeammateManager�� ������� �ʾҴٸ� FindObjectOfType�� ã��
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
                Debug.LogError("TeammateManager�� ã�� �� �����ϴ�! ���� TeammateManager�� �߰��ߴ��� Ȯ���ϼ���.");
                return;
            }

            if (monsterManager == null)
            {
                Debug.LogError("MonsterManager�� ã�� �� �����ϴ�! ���� MonsterManager�� �߰��ߴ��� Ȯ���ϼ���.");
                return;
            }
            // Teammates�� Monster �ʱ�ȭ
            InitializeBattleTeammates();
            InitializeBattleMonster();
            PrintBattleTeammates();

            turn = 1;
        }


        public void ApplySkillDamage(Teammate teammate, Skill skill)
        {
            if (battleMonster == null)
            {
                Debug.LogWarning("battleMonster�� null�Դϴ�. InitializeBattleMonster()�� �ٽ� ȣ���մϴ�.");
                InitializeBattleMonster();

                if (battleMonster == null)
                {
                    Debug.LogError("InitializeBattleMonster ȣ�� �Ŀ��� battleMonster�� null�Դϴ�!");
                    return;
                }
            }

            /*if (teammate == null)
            {
                Debug.LogError("Teammate null�Դϴ�!");
                return;
            }*/
            if (skill == null)
            {
                Debug.LogError("Skill�� null�Դϴ�!");
                return;
            }
            if (skill.skillName == "�������� �̵�")
            {
                teammate.standGauge -= skill.usingStandGauge;
                double Damage = RandomDamage(110, 140, 100);
                
                if (teammate.isDead) {
                    Damage = 0;
                 }

                battleMonster.currentHP -= Mathf.RoundToInt((float)Damage);
                Debug.Log($"{teammate.teammateName}�� ���� ���ĵ�������� {teammate.standGauge}�Դϴ�.");
                Debug.Log($"{teammate.teammateName}��(��) {skill.skillName}��(��) ����߽��ϴ�!");
                Debug.Log($"���� {battleMonster.MonsterName}���� {Damage}�� �������� �������ϴ�!");
                Debug.Log($"���� ���� ü��: {battleMonster.currentHP}/{battleMonster.maxHP}");

                if (battleMonster.currentHP <= 0) {
                    Debug.Log($"���� {battleMonster.MonsterName}�� �����߷Ƚ��ϴ�. ��Ʋ�� �����մϴ�.");
                    EndBattleAndReturnToTilemap();

                }

            }
            if (skill.skillName == "������ ��ġ")
            {
                teammate.standGauge -= skill.usingStandGauge;
                double Damage = RandomDamage(180, 220, 100); 
            
                if (teammate.isDead) {
				    Damage = 0;
			    }

			    battleMonster.currentHP -= Mathf.RoundToInt((float)Damage);
                //���� ���� �ʿ� 
                //�� ���� �ڵ� �ʿ�
                battleMonster.stun = true;
                Debug.Log($"{teammate.teammateName}�� ���� ���ĵ�������� {teammate.standGauge}�Դϴ�.");
                Debug.Log($"{teammate.teammateName}��(��) {skill.skillName}��(��) ����߽��ϴ�!");
                if (battleMonster.stun)
                {
                    Debug.Log($"���� {battleMonster.MonsterName}�� �����߽��ϴ�!");
                }
                Debug.Log($"���� {battleMonster.MonsterName}���� {Damage}�� �������� �������ϴ�!");
                Debug.Log($"���� ���� ü��: {battleMonster.currentHP}/{battleMonster.maxHP}");
                if (battleMonster.currentHP <= 0)
                {
                    Debug.Log($"���� {battleMonster.MonsterName}�� �����߷Ƚ��ϴ�. ��Ʋ�� �����մϴ�.");
                    EndBattleAndReturnToTilemap();

                }
            }
            if (skill.skillName == "��Ÿ �÷�Ƽ�� ����")
            {
                teammate.standGauge -= skill.usingStandGauge;
                double Damage = RandomDamage(430, 470, 100);

			    if (teammate.isDead) {
				    Damage = 0;
			    }

			    battleMonster.currentHP -= Mathf.RoundToInt((float)Damage);
                Debug.Log($"{teammate.teammateName}�� ���� ���ĵ�������� {teammate.standGauge}�Դϴ�.");
                Debug.Log($"{teammate.teammateName}��(��) {skill.skillName}��(��) ����߽��ϴ�!");
                Debug.Log($"���� {battleMonster.MonsterName}���� {Damage}�� �������� �������ϴ�!");
                Debug.Log($"���� ���� ü��: {battleMonster.currentHP}/{battleMonster.maxHP}");
                if (battleMonster.currentHP <= 0)
                {
                    Debug.Log($"���� {battleMonster.MonsterName}�� �����߷Ƚ��ϴ�. ��Ʋ�� �����մϴ�.");
                    EndBattleAndReturnToTilemap();

                }
            }
            if (skill.skillName == "ȭ����")
            {
                teammate.standGauge -= skill.usingStandGauge;
                double Damage = RandomDamage(90, 120, 120);

                if (teammate.isDead) {
				    Damage = 0;
			    }

                battleMonster.currentHP -= Mathf.RoundToInt((float)Damage);
                Debug.Log($"{teammate.teammateName}�� ���� ���ĵ�������� {teammate.standGauge}�Դϴ�.");
                Debug.Log($"{teammate.teammateName}��(��) {skill.skillName}��(��) ����߽��ϴ�!");
                Debug.Log($"���� {battleMonster.MonsterName}���� {Damage}�� �������� �������ϴ�!");
                Debug.Log($"���� ���� ü��: {battleMonster.currentHP}/{battleMonster.maxHP}");
                if (battleMonster.currentHP <= 0)
                {
                    Debug.Log($"���� {battleMonster.MonsterName}�� �����߷Ƚ��ϴ�. ��Ʋ�� �����մϴ�.");
                    EndBattleAndReturnToTilemap();

                }
            }
            if (skill.skillName == "�Ҳ��� �ϰ�")
            {
                teammate.standGauge -= skill.usingStandGauge;
                double Damage = RandomDamage(230, 250, 120);

                if (teammate.isDead) {
				    Damage = 0;
			    }

                battleMonster.currentHP -= Mathf.RoundToInt((float)Damage);
                Debug.Log($"{teammate.teammateName}�� ���� ���ĵ�������� {teammate.standGauge}�Դϴ�.");
                Debug.Log($"{teammate.teammateName}��(��) {skill.skillName}��(��) ����߽��ϴ�!");
                Debug.Log($"���� {battleMonster.MonsterName}���� {Damage}�� �������� �������ϴ�!");
                Debug.Log($"���� ���� ü��: {battleMonster.currentHP}/{battleMonster.maxHP}");
                if (battleMonster.currentHP <= 0)
                {
                    Debug.Log($"���� {battleMonster.MonsterName}�� �����߷Ƚ��ϴ�. ��Ʋ�� �����մϴ�.");
                    EndBattleAndReturnToTilemap();

                }
            }
            if (skill.skillName == "������ �Ҳ�")
            {
                teammate.standGauge -= skill.usingStandGauge;
                double Damage = RandomDamage(490, 510, 120);

                 if (teammate.isDead) {
				    Damage = 0;
		         }
            
                battleMonster.currentHP -= Mathf.RoundToInt((float)Damage);
                Debug.Log($"{teammate.teammateName}�� ���� ���ĵ�������� {teammate.standGauge}�Դϴ�.");
                Debug.Log($"{teammate.teammateName}��(��) {skill.skillName}��(��) ����߽��ϴ�!");
                Debug.Log($"���� {battleMonster.MonsterName}���� {Damage}�� �������� �������ϴ�!");
                Debug.Log($"���� ���� ü��: {battleMonster.currentHP}/{battleMonster.maxHP}");
                if (battleMonster.currentHP <= 0)
                {
                    Debug.Log($"���� {battleMonster.MonsterName}�� �����߷Ƚ��ϴ�. ��Ʋ�� �����մϴ�.");
                    EndBattleAndReturnToTilemap();

                }
            }
            if (skill.skillName == "������ ����")
            {
                teammate.standGauge -= skill.usingStandGauge;
                teammate.defensePercentTeammate += 20;
                //���� ���� �ʿ�
                //�� ���� �ڵ� �ʿ�
                Debug.Log($"{teammate.teammateName}�� ���� ���ĵ�������� {teammate.standGauge}�Դϴ�.");
                Debug.Log($"{teammate.teammateName}��(��) {skill.skillName}��(��) ����߽��ϴ�!");
                Debug.Log($"{teammate.teammateName}��(��) {teammate.defensePercentTeammate}�� ������ �����ϴ�.");
                if (battleMonster.currentHP <= 0)
                {
                    Debug.Log($"���� {battleMonster.MonsterName}�� �����߷Ƚ��ϴ�. ��Ʋ�� �����մϴ�.");
                    EndBattleAndReturnToTilemap();

                }
            }
            if (skill.skillName == "������ ����")
            {
                teammate.standGauge -= skill.usingStandGauge;
                double health = teammate.maxHP * 0.15;
                teammate.currentHP += Mathf.RoundToInt((float)health);
                teammate.defensePercentTeammate += 20;
                //�� ���� �ڵ� �ʿ�
                Debug.Log($"{teammate.teammateName}�� ���� ���ĵ�������� {teammate.standGauge}�Դϴ�.");
                Debug.Log($"{teammate.teammateName}��(��) {skill.skillName}��(��) ����߽��ϴ�!");
                Debug.Log($"{teammate.teammateName}��(��) {health}��ŭ ȸ���߽��ϴ�!");
                Debug.Log($"{teammate.teammateName}��(��) {teammate.defensePercentTeammate}�� ������ �����ϴ�.");
                if (battleMonster.currentHP <= 0)
                {
                    Debug.Log($"���� {battleMonster.MonsterName}�� �����߷Ƚ��ϴ�. ��Ʋ�� �����մϴ�.");
                    EndBattleAndReturnToTilemap();

                }
            }
            if (skill.skillName == "������ �г�")
            {
                teammate.standGauge -= skill.usingStandGauge;
                double Damage = RandomDamage(280, 310, 90);

                if (teammate.isDead) {
				    Damage = 0;
			    }

                battleMonster.currentHP -= Mathf.RoundToInt((float)Damage);
                battleMonster.stun = true;
                //���� �ڵ� ���� �ʿ�
                //�� ���� �ڵ� �ʿ�
                Debug.Log($"{teammate.teammateName}�� ���� ���ĵ�������� {teammate.standGauge}�Դϴ�.");
                Debug.Log($"{teammate.teammateName}��(��) {skill.skillName}��(��) ����߽��ϴ�!");
                if (battleMonster.stun)
                {
                    Debug.Log($"���� {battleMonster.MonsterName}�� �����߽��ϴ�!");
                }
                Debug.Log($"���� {battleMonster.MonsterName}���� {Damage}�� �������� �������ϴ�!");
                Debug.Log($"���� ���� ü��: {battleMonster.currentHP}/{battleMonster.maxHP}");
                if (battleMonster.currentHP <= 0)
                {
                    Debug.Log($"���� {battleMonster.MonsterName}�� �����߷Ƚ��ϴ�. ��Ʋ�� �����մϴ�.");
                    EndBattleAndReturnToTilemap();
                }
            }
            if (skill.skillName == "���� ���")
            {
                teammate.standGauge -= skill.usingStandGauge;
                double Damage = RandomDamage(120, 120, 80);

               if (teammate.isDead) {
				    Damage = 0;
			   }
            
                battleMonster.currentHP -= Mathf.RoundToInt((float)Damage);
                //���� �ڵ� ���� �ʿ�
                //�� ���� �ڵ� �ʿ�

                battleMonster.stun = true;
                Debug.Log($"{teammate.teammateName}�� ���� ���ĵ�������� {teammate.standGauge}�Դϴ�.");
                Debug.Log($"{teammate.teammateName}��(��) {skill.skillName}��(��) ����߽��ϴ�!");
                if (battleMonster.stun)
                {
                    Debug.Log($"���� {battleMonster.MonsterName}�� �����߽��ϴ�!");
                }
                Debug.Log($"���� {battleMonster.MonsterName}���� {Damage}�� �������� �������ϴ�!");
                Debug.Log($"���� ���� ü��: {battleMonster.currentHP}/{battleMonster.maxHP}");
                if (battleMonster.currentHP <= 0)
                {
                    Debug.Log($"���� {battleMonster.MonsterName}�� �����߷Ƚ��ϴ�. ��Ʋ�� �����մϴ�.");
                    EndBattleAndReturnToTilemap();
                }
            }
            if (skill.skillName == "���� ��ȭ")
            {
                teammate.standGauge -= skill.usingStandGauge;
                foreach (Teammate battleteammate in battleTeammates)
                {
                    battleteammate.attackPercent *= 1.2;
                    battleteammate.defensePercentTeammate += 20;
                    Debug.Log($"{battleteammate.teammateName}�� ���ݷ��� {battleteammate.attackPercent}�Դϴ�.");
                    Debug.Log($"{battleteammate.teammateName}�� ������ {battleteammate.defensePercentTeammate}�Դϴ�.");
                }
                //�� ���� �ڵ� �ʿ�
                Debug.Log($"{teammate.teammateName}��(��) {skill.skillName}��(��) ����߽��ϴ�!");
            }
            if (skill.skillName == "õ���� ����")
            {
                teammate.standGauge -= skill.usingStandGauge;
                double Damage = RandomDamage(350, 400, 80);

                if (teammate.isDead) {
				    Damage = 0;
			    }

                battleMonster.currentHP -= Mathf.RoundToInt((float)Damage);
                Debug.Log($"{teammate.teammateName}�� ���� ���ĵ�������� {teammate.standGauge}�Դϴ�.");
                Debug.Log($"{teammate.teammateName}��(��) {skill.skillName}��(��) ����߽��ϴ�!");
                Debug.Log($"���� {battleMonster.MonsterName}���� {Damage}�� �������� �������ϴ�!");
                Debug.Log($"���� ���� ü��: {battleMonster.currentHP}/{battleMonster.maxHP}");
                if (battleMonster.currentHP <= 0)
                {
                    Debug.Log($"���� {battleMonster.MonsterName}�� �����߷Ƚ��ϴ�. ��Ʋ�� �����մϴ�.");
                    EndBattleAndReturnToTilemap();

                }
            }
            if (skill.skillName == "ġ���� �ٶ�")
            {
                teammate.standGauge -= skill.usingStandGauge;
                Debug.Log($"{teammate.teammateName}�� ���� ���ĵ�������� {teammate.standGauge}�Դϴ�.");
                Debug.Log($"{teammate.teammateName}��(��) {skill.skillName}��(��) ����߽��ϴ�!");
                foreach (Teammate battleteammate in battleTeammates)
                {
                    battleteammate.currentHP += 40;
                    Debug.Log($"{battleteammate.teammateName}�� ü���� {battleteammate.currentHP}�Դϴ�.");
                }


            }

            if (skill.skillName == "�ٶ��� �⵵")
            {
                //�� ���� �ڵ� �ʿ�
                teammate.standGauge -= skill.usingStandGauge;
                double Damage = RandomDamage(100, 120, 80);

                if (teammate.isDead) {
				    Damage = 0;
			    }

                battleMonster.currentHP -= Mathf.RoundToInt((float)Damage);
                double decrease = battleMonster.attackPower * 0.2;
                battleMonster.attackPower -= Mathf.RoundToInt((float)decrease);
                Debug.Log($"{teammate.teammateName}�� ���� ���ĵ�������� {teammate.standGauge}�Դϴ�.");
                Debug.Log($"{teammate.teammateName}��(��) {skill.skillName}��(��) ����߽��ϴ�!");
                Debug.Log($"���� {battleMonster.MonsterName}���� {Damage}�� �������� �������ϴ�!");
                Debug.Log($"���� {battleMonster.MonsterName}�� ���� ���ݷ��� {battleMonster.attackPower}�Դϴ�.");
                Debug.Log($"���� ���� ü��: {battleMonster.currentHP}/{battleMonster.maxHP}");
            }

            if (skill.skillName == "ȸ���� ��ǳ")
            {
                //�� ���� �ڵ� �ʿ�
                teammate.standGauge -= skill.usingStandGauge;
                foreach (Teammate battleteammate in battleTeammates)
                {
                    double health = teammate.maxHP * 0.35;
                    teammate.currentHP += Mathf.RoundToInt((float)health);
                    teammate.defensePercentTeammate += 15;
                    Debug.Log($"{battleteammate.teammateName}�� ������ {battleteammate.defensePercentTeammate}�Դϴ�.");
                }
                Debug.Log($"{teammate.teammateName}�� ���� ���ĵ�������� {teammate.standGauge}�Դϴ�.");
                Debug.Log($"{teammate.teammateName}��(��) {skill.skillName}��(��) ����߽��ϴ�!");

            }



            // ��ų ������ ���
            /*double damage = (skill.attackDamage * teammate.attackPercent);
            battleMonster.currentHP -= Mathf.RoundToInt((float)damage);

            Debug.Log($"{teammate.teammateName}��(��) {skill.skillName}��(��) ����߽��ϴ�!");
            Debug.Log($"���� {battleMonster.MonsterName}���� {damage}�� �������� �������ϴ�!");
            Debug.Log($"���� ���� ü��: {battleMonster.currentHP}/{battleMonster.maxHP}");
            if(battleMonster.currentHP <= 0)
            {
                Debug.Log($"{battleMonster.MonsterName}�� �׾����ϴ�.");
            }*/
        }

        public void MonsterSkillUse(Monster monster, Skill skill)
        {
            if (skill.skillName == "�����ϰ� �ֵθ���")
            {
                Debug.Log($"{monster.MonsterName}�� {skill.skillName}�� ����߽��ϴ�.");
                monster.standGauge -= skill.usingStandGauge;
                double Damage = RandomDamage(100, 120, 40);
                System.Random rand = new System.Random();
                int randTeammate = rand.Next(0, battleTeammates.Count - 1);
                battleTeammates[randTeammate].currentHP -= Mathf.RoundToInt((float)Damage);
                Debug.Log($"{battleTeammates[randTeammate].teammateName}�� ���� ü���� {battleTeammates[randTeammate].currentHP}�Դϴ�.");
            }

            if (skill.skillName == "������ ��ħ")
            {
                Debug.Log($"{monster.MonsterName}�� {skill.skillName}�� ����߽��ϴ�.");
                monster.standGauge -= skill.usingStandGauge;

                foreach (Teammate battleteammate in battleTeammates)
                {
                    double decrease = battleteammate.attackPercent * 0.3;
                    battleteammate.attackPercent -= Mathf.RoundToInt((float)decrease);
                    Debug.Log($"{battleteammate.teammateName}�� ���ݷ��� {battleteammate.attackPercent}�Դϴ�.");
                }
            }
            if (skill.skillName == "������ ����")
            {
                Debug.Log($"{monster.MonsterName}�� {skill.skillName}�� ����߽��ϴ�.");
                monster.standGauge -= skill.usingStandGauge;

                double Damage = RandomDamage(80, 80, 100);
                System.Random rand = new System.Random();
                int randTeammate = rand.Next(0, battleTeammates.Count - 1);
                battleTeammates[randTeammate].currentHP -= Mathf.RoundToInt((float)Damage);
                battleTeammates[randTeammate].defensePercentTeammate -= 5;
                Debug.Log($"{battleTeammates[randTeammate].teammateName}�� ���� ü���� {battleTeammates[randTeammate].currentHP}�Դϴ�.");
            }
            if (skill.skillName == "��� ����")
            {
                Debug.Log($"{monster.MonsterName}�� {skill.skillName}�� ����߽��ϴ�.");
                monster.standGauge -= skill.usingStandGauge;
                double Damage = RandomDamage(180, 200, 100);
                System.Random rand = new System.Random();
                int randTeammate = rand.Next(0, battleTeammates.Count - 1);
                battleTeammates[randTeammate].currentHP -= Mathf.RoundToInt((float)Damage);
                Debug.Log($"{battleTeammates[randTeammate].teammateName}�� ���� ü���� {battleTeammates[randTeammate].currentHP}�Դϴ�.");
            }
            if (skill.skillName == "���ڽ� ����")
            {
                Debug.Log($"{monster.MonsterName}�� {skill.skillName}�� ����߽��ϴ�.");
                monster.standGauge -= skill.usingStandGauge;
                double Damage = RandomDamage(120, 150, 100);
                System.Random rand = new System.Random();
                foreach (Teammate battleteammate in battleTeammates)
                {
                    battleteammate.currentHP -= Mathf.RoundToInt((float)Damage);
                    Debug.Log($"{battleteammate.teammateName}�� ���� ü���� {battleteammate.currentHP}�Դϴ�.");
                }
            }

            if (skill.skillName == "�����")
            {
                Debug.Log($"{monster.MonsterName}�� {skill.skillName}�� ����߽��ϴ�.");
                monster.standGauge -= skill.usingStandGauge;
                double Damage = RandomDamage(150, 150, 80);
                System.Random rand = new System.Random();
                int randTeammate = rand.Next(0, battleTeammates.Count - 1);
                battleTeammates[randTeammate].currentHP -= Mathf.RoundToInt((float)Damage);
                Debug.Log($"{battleTeammates[randTeammate].teammateName}�� ���� ü���� {battleTeammates[randTeammate].currentHP}�Դϴ�.");
            }

            if (skill.skillName == "���� ����")
            {
                Debug.Log($"{monster.MonsterName}�� {skill.skillName}�� ����߽��ϴ�.");
                monster.standGauge -= skill.usingStandGauge;
                foreach (Teammate battleteammate in battleTeammates)
                {
                    double decrease = battleteammate.attackPercent * 0.2;
                    battleteammate.attackPercent -= Mathf.RoundToInt((float)decrease);
                    battleteammate.standGauge -= 10;
                    Debug.Log($"{battleteammate.teammateName}�� ���ݷ��� {battleteammate.attackPercent}�Դϴ�.");
                    Debug.Log($"{battleteammate.teammateName}�� ���ĵ�������� {battleteammate.standGauge}�Դϴ�.");
                }
            }
            if (skill.skillName == "������")
            {
                Debug.Log($"{monster.MonsterName}�� {skill.skillName}�� ����߽��ϴ�.");
                monster.standGauge -= skill.usingStandGauge;
                double Damage = RandomDamage(170, 200, 80);
                System.Random rand = new System.Random();
                foreach (Teammate battleteammate in battleTeammates)
                {
                    battleteammate.currentHP -= Mathf.RoundToInt((float)Damage);
                    Debug.Log($"{battleteammate.teammateName}�� ���� ü���� {battleteammate.currentHP}�Դϴ�.");
                }
            }

        }



        public void InitializeBattleTeammates()
        {
            // TeammateManager�� ���� ���� �����͸� ������
            if (teammateManager == null)
            {
                teammateManager = TeammateManager.Instance; // �̱��� ����
                if (teammateManager == null)
                {
                    Debug.LogError("TeammateManager�� ã�� �� �����ϴ�!");
                }
            }

            if (teammateManager.teammates == null || teammateManager.teammates.Count == 0)
            {
                Debug.LogError("BattleManager: TeammateManager�� ���� �����Ͱ� �����ϴ�!");
                return;
            }

            // ���� �����͸� �ʱ�ȭ
            battleTeammates.Clear();

            // TeammateManager�� �����͸� ����
            //battleTeammates.AddRange(teammateManager.teammates);
            battleTeammates = new List<Teammate>(teammateManager.teammates);

            if (battleTeammates.Count == 0)
            {
                Debug.LogWarning("BattleManager: battleTeammates�� �߰��� ���ᰡ �����ϴ�.");
                return;
            }

            if (battleTeammates.Count > 0)
            {
                Debug.Log("BattleManager: �̹� ���� �����͸� �ʱ�ȭ�߽��ϴ�.");
                return; // �ߺ� �ʱ�ȭ ����
            }

            // ����� �޽����� ���� ���� ���
            Debug.Log($"BattleManager: {battleTeammates.Count}���� ���ᰡ �ʱ�ȭ�Ǿ����ϴ�.");
            foreach (Teammate teammate in battleTeammates)
            {
                Debug.Log($"BattleManager: ���� �̸� - {teammate.teammateName}, ü��: {teammate.currentHP}, ���ݷ�: {teammate.attackPercent}, ���ĵ������ : {teammate.standGauge}");
            }
        }



        private void InitializeBattleMonster()
        {
            if (monsterManager == null)
            {
                Debug.LogError("monsterManager�� null�Դϴ�!");
                return;
            }

            if (monsterManager.currentMonster == null)
            {
                Debug.LogError("monsterManager�� currentMonster�� null�Դϴ�!");
                return;
            }

            battleMonster = monsterManager.currentMonster;

            if (battleMonster != null)
            {
                Debug.Log($"��Ʋ�� ������ ����: {battleMonster.MonsterName}");
                Debug.Log($"HP: {battleMonster.maxHP}, ���ݷ�: {battleMonster.attackPower}, ��ų ����: {battleMonster.skills.Count}");
            }
            else
            {
                Debug.LogWarning("���� ��Ʋ�� ����� ���Ͱ� �����ϴ�!");
            }
        }
        private void PrintBattleTeammates()
        {
            if (battleTeammates.Count == 0)
            {
                Debug.LogWarning("��Ʋ�� ������ ���ᰡ �����ϴ�!");
                return;
            }

            foreach (Teammate teammate in battleTeammates)
            {
                Debug.Log($"��Ʋ�� ������ ����: {teammate.teammateName}");
            }
        }
        private void PrintBattleMonster()
        {


            Debug.Log($"��Ʋ�� ������ ����: {battleMonster.MonsterName}");

        }

        public double RandomDamage(int startPercent, int endPercent, int baseAttackPower)
        {
            System.Random rand = new System.Random();
            double randPercent = ((float) rand.Next(startPercent, endPercent)) / 100;

            return randPercent * ((double) baseAttackPower);

        }

        public int GetExpectedActionsCount()
        {
            return battleTeammates.Count; // ��: ���� �� + ���� 1��
        }
        public void EndTurn()
        {
            turn++;
            Debug.Log($"�� {turn}��(��) ���۵Ǿ����ϴ�.");
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
                Debug.Log("BattleManager: TeammateManager�� ���� �� �����͸� ������Ʈ�մϴ�.");
                teammateManager.UpdateTeammates(battleTeammates);
            }
            else
            {
                Debug.LogError("BattleManager: TeammateManager�� null�Դϴ�. ������ ������Ʈ ����.");
            }
        

            // Tilemap ������ �̵�
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

            // �߰� ���� ������Ʈ ���� �ۼ�
            Debug.Log("��Ʋ ���°� ������Ʈ�Ǿ����ϴ�.");
        }
        void Update()
        {

        }
    }
