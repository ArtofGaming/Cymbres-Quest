using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region "Player Vars"
    public bool moved = false;
    public MeshGeneration meshGeneration;
    public bool attacked;
    public RaycastHit hit;
    public Ray ray;
    public Ray2D ray2d;
    public RaycastHit2D hit2d;
    public GameManager gameManager;
    public MeshInteraction meshInteraction;
    #endregion


    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started)
        { 
            //This is to send out a ray which will give back information on what it has hit
            //change this up to become more of gamemanager's responsibility
            ray = Camera.main.ScreenPointToRay(new Vector3 (Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), 0));
            Physics.Raycast(ray, out hit);
            Debug.DrawRay(ray.origin, Vector3.forward, Color.red, 100);

            //if the object gamemanager has selected is not the gameobject that has been hit, make the selected this object
            #region "If ray hit object not selected..."
            if (gameManager.selectedUnit != this.gameObject && hit.collider != null && !this.moved)
            {
                gameManager.selectedUnit = this.gameObject;
                meshGeneration.circleHolder = this.gameObject;

                meshGeneration.GenerateMovementMesh();
                meshGeneration.meshCollider.convex = true;

            }
            #endregion

            //Or if the object gamemanager has selected is not the game object that has been hit & found it is a child GO, make the selected this object
            #region "Else If ray hit child & not selected..."
            else if (hit.collider == gameManager.selectedUnit.GetComponentInChildren<MeshCollider>() && hit.collider != null && !this.moved)
            {
                //Need to adjust to highlight sections of the available grid
                ray = Camera.main.ScreenPointToRay(new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), 0));
                Physics.Raycast(ray, out hit);
                gameManager.selectedUnit.transform.position = hit.point;
                this.moved = true;
                //gameManager.ActionMenu();
            }
            #endregion

            //If game mamanger selected is this object, has moved, but not attacked...goes into attack mode
            #region "Else If selected hasn't attacked, but moved..."
            else if (gameManager.selectedUnit == this.gameObject && this.moved && !attacked)
            {
                Debug.Log("AttackMesh generated");
                ray = Camera.main.ScreenPointToRay(new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), 0));
                Physics.Raycast(ray, out hit);
                GameObject.Destroy(meshGeneration.mesh);
                gameManager.selectedUnit = this.gameObject;
                meshGeneration.circleHolder = this.gameObject;
                meshGeneration.GenerateAttackMesh();
            }
            #endregion
        }
        else if (context.performed)
        {
            //Do nothing
        }  
    }
}
