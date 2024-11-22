using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public Monster currentMonster; // 현재 등장하는 몬스터

    // Start is called before the first frame update
    void Start()
    {
        GameObject MonsterObject = new GameObject("김규석");
        Monster monster = MonsterObject.AddComponent<Monster>();

        monster.InitializeMonster("김규석");

        currentMonster = monster;
        Debug.Log($"배틀에 등장한 몬스터: {currentMonster.MonsterName}");
        Debug.Log($"HP: {currentMonster.maxHP}, 공격력: {currentMonster.attackPower}, 스킬 개수: {currentMonster.skills.Count}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Awake()
    {


        DontDestroyOnLoad(gameObject);


    }
}
