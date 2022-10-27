using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySolider : UnitInfo
{
    // Start is called before the first frame update
    void Awake()
    {
        unitClass = "EnemySoldier";
        if (unitLevel == 0)
        {
            unitLevel = 1;
        }
        unitMaxHealth = unitLevel * 3 + 12;
        unitHealth = unitMaxHealth;
        unitMovementSpeed = .1f +(unitLevel * .01f);
        unitAttackRange = .1f;
        unitAttack = Mathf.RoundToInt(unitLevel * 1.15f + 7);
        unitCritChance = 0;
        unitSkillResist = Mathf.RoundToInt(unitLevel * .5f + 1);
        unitDefense = Mathf.RoundToInt(unitLevel * 1.15f + 2);
        unitEnergy = 0;
        unitEvasion = 0;

    }

    // Update is called once per frame
    void Update()
    {

    }
}