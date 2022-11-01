using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardian : UnitInfo
{
    // Start is called before the first frame update
    void Awake()
    {
        unitClass = "Guardian";
        if (unitLevel == 0)
        {
            unitLevel = 1;
        }
        unitMaxHealth = unitLevel + 21;
        unitHealth = unitMaxHealth;
        unitMovementSpeed = unitLevel * .15f - .05f;
        unitAttackRange = unitLevel * .15f - .05f;
        unitAttack = Mathf.RoundToInt(unitLevel * 1.15f + 5);
        unitCritChance = Mathf.RoundToInt(unitLevel * .15f + 4);
        unitSkillResist = Mathf.RoundToInt(unitLevel * .5f + 5);
        unitDefense = Mathf.RoundToInt(unitLevel * 1.15f + 5);
        unitEnergy = Mathf.RoundToInt(unitLevel * .75f + 2);
        unitEvasion = Mathf.RoundToInt(unitLevel * .3f + 2);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
