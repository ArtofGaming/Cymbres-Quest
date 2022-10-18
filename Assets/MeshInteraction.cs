using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeshInteraction : MonoBehaviour
{

    #region "Vars"
    Ray ray;
    RaycastHit hit;
    public GameObject mesh;
    public Player player;
    public MeshGeneration meshGeneration;
    public GameManager gameManager;
    #endregion

    // grabs gamemanager to keep as a varaible to call back later in the script
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    //checks the players in moving the players if they have not moved then having player attack in they have not yet.
    public void Movement()
    {
        
        Debug.Log("Recieved point");
        if (!gameManager.selectedUnit.GetComponent<Player>().moved)
        {
            gameManager.selectedUnit.transform.position = new Vector3(Mouse.current.position.x.ReadValue(), gameManager.selectedUnit.transform.position.y, Mouse.current.position.y.ReadValue());
        }
        else if (!gameManager.selectedUnit.GetComponent<Player>().attacked)
        {
            gameManager.UnitAttack();
        }
    }
}
