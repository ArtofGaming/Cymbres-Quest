using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//
public class StartSceneFunction : MonoBehaviour
{
    private bool OptionIsOn = false;
    public GameObject OptionsPanel;

    public void NewGamePressed()
    {
        Debug.Log("New Game to be started.");
        SceneManager.LoadScene("Customization");
    }

    public void LoadGamePressed()
    {
        Debug.Log("This has not been implemented yet.");
    }

    public void OptionsToggle()
    {
        if(OptionIsOn == false)
        {
            OptionIsOn = true;
            OptionsPanel.SetActive(true);
            Debug.Log("Options are now to be shown");
        }
        else if(OptionIsOn == true)
        {
            OptionIsOn=false;
            OptionsPanel.SetActive(false);  
            Debug.Log("Options are now turned off");
        }

    }
    public void ExitGame()
    {
        Debug.Log("Game is exiting now.");
        Application.Quit();
    }
}
