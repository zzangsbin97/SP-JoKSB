using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill 
{
    public string skillName;
    public double attackDamage;
    public double defensePercent;
    public int buffConst;

    public Skill(string skillName, double attackPercent = 0, double defensePercent = 0.00, int buffConst = 0) {
        // 스킬명은 무조건 존재해야 하며 기본 공격력, 방어력, 버프는 0으로 초기화됨
        // 왜냐면 필요한 값만 넣어서 쓰라고...
        this.skillName = skillName;
        this.attackDamage = attackPercent;
        this.defensePercent = defensePercent;
        this.buffConst = buffConst;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
