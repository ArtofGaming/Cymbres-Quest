using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridMovement : MonoBehaviour
{
    public RaycastHit hit;
    public Ray ray;
    public string selected = "";
    Collider myCollider;
    private float movementReach = 0;
    public bool moved = false;
    public Material movementMaterial;
    public bool attackPossible;
    public string unitClass;
    public Customization customization;
    GameManager gameManager;
    public bool added = false;
    int unitsMoved = 0;
    public Collider collider;
    public InfoPopulation infoPopulation;
    

    //show movement and other actions possible

    // Start is called before the first frame update
    void Start()
    {
        attackPossible = false;
        customization = GameObject.Find("boss").GetComponent<Customization>();
        gameManager = GameObject.Find("god").GetComponent<GameManager>();
        gameManager.selectedUnit = gameManager.aliveUnits[0];
        infoPopulation = GameObject.Find("Unit Information").GetComponent<InfoPopulation>();
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        
        gameManager.selectedUnit = collider.transform.parent.gameObject;
        if (gameManager.selectedUnit.tag == "Player" && gameManager.whoseTurn == "Player" && selected != "player")
        {
            Debug.Log("Mouse Down");
            // Calculate the squares that unit can move to
            movementReach = gameManager.selectedUnit.GetComponent<UnitInfo>().unitMovementSpeed;
            // Show area that is movable
            gameManager.selectedUnit.transform.GetChild(0).localScale = new Vector3((float)movementReach, (float).002, (float)movementReach);
            myCollider = gameManager.selectedUnit.GetComponentInChildren<MeshCollider>();
            myCollider.gameObject.GetComponentInChildren<Renderer>().material = movementMaterial;
            selected = "player";
            gameManager.gridMovement = gameManager.selectedUnit.GetComponentInChildren<GridMovement>();
        }
        else
        {
            // if point selected is in allowed range and unit has not moved yet
            if (gameManager.selectedUnit.GetComponentInChildren<GridMovement>().moved == false)
            {
                Debug.Log("Moving");
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray, out hit);
                gameManager.selectedUnit.transform.position = new Vector3(hit.point.x, 0, hit.point.z);
                gameManager.selectedUnit.GetComponentInChildren<GridMovement>().moved = true;
                gameManager.selectedUnit.transform.GetChild(0).localScale = new Vector3((float).05, (float).005, (float).05);

            }
        }        
    }

    private void OnMouseOver()
    {
        infoPopulation.selectedCollider = this.gameObject.GetComponent<Collider>();
        infoPopulation.Activate();
    }

    private void OnMouseExit()
    {
        infoPopulation.selectedCollider = null;
        infoPopulation.unitInfoPanel.GetComponent<RectTransform>().localPosition = new Vector3(2000, 1000, 1000);
    }

}
