using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour
{
    #region "UnitInfo Vars"
    public string unitName;
    [HideInInspector]
    public int unitLevel;
    public int currentUnitExperience;
    public int maxUnitExperience;
    public string unitClass;
    public int unitAttack;
    public int unitDefense;
    public int unitHealth;
    public float unitMovementSpeed;
    public float unitAttackRange;
    public int unitEvasion;
    public int unitCritChance;
    public int unitMaxHealth;
    public string unitSkill;
    public string unitWeapon;
    public int unitEnergy;
    public int unitSkillResist;
    Customization customization;
    #endregion


    void Start()
    {
        customization = GameObject.Find("boss").GetComponent<Customization>();
    }


    void Update()
    {
        if (currentUnitExperience == maxUnitExperience)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        currentUnitExperience = 0;
        unitLevel += 1;
        maxUnitExperience = Mathf.RoundToInt(unitLevel * 1.35f);
    }
    public void IdentifyYourself()
    {
        customization.currentUnitClass = unitClass;
    }
}
