using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // for TMP text UI
using System.IO; //File IO for text file scripts
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SmallTextScript : MonoBehaviour
{

    public int counter = 0;
    public int startingCounter = 0;
    public int postAttackCounter = 0;

    #region "Vars"
    public TextMeshProUGUI DialogueTextMesh;
    public TextMeshProUGUI CharNameTextMesh;
    public TextAsset TextFile;
    public float TextTimeDelay = .01f;
    //DO we need a button to go to the next scene?
    //public Button CloseUIButton;
    private string CharacterName;
    private string DialogueText;
    private StreamReader textReader;
    float timer = 1f;
    private bool SkipButtonHasBeenPressed = false;


    #endregion

    private void Start()
    {
        StartText();
    }

    public void TextTrigger()
    {
        if(counter == 0 || counter == 1 || counter == 5)
        {
            GrabNextText();
        }
        counter++;
    }

    public void StartText()
    {
        string path = "Assets/Resources/Narrative/" + TextFile.name + ".txt";
        //Read the text from directly from the test.txt file
        textReader = new StreamReader(Path.Combine(Application.streamingAssetsPath, "TestFile1.txt"));

        //textReader = new StreamReader(Path.Combine(Application.streamingAssetsPath, TextFile.name));
        GrabNextText();
    }

    public void GrabNextText()
    {
        FindObjectOfType<AudioManager>().buttonNoise.Play();

        if (textReader != null && !textReader.EndOfStream)
        {

            CharacterName = textReader.ReadLine();
            DialogueText = textReader.ReadLine();

            CharNameTextMesh.text = CharacterName;
            DialogueTextMesh.text = "";

            gameObject.GetComponent<ImageTextboxController>().ChangeImage();

            StartCoroutine(PlayText());

        }
        else
        {
            EndText();
        }


    }

    public void EndText()
    {
        textReader.Close();

    }
    IEnumerator PlayText()
    {

        foreach (char c in DialogueText)
        {
            {
                DialogueTextMesh.text += c;
                yield return new WaitForSeconds(TextTimeDelay);
            }

        }
        if(startingCounter < 3)
        {
            startingCounter++;
            Invoke("GrabNextText", 4.0f);
        } else if(postAttackCounter < 2 && counter == 2)
        {
            postAttackCounter++;
            Invoke("GrabNextText", 4.0f);
        } else if (counter == 5)
        {
            Invoke("GrabNextText", 4.0f);
        }
    }
}
