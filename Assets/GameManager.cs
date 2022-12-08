using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;


public class GameManager : MonoBehaviour
{
    #region "Variables in GameManager"
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
    //Customization customization;
    public GameObject selectedUnit;
    public GameObject selectedEnemy;
    public int crit;
    public int counterChance;
    public int finishedEnemyUnits;
    public int finishedUnits;
    public List <GameObject> enemySpeedList;
    GameObject fastestUnit;
    public TextMeshProUGUI damageText;
    public List <int> unitDistance;
    #endregion

    //have units deselect at the end of turn
    //control health gain/loss
    //control win/loss condition
    //control death


    void Awake()
    {
        infoPopulation = GetComponent<InfoPopulation>();
        //customization = GameObject.Find("boss").GetComponent<Customization>();
        attackingUnit = null;
        whoseTurn = "Player";

        // DontDestroyOnLoad(this.gameObject);
        #region "Grabbing player units position and set active; grabs enemies units"
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length; i++)
        {
            //Debug.Log(GameObject.FindGameObjectsWithTag("Spawn").Length);

        }
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Player"))
        {
            aliveUnits.Add(unit);
            //unit.gameObject.transform.localPosition = GameObject.FindGameObjectsWithTag("Spawn")[aliveUnits.IndexOf(unit)].gameObject.transform.localPosition;
            //Debug.Log(unit.transform.childCount);
            //unit.transform.GetChild(0).Set
        }
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Enemy"))
        {

            aliveEnemyUnits.Add(unit);
        }
        #endregion


        TurnSwitch();

        //startButton = GameObject.Find("StartButton").GetComponent<Button>();
        
    }
    //grabs grid movement
    private void Start()
    {
        for(int x =0; x < aliveUnits.Count; x++)
        {
            aliveUnits[x].transform.position += new Vector3(x,0, 0);
        }
        //gridMovement = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<GridMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //Grid movement for the player units
        /*if (gridMovement == null)
        {
            if(aliveUnits.Count > 0)
            {
                gridMovement = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<GridMovement>();
            }
            
        }*/
        if (false) 
            ;
        else
        {
            if (gridMovement != null && gridMovement.attackPossible == true)
            {

                //Attack Calculation
                if (attackingUnit == null)
                {
                    attackingUnit = selectedUnit;
                    Debug.Log("Its " + attackingUnit);
                    //attackButton.gameObject.SetActive(false);
                }
            }
            else
            {
                //attackButton.gameObject.SetActive(false);
            }
        }


        //If all player units have gone, switch turns
        if (finishedUnits == aliveUnits.Count)
        {
            Debug.Log(finishedUnits);
            TurnSwitch();
        }

        //player loses all units
        if (aliveUnits.Count <= 0)
        {
            Debug.Log("You Lose");

        }

        //enemy loses all units
        if(aliveEnemyUnits.Count <= 0)
        {
            Debug.Log("You Win!");
            SceneManager.LoadScene("WinScene");
        }

        //selection of units for the player, shows in green
        if (Input.GetMouseButtonDown(1))
        {
            selectedUnit.GetComponent<Renderer>().material.color = Color.green;
                
        }
    }


    //switches turns between player and enemies.
    // ------RECURSIVE FUNCTION-------
    public void TurnSwitch()
    {
        if (whoseTurn == "Enemy")
        {
            Debug.Log("Enemy's Turn");
            for (int i = 0; i < aliveUnits.Count; i++)
            {
                aliveUnits[i].transform.gameObject.SetActive(true);
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
                    gridMovement = ally.gameObject.GetComponent<GridMovement>();
                    gridMovement.attackPossible = false;
                    gridMovement.moved = false;
                    ally.transform.GetChild(0).gameObject.SetActive(false);
                }

                whoseTurn = "Enemy";
                finishedUnits = 0;
                //TurnSwitch();

            }
            
            
        }
    }

    //Unit attack, checks unit attacking, enemy health low, and counter attack for the unit
    public void UnitAttack()
    {
        gridMovement = selectedUnit.GetComponent<GridMovement>();
        attackingUnit = selectedUnit;
        attackingEnemyUnit = selectedEnemy;
        attackingEnemyInfo = attackingEnemyUnit.GetComponent<UnitInfo>();
        attackingUnitInfo = attackingUnit.GetComponent<UnitInfo>();

        Debug.Log("Attacking " + attackingEnemyUnit.name);
        crit = Random.Range(attackingUnitInfo.unitCritChance, 100);
        StartCoroutine(ShowDamage(attackingUnitInfo.unitAttack, attackingEnemyUnit));
        
        //if player rolls higher than 80, crit occurs
        if (crit > 80)
        {
            attackingEnemyUnit.GetComponent<EnemyHPBar>().ShowHP(attackingUnitInfo.unitAttack + Mathf.RoundToInt(attackingUnitInfo.unitAttack * (float).1));
            damageText.text = (Mathf.RoundToInt(attackingUnitInfo.unitAttack += Mathf.RoundToInt(attackingUnitInfo.unitAttack * (float).1))).ToString();
            attackingEnemyInfo.unitHealth -= attackingUnitInfo.unitAttack + Mathf.RoundToInt(attackingUnitInfo.unitAttack*(float).1);
        }

        //no crit
        else
        {
            attackingEnemyUnit.GetComponent<EnemyHPBar>().ShowHP(attackingUnitInfo.unitAttack);
            damageText.text = attackingUnitInfo.unitAttack.ToString();
            attackingEnemyInfo.unitHealth -= attackingUnitInfo.unitAttack;
        }
        finishedUnits++;

        Debug.Log("enemy health is now " + attackingEnemyInfo.unitHealth);
        Debug.Log(finishedUnits);

        //If enemy loses all health, destroy GO
        if (attackingEnemyInfo.unitHealth <= 0)
        {
            gridMovement.selected = "None";
            aliveEnemyUnits.Remove(attackingEnemyUnit);
            //damageText.gameObject.SetActive(false);
            Destroy(attackingEnemyUnit);
        }

        //Chance to see if counter attack occurs
        else
        {
            //Counterattack
            counterChance = Random.Range(0, 10);
            if (counterChance > 7)
            {
                damageText.text = attackingEnemyInfo.unitAttack.ToString();
                attackingUnitInfo.unitHealth -= attackingEnemyInfo.unitAttack;
                Debug.Log(attackingUnitInfo.unitHealth);
                //Is player alive? No, destroy GO
                if (attackingUnitInfo.unitHealth <= 0)
                {
                    aliveUnits.Remove(attackingUnit);
                    Destroy(attackingUnit);
                }
            }
            
        }
        
        //clears out the variables for next run of script code
        selectedUnit = null;
        selectedEnemy = null;
        attackingUnit = null;
        gridMovement.selected = null;
        
        gridMovement.attackPossible = false;

    }

    //Enemy Attacks, checks fastest enemy, goes in order for all enemies, checks for player unit low, and counter attack for player unit
    IEnumerator EnemyAttack()
    {
        //checks to see what is the fastest unit and sort it out in a varaible
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
            //checks to find the closest attacking unit for the enemy unit
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

            //crit chance, if >80 crit, else normal damage
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


            //checks to see if unit died, if so, destroy
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
                    
                    //checks to see if the enemy dies from counter attack
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

        //clears out variables for next run of script code
        selectedUnit = null;
        selectedEnemy = null;
        gridMovement.selected = null;
        gridMovement.attackPossible = false;
        Debug.Log("Enemy Done");
        whoseTurn = "Player";
        
        TurnSwitch();
    }

    //Shows the damage that is done to the attacked unit
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
