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
        unitMaxHealth = unitLevel * 3 + 12;
        unitHealth = unitMaxHealth;
        unitMovementSpeed = unitLevel * .15f - .05f;
        unitAttackRange = unitLevel * .15f - .05f;
        unitAttack = Mathf.RoundToInt(unitLevel * 1.15f + 7);
        unitCritChance = Mathf.RoundToInt(unitLevel * .15f + 4);
        unitSkillResist = Mathf.RoundToInt(unitLevel * .5f + 4);
        unitDefense = Mathf.RoundToInt(unitLevel * 1.15f + 5);
        unitEnergy = Mathf.RoundToInt(unitLevel * .75f + 2);
        unitEvasion = Mathf.RoundToInt(unitLevel * .3f + 3);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
