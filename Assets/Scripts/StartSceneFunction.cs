using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public class StartSceneFunction : MonoBehaviour
{
    #region "vars"
    int x;
    char y;
    string z;
    GameManager manager;
    #endregion

    
    public void ExitGame()
    {

        Application.Quit();
    }
}
