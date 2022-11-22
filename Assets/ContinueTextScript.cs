using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueTextScript : MonoBehaviour
{

    public void CustomizationScenePressed()
    {
        SceneManager.LoadScene("Customization");
    }

}
