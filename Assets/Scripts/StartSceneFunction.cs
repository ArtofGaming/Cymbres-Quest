using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//
public class StartSceneFunction : MonoBehaviour
{
    private bool OptionIsOn = false;
    public GameObject OptionsPanel;
    public GameObject MainMenu;
    public AudioManager audioManager;

    public void NewGamePressed()
    {
        Debug.Log("New Game to be started.");
        audioManager.LeavingStartScreen();
        SceneManager.LoadScene("IntroNarrativeScene");
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
            MainMenu.SetActive(false);
            OptionsPanel.SetActive(true);
            Debug.Log("Options are now to be shown");
        }
        else if(OptionIsOn == true)
        {
            OptionIsOn=false;
            MainMenu.SetActive(true);
            OptionsPanel.SetActive(false);  
            Debug.Log("Options are now turned off");
        }

    }

    public void StartScreenPressed()
    {
        Debug.Log("Start Menu now starting.");
        SceneManager.LoadScene("StartScene");
    }

    public void ExitGame()
    {
        Debug.Log("Game is exiting now.");
        Application.Quit();
    }
}
