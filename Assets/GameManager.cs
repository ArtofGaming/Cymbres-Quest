using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject selectedObject;
    // Start is called before the first frame update
    void Start()
    {
        selectedObject = GameObject.Find("DummySO");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActionMenu()
    {

    }

    public void AttackSequence()
    {

    }
}
