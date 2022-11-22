using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class InfoPopulation : MonoBehaviour
{
    
    #region "Variables in InfoPopulation"
    [HideInInspector]
    public TextMeshProUGUI unitNameText;
    [HideInInspector]
    public TextMeshProUGUI unitLevel;
    [HideInInspector]
    public TextMeshProUGUI unitClass;
    [HideInInspector]
    public TextMeshProUGUI unitAttack;
    [HideInInspector]
    public TextMeshProUGUI unitDefense;
    [HideInInspector]
    public TextMeshProUGUI unitHealth;
    [HideInInspector]
    public TextMeshProUGUI unitSpeed;
    [HideInInspector]
    public TextMeshProUGUI unitCrit;
    [HideInInspector]
    public TextMeshProUGUI unitDebuffResist;
    [HideInInspector]
    public TextMeshProUGUI unitEvasion;
    //public TextMeshProUGUI unitHealthResist;
    public GameObject unitInfoPanel = null;
    public GameManager gameManager;
    public UnitInfo selectedObject;
    public GridMovement gridMovement;
    public Collider selectedCollider;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        if (selectedCollider.gameObject.tag == "Player")
        {
            selectedObject = selectedCollider.gameObject.GetComponent<UnitInfo>();
            gridMovement = selectedCollider.gameObject.GetComponent<GridMovement>();

            unitInfoPanel.SetActive(true);

            if (selectedCollider.gameObject.transform.position.x > 0)
            {
                unitInfoPanel.GetComponent<RectTransform>().localPosition = new Vector3(gridMovement.hit.point.x + selectedCollider.gameObject.transform.position.x * 400, gridMovement.hit.point.y, gridMovement.hit.point.z);
            }
            else if (selectedCollider.gameObject.transform.position.x == 0)
            {
                unitInfoPanel.GetComponent<RectTransform>().localPosition = new Vector3(gridMovement.hit.point.x + 400, gridMovement.hit.point.y, gridMovement.hit.point.z);
            }
            else
            {
                unitInfoPanel.GetComponent<RectTransform>().localPosition = new Vector3(gridMovement.hit.point.x + selectedCollider.gameObject.transform.position.x * 400, gridMovement.hit.point.y, gridMovement.hit.point.z);
            }
            //unitInfoPanel.transform.position = new Vector3(gridMovement.hit.point.x + 100, 0, gridMovement.hit.point.z);
            unitNameText.text = "Name: " + selectedObject.unitName;
            unitLevel.text = "Lv: " + selectedObject.unitLevel.ToString();
            unitClass.text = "Class: " + selectedObject.unitClass;
            unitAttack.text = "Attack: " + selectedObject.unitAttack.ToString();
            unitDefense.text = "Def: " + selectedObject.unitDefense.ToString();
            unitHealth.text = "HP: " + selectedObject.unitHealth.ToString();
            unitSpeed.text = "Speed: " + selectedObject.unitMovementSpeed.ToString();
            unitCrit.text = "Crit Chance: " + selectedObject.unitCritChance.ToString();
            unitDebuffResist.text = "Debuff Res.: " + selectedObject.unitSkillResist.ToString();
            unitEvasion.text = "Evasion: " + selectedObject.unitEvasion.ToString();
        }
        else if (selectedCollider.gameObject.tag == "Enemy")
        {
            selectedObject = selectedCollider.gameObject.GetComponent<UnitInfo>();

            //Debug.Log(gridMovement.hit.point);
            if (selectedCollider.gameObject.transform.position.x > 0)
            {
                Debug.Log(selectedCollider.gameObject.transform.position.x * 400);
                unitInfoPanel.GetComponent<RectTransform>().transform.parent.position = new Vector3(gridMovement.hit.point.x + selectedCollider.gameObject.transform.position.x * 400, gridMovement.hit.point.y, gridMovement.hit.point.z);
            }
            else if (selectedCollider.gameObject.transform.position.x == 0)
            {
                Debug.Log(gridMovement.hit.point.x + selectedCollider.gameObject.transform.position.x + 400);
                unitInfoPanel.GetComponent<RectTransform>().transform.parent.position = new Vector3(gridMovement.hit.point.x + selectedCollider.gameObject.transform.position.x + 400, gridMovement.hit.point.y, gridMovement.hit.point.z);
            }
            else
            {
                unitInfoPanel.GetComponent<RectTransform>().transform.parent.position = new Vector3(gridMovement.hit.point.x + selectedCollider.gameObject.transform.position.x * 400, gridMovement.hit.point.y, gridMovement.hit.point.z);
            }
            unitInfoPanel.transform.position = new Vector3(gridMovement.hit.point.x + 100, 0, gridMovement.hit.point.z);
            unitNameText.text = "Name: " + selectedObject.unitName;
            unitLevel.text = "Lv: " + selectedObject.unitLevel.ToString();
            unitClass.text = "Class: " + selectedObject.unitClass;
            unitAttack.text = "Attack: " + selectedObject.unitAttack.ToString();
            unitDefense.text = "Def: " + selectedObject.unitDefense.ToString();
            unitHealth.text = "HP: " + selectedObject.unitHealth.ToString();
            unitSpeed.text = "Speed: " + selectedObject.unitMovementSpeed.ToString();
            unitCrit.text = "Crit Chance: " + selectedObject.unitCritChance.ToString();
            unitDebuffResist.text = "Debuff Res.: " + selectedObject.unitSkillResist.ToString();
            unitEvasion.text = "Evasion: " + selectedObject.unitEvasion.ToString();
        }
        
        
    }

    void ChangedActiveScene(Scene current, Scene next)
    {
        if (next.name == "Battle Scene")
        {
            gameManager = GameObject.Find("boss").GetComponent<GameManager>();
            gameObject.AddComponent<GridMovement>();
            //gridMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<GridMovement>();
            //gridMovement.collider = this.GetComponent<CapsuleCollider>();
        }
    }
}
