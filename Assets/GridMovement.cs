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
    private float movementReach = 4;
    public bool moved = false;
    public Material movementMaterial;
    public bool attackPossible;
    public string unitClass;
    //public Customization customization;
    GameManager gameManager;
    public bool added = false;
    int unitsMoved = 0;
    public Collider collider;
    public InfoPopulation infoPopulation;

    public SmallTextScript text;

    //show movement and other actions possible

    // Start is called before the first frame update
    void Start()
    {
        attackPossible = true;
        //customization = GameObject.Find("boss").GetComponent<Customization>();
        gameManager = GameObject.Find("boss").GetComponent<GameManager>();
        gameManager.selectedUnit = gameManager.aliveUnits[0];
        infoPopulation = this.gameObject.GetComponent<InfoPopulation>();
        text = FindObjectOfType<SmallTextScript>();
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        
            gameManager.selectedUnit = this.transform.gameObject;
        Debug.Log(gameManager.selectedUnit);
        if (gameManager.selectedUnit.tag == "Player" && gameManager.whoseTurn == "Player" && selected != "player")
        {
            Debug.Log("Mouse Down");
            // Calculate the squares that unit can move to
            movementReach = gameManager.selectedUnit.GetComponent<UnitInfo>().unitMovementSpeed;
            // Show area that is movable
            gameManager.selectedUnit.transform.GetChild(0).localScale = new Vector3((float)movementReach, (float).002, (float)movementReach);
            myCollider = gameManager.selectedUnit.GetComponentInChildren<MeshCollider>();
            //myCollider.gameObject.GetComponentInChildren<Renderer>().material = movementMaterial;
            selected = "player";
            gameManager.gridMovement = gameManager.selectedUnit.GetComponent<GridMovement>();
            text.TextTrigger();
        }
        else
        {
            // if point selected is in allowed range and unit has not moved yet
            if (gameManager.selectedUnit.GetComponent<GridMovement>().moved == false)
            {
                Debug.Log("Moving");
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray, out hit);
                gameManager.selectedUnit.transform.position = new Vector3(hit.point.x, 0, hit.point.z);
                gameManager.selectedUnit.GetComponentInChildren<GridMovement>().moved = true;
                gameManager.selectedUnit.transform.GetChild(0).localScale = Vector3.zero;
                text.TextTrigger();

            }
        }        
    }

    private void OnMouseOver()
    {
        infoPopulation.selectedCollider = this.gameObject.GetComponent<Collider>();
        Debug.Log(infoPopulation.selectedCollider.transform.position);
        //infoPopulation.Activate();
    }

    private void OnMouseExit()
    {
        infoPopulation.selectedCollider = null;
        //infoPopulation.unitInfoPanel.GetComponent<RectTransform>().localPosition = new Vector3(2000, 1000, 1000);
    }

}
