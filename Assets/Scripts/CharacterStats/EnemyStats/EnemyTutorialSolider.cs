using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTutorialSolider : UnitInfo
{
    // Start is called before the first frame update
    void Awake()
    {
        unitClass = "EnemyTutorialSoldier";
        if (unitLevel == 0)
        {
            unitLevel = 1;
        }
        unitMaxHealth = 20;
        unitHealth = unitMaxHealth;
        unitMovementSpeed = .11f;
        unitAttackRange = .1f;
        unitAttack = 7;
        unitCritChance = 0;
        unitSkillResist = 5;
        unitDefense = 5;
        unitEnergy = 0;
        unitEvasion = 0;

    }

    // Update is called once per frame
    void Update()
    {

    }
}