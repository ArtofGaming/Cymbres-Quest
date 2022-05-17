using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    public bool moved;
    public GameObject selectedObject;
    public MeshGeneration meshGeneration;
    public bool attacked;
    public RaycastHit hit;
    public Ray ray;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Yes()
    {
        Debug.Log("Yes");
    }
    void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("Moved");
        if (selectedObject != this && Mouse.current.position.ReadValue() == new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y) && !this.moved)
        {
            selectedObject = this.gameObject;
            meshGeneration.circleHolder = this.gameObject;
            Physics.Raycast(ray, out hit);
            meshGeneration.GenerateMovementMesh();

        }
        else if (selectedObject != this && moved && !attacked)
        {
            selectedObject = this.gameObject;
            meshGeneration.circleHolder = this.gameObject;
            Physics.Raycast(ray, out hit);
            meshGeneration.GenerateAttackMesh();
        }



    }
}
