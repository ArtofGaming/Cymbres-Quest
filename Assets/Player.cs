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
    public Ray2D ray2d;
    public RaycastHit2D hit2d;
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
            //change this up to become more of gamemanager's responsibility
            ray = Camera.main.ScreenPointToRay(new Vector3 (Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), 0));
            Physics.Raycast(ray, out hit);
            // Debug.Log(gameManager.selectedObject);
            // Debug.Log(hit.collider);
            // Debug.Log(this.moved);
            Debug.DrawRay(ray.origin, Vector3.forward, Color.red, 100);
            if (gameManager.selectedObject != this.gameObject && hit.collider != null && !this.moved)
            {
                gameManager.selectedObject = this.gameObject;
                meshGeneration.circleHolder = this.gameObject;

                meshGeneration.GenerateMovementMesh();
                meshGeneration.meshCollider.convex = true;

            }
            else if (hit.collider == gameManager.selectedObject.GetComponentInChildren<MeshCollider>() && hit.collider != null && !this.moved)
            {
                //Need to adjust to highlight sections of the available grid
                ray = Camera.main.ScreenPointToRay(new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), 0));
                Physics.Raycast(ray, out hit);
                gameManager.selectedObject.transform.position = hit.point;
                this.moved = true;
                gameManager.ActionMenu();
            }
            else if (gameManager.selectedObject == this.gameObject && this.moved && !attacked)
            {
                Debug.Log("AttackMesh generated");
                ray = Camera.main.ScreenPointToRay(new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), 0));
                Physics.Raycast(ray, out hit);
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
