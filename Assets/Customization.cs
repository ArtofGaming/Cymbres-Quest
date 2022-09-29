using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Customization : MonoBehaviour
{
    public GameObject currentUnit;
    GameObject lastUnit;
    public List<GameObject> units;
    public List<string> colorOptions;
    public TMP_Dropdown materialDropdown;
    public TMP_Dropdown colorDropdown;
    public Material selectedMaterial;
    public string currentUnitClass;
    UnitInfo currentUnitInfo;
    public Material lastMaterial;
    public GameManager gameManager;
    public test Test;
    public GameObject god;


    public TextMeshProUGUI unitNameText;
    public TextMeshProUGUI unitLevel;
    public TextMeshProUGUI unitClass;
    public TextMeshProUGUI unitAttack;
    public TextMeshProUGUI unitDefense;
    public TextMeshProUGUI unitHealth;
    public TextMeshProUGUI unitSpeed;
    public TextMeshProUGUI unitCrit;
    public TextMeshProUGUI unitDebuffResist;
    public TextMeshProUGUI unitEvasion;
    public TextMeshProUGUI unitEnergy;
    public TextMeshProUGUI unitMovementSpeed;
    public TextMeshProUGUI unitAttackRange;

    TMP_Dropdown.OptionData bodyOption1, bodyOption2, bodyOption3;
    TMP_Dropdown.OptionData colorOption1, colorOption2, colorOption3, colorOption4, colorOption5, colorOption6;
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this);
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            DontDestroyOnLoad(GameObject.FindGameObjectsWithTag("Player")[i]);
            units.Add(GameObject.FindGameObjectsWithTag("Player")[i]);
            
        }

    }
    void Start()
    {
        
        materialDropdown.ClearOptions();
        colorDropdown.ClearOptions();

        List<TMP_Dropdown.OptionData> bodyOptions = new List<TMP_Dropdown.OptionData>();
        bodyOption1 = new TMP_Dropdown.OptionData();
        bodyOption1.text = "Head";
        bodyOptions.Add(bodyOption1);

        bodyOption2 = new TMP_Dropdown.OptionData();
        bodyOption2.text = "Body";
        bodyOptions.Add(bodyOption2);

        bodyOption3 = new TMP_Dropdown.OptionData();
        bodyOption3.text = "Feet";
        bodyOptions.Add(bodyOption3);

        foreach (TMP_Dropdown.OptionData option in bodyOptions)
        {
            materialDropdown.options.Add(option);
        }

        List<TMP_Dropdown.OptionData> colorOptions = new List<TMP_Dropdown.OptionData>();
        colorOption1 = new TMP_Dropdown.OptionData();
        colorOption1.text = "Red";
        colorOptions.Add(colorOption1);

        colorOption2 = new TMP_Dropdown.OptionData();
        colorOption2.text = "Purple";
        colorOptions.Add(colorOption2);

        colorOption3 = new TMP_Dropdown.OptionData();
        colorOption3.text = "Blue";
        colorOptions.Add(colorOption3);

        colorOption4 = new TMP_Dropdown.OptionData();
        colorOption4.text = "Cyan";
        colorOptions.Add(colorOption4);

        colorOption5 = new TMP_Dropdown.OptionData();
        colorOption5.text = "Green";
        colorOptions.Add(colorOption5);

        colorOption6 = new TMP_Dropdown.OptionData();
        colorOption6.text = "Yellow";
        colorOptions.Add(colorOption6);

        foreach (TMP_Dropdown.OptionData option in colorOptions)
        {
            colorDropdown.options.Add(option);
        }

        units[0].transform.localPosition = new Vector3(0, 1, (float).72);
        currentUnit = units[0];
        foreach(GameObject unit in units)
        {
            if (unit != currentUnit)
            {
                unit.SetActive(false);
            }
        }
        currentUnitInfo = currentUnit.GetComponent<UnitInfo>();
        selectedMaterial = currentUnit.GetComponent<Renderer>().materials[materialDropdown.value];
        lastMaterial = selectedMaterial;
        ShowInfo();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextClick()
    {
        lastUnit = currentUnit;
        if (units.Count <= units.IndexOf(lastUnit) + 1)
        {
            currentUnit = units[0];
        }
        else
        {
            currentUnit = units[units.IndexOf(lastUnit) + 1];
        }
        lastUnit.SetActive(false);
        currentUnit.transform.localPosition = new Vector3((float)0, (float)1, (float).72);
        selectedMaterial = currentUnit.GetComponent<Renderer>().materials[0];
        currentUnit.SetActive(true);
        currentUnitInfo = currentUnit.GetComponent<UnitInfo>();
        ShowInfo();
        Debug.Log(currentUnitInfo.unitClass);
    }
    public void PrevClick()
    {
        lastUnit = currentUnit;
        if (units.IndexOf(lastUnit) == 0)
        {
            currentUnit = units[units.Count -1];
        }
        else
        {
            currentUnit = units[units.IndexOf(lastUnit) - 1];
        }
        lastUnit.SetActive(false);
        currentUnit.SetActive(true);
        currentUnit.transform.localPosition = new Vector3((float)0, (float)1, (float).72);
        selectedMaterial = currentUnit.GetComponent<Renderer>().materials[0];
        currentUnitInfo = currentUnit.GetComponent<Warrior>();
        ShowInfo();
        
    }

    public void ChangeMaterial()
    {
        if(materialDropdown.value == 0)
        {
            lastMaterial = selectedMaterial;
            selectedMaterial = currentUnit.GetComponent<Renderer>().materials[0];

        }
        else if (materialDropdown.value == 1)
        {
            lastMaterial = selectedMaterial;
            selectedMaterial = currentUnit.GetComponent<Renderer>().materials[2];
        }
        else
        {
            lastMaterial = selectedMaterial;
            selectedMaterial = currentUnit.GetComponent<Renderer>().materials[1];
        }
    }
    public void ChangeColor()
    {
        if (colorDropdown.value == 0)
        {
            if (lastMaterial == selectedMaterial)
            {
                unitAttack.text = "Attack: " + currentUnitInfo.unitAttack.ToString();
                unitEnergy.text = "Energy: " + currentUnitInfo.unitEnergy.ToString();
                unitCrit.text = "Crit Rate: " + currentUnitInfo.unitCritChance.ToString();
                unitDebuffResist.text = "Debuff Resist: " + currentUnitInfo.unitSkillResist.ToString();
                unitHealth.text = "Health: " + currentUnitInfo.unitHealth.ToString();
                unitDefense.text = "Defense: " + currentUnitInfo.unitDefense.ToString();
                unitMovementSpeed.text = "Move Speed: " + currentUnitInfo.unitMovementSpeed.ToString();
                unitEnergy.text = "Attack Range: " + currentUnitInfo.unitAttackRange.ToString();
                unitEvasion.text = "Evasion: " + currentUnitInfo.unitEvasion.ToString();
            }
            selectedMaterial.color = new Color(1,0,0,1);
            ChangingStats();
        }
        else if (colorDropdown.value == 1)
        {
            if (lastMaterial == selectedMaterial)
            {
                unitAttack.text = "Attack: " + currentUnitInfo.unitAttack.ToString();
                unitEnergy.text = "Energy: " + currentUnitInfo.unitEnergy.ToString();
                unitCrit.text = "Crit Rate: " + currentUnitInfo.unitCritChance.ToString();
                unitDebuffResist.text = "Debuff Resist: " + currentUnitInfo.unitSkillResist.ToString();
                unitHealth.text = "Health: " + currentUnitInfo.unitHealth.ToString();
                unitDefense.text = "Defense: " + currentUnitInfo.unitDefense.ToString();
                unitMovementSpeed.text = "Move Speed: " + currentUnitInfo.unitMovementSpeed.ToString();
                unitEnergy.text = "Attack Range: " + currentUnitInfo.unitAttackRange.ToString();
                unitEvasion.text = "Evasion: " + currentUnitInfo.unitEvasion.ToString();
            }
            selectedMaterial.color = new Color(.5f,0.5f,1);
            ChangingStats();
        }
        else if (colorDropdown.value == 2)
        {
            if (lastMaterial == selectedMaterial)
            {
                unitAttack.text = "Attack: " + currentUnitInfo.unitAttack.ToString();
                unitEnergy.text = "Energy: " + currentUnitInfo.unitEnergy.ToString();
                unitCrit.text = "Crit Rate: " + currentUnitInfo.unitCritChance.ToString();
                unitDebuffResist.text = "Debuff Resist: " + currentUnitInfo.unitSkillResist.ToString();
                unitHealth.text = "Health: " + currentUnitInfo.unitHealth.ToString();
                unitDefense.text = "Defense: " + currentUnitInfo.unitDefense.ToString();
                unitMovementSpeed.text = "Move Speed: " + currentUnitInfo.unitMovementSpeed.ToString();
                unitEnergy.text = "Attack Range: " + currentUnitInfo.unitAttackRange.ToString();
                unitEvasion.text = "Evasion: " + currentUnitInfo.unitEvasion.ToString();
            }
            selectedMaterial.color = new Color(0,0,1,1);
            ChangingStats();
        }
        else if (colorDropdown.value == 3)
        {
            if (lastMaterial == selectedMaterial)
            {
                unitAttack.text = "Attack: " + currentUnitInfo.unitAttack.ToString();
                unitEnergy.text = "Energy: " + currentUnitInfo.unitEnergy.ToString();
                unitCrit.text = "Crit Rate: " + currentUnitInfo.unitCritChance.ToString();
                unitDebuffResist.text = "Debuff Resist: " + currentUnitInfo.unitSkillResist.ToString();
                unitHealth.text = "Health: " + currentUnitInfo.unitHealth.ToString();
                unitDefense.text = "Defense: " + currentUnitInfo.unitDefense.ToString();
                unitMovementSpeed.text = "Move Speed: " + currentUnitInfo.unitMovementSpeed.ToString();
                unitEnergy.text = "Attack Range: " + currentUnitInfo.unitAttackRange.ToString();
                unitEvasion.text = "Evasion: " + currentUnitInfo.unitEvasion.ToString();
            }
            selectedMaterial.color = new Color(0,.5f,.5f,1);
            ChangingStats();
        }
        else if (colorDropdown.value == 4)
        {
            if (lastMaterial == selectedMaterial)
            {
                unitAttack.text = "Attack: " + currentUnitInfo.unitAttack.ToString();
                unitEnergy.text = "Energy: " + currentUnitInfo.unitEnergy.ToString();
                unitCrit.text = "Crit Rate: " + currentUnitInfo.unitCritChance.ToString();
                unitDebuffResist.text = "Debuff Resist: " + currentUnitInfo.unitSkillResist.ToString();
                unitHealth.text = "Health: " + currentUnitInfo.unitHealth.ToString();
                unitDefense.text = "Defense: " + currentUnitInfo.unitDefense.ToString();
                unitMovementSpeed.text = "Move Speed: " + currentUnitInfo.unitMovementSpeed.ToString();
                unitEnergy.text = "Attack Range: " + currentUnitInfo.unitAttackRange.ToString();
                unitEvasion.text = "Evasion: " + currentUnitInfo.unitEvasion.ToString();
            }
            selectedMaterial.color = new Color(0,1,0,1);
            ChangingStats();
        }
        else
        {
            if (lastMaterial == selectedMaterial)
            {
                unitAttack.text = "Attack: " + currentUnitInfo.unitAttack.ToString();
                unitEnergy.text = "Energy: " + currentUnitInfo.unitEnergy.ToString();
                unitCrit.text = "Crit Rate: " + currentUnitInfo.unitCritChance.ToString();
                unitDebuffResist.text = "Debuff Resist: " + currentUnitInfo.unitSkillResist.ToString();
                unitHealth.text = "Health: " + currentUnitInfo.unitHealth.ToString();
                unitDefense.text = "Defense: " + currentUnitInfo.unitDefense.ToString();
                unitMovementSpeed.text = "Move Speed: " + currentUnitInfo.unitMovementSpeed.ToString();
                unitEnergy.text = "Attack Range: " + currentUnitInfo.unitAttackRange.ToString();
                unitEvasion.text = "Evasion: " + currentUnitInfo.unitEvasion.ToString();
            }
            selectedMaterial.color = new Color(.5f,.5f,0,1);
            ChangingStats();
        }
        
    }

    public void SceneSwitch()
    {

        currentUnitInfo.unitAttack += Mathf.RoundToInt(.25f * currentUnit.GetComponent<Renderer>().materials[0].color.r * currentUnitInfo.unitAttack);
        currentUnitInfo.unitEnergy += Mathf.RoundToInt(.25f * currentUnit.GetComponent<Renderer>().materials[0].color.g * currentUnitInfo.unitEnergy);
        currentUnitInfo.unitCritChance += Mathf.RoundToInt(.25f * currentUnit.GetComponent<Renderer>().materials[0].color.b * currentUnitInfo.unitCritChance);

        currentUnitInfo.unitSkillResist += Mathf.RoundToInt(.25f * currentUnit.GetComponent<Renderer>().materials[2].color.r * currentUnitInfo.unitSkillResist);
        currentUnitInfo.unitMaxHealth += Mathf.RoundToInt(.25f * currentUnit.GetComponent<Renderer>().materials[2].color.g * currentUnitInfo.unitMaxHealth);
        currentUnitInfo.unitDefense += Mathf.RoundToInt(.25f * currentUnit.GetComponent<Renderer>().materials[2].color.b * currentUnitInfo.unitDefense);
        
        currentUnitInfo.unitMovementSpeed += .05f * currentUnit.GetComponent<Renderer>().materials[1].color.r * currentUnitInfo.unitMovementSpeed;
        currentUnitInfo.unitAttackRange += .05f * currentUnit.GetComponent<Renderer>().materials[1].color.g * currentUnitInfo.unitEnergy;
        currentUnitInfo.unitEvasion += Mathf.RoundToInt(.25f * currentUnit.GetComponent<Renderer>().materials[1].color.b * currentUnitInfo.unitCritChance);
        
        SceneManager.LoadScene("SampleScene");
        for (int i = 0; i < units.Count; i++)
        {
            DontDestroyOnLoad(units[i]);
            units[i].SetActive(true);
            units[i].gameObject.transform.localScale = new Vector3(25,25,25);
            
            
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }

    void OnSceneLoaded(Scene SampleScene,LoadSceneMode single)
    {
        god = GameObject.Find("god");
        gameManager = god.GetComponent<GameManager>();
        gameManager.attackingUnit = currentUnit;
    }

    public void ShowInfo()
    {
        unitNameText.text = "Name: " + currentUnitInfo.unitName;
        unitLevel.text = "Level: " + currentUnitInfo.unitLevel.ToString();
        unitClass.text = "Class: " + currentUnitInfo.unitClass;
        unitAttack.text = "Attack: " + currentUnitInfo.unitAttack.ToString();
        unitDefense.text = "Defense: " + currentUnitInfo.unitDefense.ToString();
        unitHealth.text = currentUnitInfo.unitMaxHealth.ToString();
        unitSpeed.text = "Speed: " + currentUnitInfo.unitMovementSpeed.ToString();
        unitCrit.text = "Crit Chance: " + currentUnitInfo.unitCritChance.ToString();
        unitDebuffResist.text = "Skill Resist.: " + currentUnitInfo.unitSkillResist.ToString();
        unitEvasion.text = "Evasion: " + currentUnitInfo.unitEvasion.ToString();
        unitEnergy.text = "Energy: " + currentUnitInfo.unitEnergy.ToString();
        unitMovementSpeed.text = "Move Speed: " + currentUnitInfo.unitMovementSpeed.ToString();
        Debug.Log(currentUnitInfo.unitEnergy);
    }

    public void ChangingStats()
    {
        if (materialDropdown.value == 0)
        {
            
            unitAttack.text = "Attack: " + (currentUnitInfo.unitAttack + Mathf.RoundToInt(.25f * selectedMaterial.color.r * currentUnitInfo.unitAttack)).ToString();
            unitEnergy.text = "Energy: " + (currentUnitInfo.unitEnergy + Mathf.RoundToInt(.25f * selectedMaterial.color.g * currentUnitInfo.unitEnergy)).ToString();
            unitCrit.text = "Crit Rate: " + (currentUnitInfo.unitCritChance + Mathf.RoundToInt(.25f * selectedMaterial.color.b * currentUnitInfo.unitCritChance)).ToString();
        }
        else if(materialDropdown.value == 1)
        {
            
            unitDebuffResist.text = "Debuff Resist: " + (currentUnitInfo.unitSkillResist + Mathf.RoundToInt(.25f * selectedMaterial.color.r * currentUnitInfo.unitSkillResist)).ToString();
            unitHealth.text = "Health: " + (currentUnitInfo.unitMaxHealth + Mathf.RoundToInt(.25f * selectedMaterial.color.g * currentUnitInfo.unitMaxHealth)).ToString();
            unitDefense.text = "Defense: " + (currentUnitInfo.unitDefense + Mathf.RoundToInt(.25f * selectedMaterial.color.b * currentUnitInfo.unitDefense)).ToString();
        }
        else
        {
            unitMovementSpeed.text = "Move Speed: " + (currentUnitInfo.unitMovementSpeed + .05f * selectedMaterial.color.r * currentUnitInfo.unitMovementSpeed).ToString();
            unitAttackRange.text = "Attack Range: " + (currentUnitInfo.unitAttackRange + .05f * selectedMaterial.color.g * currentUnitInfo.unitEnergy).ToString();
            unitEvasion.text = "Evasion: " + (currentUnitInfo.unitEvasion + Mathf.RoundToInt(.25f * selectedMaterial.color.b * currentUnitInfo.unitCritChance)).ToString();
        }
    }

}
