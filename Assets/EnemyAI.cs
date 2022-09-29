using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GridMovement gridMovement;
    Customization customization;
    public Collider collider;
    public GameManager gameManager;
    public InfoPopulation infoPopulation;
    // Start is called before the first frame update
    void Start()
    {
        gridMovement = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<GridMovement>();
        customization = GameObject.Find("boss").GetComponent<Customization>();
        gameManager = GameObject.Find("god").GetComponent<GameManager>();
        infoPopulation = GameObject.Find("Unit Information").GetComponent<InfoPopulation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        gridMovement = gameManager.selectedUnit.GetComponentInChildren<GridMovement>();
        gridMovement.selected = "Enemy";
        //OnTriggerEnter(hit.collider);
        gameManager.selectedEnemy = collider.transform.gameObject;
        gameManager.attackingEnemyUnit = collider.gameObject;
        //gameManager.finishedUnits = selectedUnit.GetComponentInChildren<GridMovement>().unitsMoved;
        gridMovement.attackPossible = true;
        //gameManager.attackButton.gameObject.SetActive(true);
        gameManager.UnitAttack();
    }
    private void OnMouseOver()
    {
        Debug.Log("Over enemy");
        infoPopulation.selectedCollider = this.gameObject.GetComponent<Collider>();
        infoPopulation.Activate();
    }
    private void OnMouseExit()
    {
        infoPopulation.selectedCollider = null;
        infoPopulation.unitInfoPanel.GetComponent<RectTransform>().localPosition = new Vector3(2000, 1000, 1000);
    }
}
