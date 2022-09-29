using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public List<GameObject> aliveUnits;
    public List <GameObject> aliveEnemyUnits;
    public GridMovement gridMovement;
    public GameObject attackingUnit;
    public GameObject attackingEnemyUnit;
    public Button attackButton;
    public Button startButton;
    UnitInfo attackingUnitInfo;
    UnitInfo attackingEnemyInfo;
    InfoPopulation infoPopulation;
    public string whoseTurn;
    Customization customization;
    public GameObject selectedUnit;
    public GameObject selectedEnemy;
    public int crit;
    public int counterChance;
    public int finishedEnemyUnits;
    public int finishedUnits;
    public List <GameObject> enemySpeedList;
    GameObject fastestUnit;
    public TextMeshPro damageText;
    public List <int> unitDistance;


    //have units deselect at the end of turn
    //control health gain/loss
    //control win/loss condition
    //control death
    // Start is called before the first frame update

    void Awake()
    {
        infoPopulation = GameObject.Find("god").GetComponent<InfoPopulation>();
        customization = GameObject.Find("boss").GetComponent<Customization>();
        attackingUnit = null;
        whoseTurn = "Player";

        

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            Debug.Log(GameObject.FindGameObjectsWithTag("Spawn").Length);
            
        }
        foreach (GameObject unit in customization.units)
        {
            aliveUnits.Add(unit);
            unit.gameObject.transform.localPosition = GameObject.FindGameObjectsWithTag("Spawn")[customization.units.IndexOf(unit)].gameObject.transform.localPosition;
            unit.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            
            aliveEnemyUnits.Add(unit);

        }
        
        TurnSwitch();

        //startButton = GameObject.Find("StartButton").GetComponent<Button>();
        
    }

    private void Start()
    {
        gridMovement = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<GridMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gridMovement == null)
        {
            if(aliveUnits.Count > 0)
            {
                gridMovement = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<GridMovement>();
            }
            
        }
        else
        {
            if (gridMovement.attackPossible == true)
            {
                
                //Attack Calculation
                if (attackingUnit == null)
                {
                    attackingUnit = selectedUnit;
                    Debug.Log("Its " + attackingUnit);
                    attackButton.gameObject.SetActive(false);
                }
            }
            else
            {
                //attackButton.gameObject.SetActive(false);
            }
        }
        
        if (finishedUnits == aliveUnits.Count)
        {
            Debug.Log(finishedUnits);
            TurnSwitch();
        }
            
        if (aliveUnits.Count <= 0)
        {
            Debug.Log("You Lose");

        }
        if(aliveEnemyUnits.Count <= 0)
        {
            Debug.Log("You Win!");
        }
        if (Input.GetMouseButtonDown(1))
        {
            selectedUnit.GetComponent<Renderer>().material.color = Color.green;
                
        }
    }

    public void TurnSwitch()
    {
        if (whoseTurn == "Enemy")
        {
            Debug.Log("Enemy's Turn");
            for (int i = 0; i < aliveUnits.Count; i++)
            {
                aliveUnits[i].transform.GetChild(0).gameObject.SetActive(true);
            }
            StartCoroutine(EnemyAttack());
            finishedUnits = 0;
            
            whoseTurn = "Player";
            TurnSwitch();

        }
        else if (whoseTurn == "Player")
        {
            if (finishedUnits == aliveUnits.Count)
            {
                foreach (GameObject ally in aliveUnits)
                {
                    Debug.Log("reseting moved");
                    gridMovement = ally.gameObject.GetComponentInChildren<GridMovement>();
                    gridMovement.attackPossible = false;
                    gridMovement.moved = false;
                    ally.transform.GetChild(0).gameObject.SetActive(false);
                }

                whoseTurn = "Enemy";
                finishedUnits = 0;
                TurnSwitch();

            }
            
            
        }
    }
    public void UnitAttack()
    {
        gridMovement = selectedUnit.GetComponentInChildren<GridMovement>();
        attackingUnit = selectedUnit;
        attackingEnemyUnit = selectedEnemy;
        attackingEnemyInfo = attackingEnemyUnit.GetComponent<UnitInfo>();
        attackingUnitInfo = attackingUnit.GetComponent<UnitInfo>();

        Debug.Log("Attacking " + attackingEnemyUnit.name);
        crit = Random.Range(attackingUnitInfo.unitCritChance, 100);
        StartCoroutine(ShowDamage(attackingUnitInfo.unitAttack, attackingEnemyUnit));
        if (crit > 80)
        {
            
            damageText.text = (Mathf.RoundToInt(attackingUnitInfo.unitAttack += Mathf.RoundToInt(attackingUnitInfo.unitAttack * (float).1))).ToString();
            attackingEnemyInfo.unitHealth -= attackingUnitInfo.unitAttack + Mathf.RoundToInt(attackingUnitInfo.unitAttack*(float).1);
        }
        else
        {
            damageText.text = attackingUnitInfo.unitAttack.ToString();
            attackingEnemyInfo.unitHealth -= attackingUnitInfo.unitAttack;
        }
        finishedUnits++;

        Debug.Log("enemy health is now " + attackingEnemyInfo.unitHealth);

        if (attackingEnemyInfo.unitHealth <= 0)
        {
            gridMovement.selected = "None";
            aliveEnemyUnits.Remove(attackingEnemyUnit);
            //damageText.gameObject.SetActive(false);
            Destroy(attackingEnemyUnit);
        }
        else
        {
            //Counterattack
            counterChance = Random.Range(0, 10);
            if (counterChance > 7)
            {
                damageText.text = attackingEnemyInfo.unitAttack.ToString();
                attackingUnitInfo.unitHealth -= attackingEnemyInfo.unitAttack;
                Debug.Log(attackingUnitInfo.unitHealth);
                if (attackingUnitInfo.unitHealth <= 0)
                {
                    aliveUnits.Remove(attackingUnit);
                    Destroy(attackingUnit);
                }
            }
            
        }
        
        selectedUnit = null;
        selectedEnemy = null;
        attackingUnit = null;
        gridMovement.selected = null;
        
        gridMovement.attackPossible = false;

    }
    IEnumerator EnemyAttack()
    {
        enemySpeedList = aliveEnemyUnits;
        for (int i = 0; i < aliveEnemyUnits.Count - 2; i++)
        {
            if(aliveEnemyUnits[i].GetComponent<UnitInfo>().unitMovementSpeed > aliveEnemyUnits[i+1].GetComponent<UnitInfo>().unitMovementSpeed || enemySpeedList.Count == 0)
            {

                fastestUnit = aliveEnemyUnits[i];
                if (!enemySpeedList.Contains(fastestUnit))
                {
                    enemySpeedList.Add(fastestUnit);
                }
            }
            else
            {
                fastestUnit = aliveEnemyUnits[i + 1];
                if (!enemySpeedList.Contains(fastestUnit))
                {
                    enemySpeedList.Add(fastestUnit);
                }
            }
        }

        for (int j = 0; j < aliveEnemyUnits.Count; j++)
        {
            Debug.Log("Test");
            attackingEnemyUnit = aliveEnemyUnits[j];
            attackingEnemyInfo = attackingEnemyUnit.GetComponent<UnitInfo>();
            for (int i = 0; i < aliveUnits.Count; i++)
            {
                unitDistance.Add(i);
            }
            attackingUnit = aliveUnits[unitDistance.IndexOf(unitDistance.Min())];
            attackingUnitInfo = attackingUnit.GetComponent<UnitInfo>();

            Debug.Log("Attacking " + attackingUnit.name);

            
            crit = Random.Range(attackingUnitInfo.unitCritChance, 100);
            StartCoroutine(ShowDamage(attackingEnemyInfo.unitAttack, attackingUnit));
            if (crit > 80)
            {
                attackingUnitInfo.unitHealth -= attackingEnemyInfo.unitAttack + Mathf.RoundToInt(attackingUnitInfo.unitAttack * (float).1);
            }
            else
            {
                attackingUnitInfo.unitHealth -= attackingEnemyInfo.unitAttack;
            }


            Debug.Log(attackingUnitInfo.unitHealth);

            if (attackingUnitInfo.unitHealth <= 0)
            {
                aliveUnits.Remove(attackingUnit);
                Destroy(attackingUnit);
            }
            else
            {
                //Counterattack
                counterChance = Random.Range(0, 10);
                if (counterChance > 7)
                {
                    damageText.text = attackingUnitInfo.unitAttack.ToString();
                    attackingEnemyInfo.unitHealth -= attackingUnitInfo.unitAttack;
                    Debug.Log(attackingEnemyInfo.unitHealth);
                    if (attackingEnemyInfo.unitHealth <= 0)
                    {
                        aliveEnemyUnits.Remove(attackingEnemyUnit);
                        enemySpeedList.Remove(attackingEnemyUnit);
                        Destroy(attackingEnemyUnit);
                    }
                }

            }
            yield return new WaitForSeconds(2);

        }

        selectedUnit = null;
        selectedEnemy = null;
        gridMovement.selected = null;
        gridMovement.attackPossible = false;
        Debug.Log("Enemy Done");
        whoseTurn = "Player";
        
        TurnSwitch();
    }

    IEnumerator ShowDamage(int attack, GameObject attackedUnit)
    {
        damageText.gameObject.SetActive(true);
        damageText.GetComponent<RectTransform>().localPosition = attackedUnit.gameObject.transform.position;
        if (crit > 80)
        {
            damageText.text = (attack + (attack * .1)).ToString();
        }
        else
        {
            damageText.text = (attack).ToString();
        }

        yield return new WaitForSeconds(2);
        damageText.gameObject.SetActive(false);
    }
}
