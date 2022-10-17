using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeshInteraction : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    public GameObject mesh;
    public Player player;
    public MeshGeneration meshGeneration;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
