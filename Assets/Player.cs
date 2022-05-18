using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public bool moved = false;
    public MeshGeneration meshGeneration;
    public bool attacked;
    public RaycastHit hit;
    public Ray ray;
    public GameManager gameManager;
    public MeshInteraction meshInteraction;
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
        if (context.started)
        {
            ray = Camera.main.ScreenPointToRay(new Vector3 (Mouse.current.position.x.ReadValue(),Mouse.current.position.y.ReadValue(),0));
            Physics.Raycast(ray, out hit);
            //ray.direction = -ray.direction;
            //Debug.Log("Moved");

            Debug.Log(gameManager.selectedObject);
            Debug.Log(hit.collider);
            Debug.Log(this.moved);
            if (gameManager.selectedObject != this.gameObject && hit.collider != null && !this.moved)
            {
                gameManager.selectedObject = this.gameObject;
                meshGeneration.circleHolder = this.gameObject;

                meshGeneration.GenerateMovementMesh();
                

            }
            else if (gameManager.selectedObject == this.gameObject && hit.collider != null)
            {

            }
            else if (gameManager.selectedObject != this && this.moved && !attacked)
            {
                Debug.Log("AttackMesh generated");
                ray = Camera.main.ScreenPointToRay(new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), 0));
                Physics.Raycast(ray, out hit);
                ray.direction = -ray.direction;
                GameObject.Destroy(meshGeneration.mesh);
                gameManager.selectedObject = this.gameObject;
                meshGeneration.circleHolder = this.gameObject;
                meshGeneration.GenerateAttackMesh();
            }
        }
        else if (context.performed)
        {
            
        }
        
        

        
    }
}
