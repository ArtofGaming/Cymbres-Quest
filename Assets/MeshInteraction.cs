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
    MeshGeneration meshGeneration;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("Recieved point");
        if (!gameManager.selectedObject.GetComponent<Player>().moved)
        {
            gameManager.selectedObject.transform.position = new Vector3(Mouse.current.position.x.ReadValue(), gameManager.selectedObject.transform.position.y, Mouse.current.position.y.ReadValue());
        }
        else if (!gameManager.selectedObject.GetComponent<Player>().attacked)
        {
            gameManager.AttackSequence();
        }
    }
}
