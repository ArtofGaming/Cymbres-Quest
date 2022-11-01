using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : UnitInfo
{
    // Start is called before the first frame update
    void Awake()
    {
        unitClass = "Hunter";
        if (unitLevel == 0)
        {
            unitLevel = 1;
        }
        unitMaxHealth = unitLevel + 1 + 15;
        unitHealth = unitMaxHealth;
        unitMovementSpeed = unitLevel * .15f - .05f;
        unitAttackRange = unitLevel * .15f - .05f;
        unitAttack = Mathf.RoundToInt(unitLevel * 1.15f + 6);
        unitCritChance = Mathf.RoundToInt(unitLevel * .15f + 4);
        unitSkillResist = Mathf.RoundToInt(unitLevel * .5f + 4);
        unitDefense = Mathf.RoundToInt(unitLevel * 1.15f + 4);
        unitEnergy = Mathf.RoundToInt(unitLevel * .75f + 4);
        unitEvasion = Mathf.RoundToInt(unitLevel * .3f + 5);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
