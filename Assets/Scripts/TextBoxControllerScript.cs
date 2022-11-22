using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // for TMP text UI
using System.IO; //File IO for text file scripts
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextBoxControllerScript : MonoBehaviour
{
    #region "Vars"
    public TextMeshProUGUI DialogueTextMesh;
    public TextMeshProUGUI CharNameTextMesh;
    public TextAsset TextFile;
    public float TextTimeDelay = .125f;
    public Button NextTextButton;
    public Button ContinueTextButton;
    //DO we need a button to go to the next scene?
    //public Button CloseUIButton;
    private string CharacterName;
    private string DialogueText;
    private StreamReader textReader;


    #endregion

    public void Awake()
    {
        NextTextButton.gameObject.SetActive(false);
        ContinueTextButton.gameObject.SetActive(false);
    }
    private void Start()
    {
        StartText();
    }

    public void StartText()
    {
        string path = "Assets/Resources/Narrative/" + TextFile.name +".txt";
        //Read the text from directly from the test.txt file
        textReader = new StreamReader (Path.Combine(Application.streamingAssetsPath, "IntroductionTextFile.txt"));

        //textReader = new StreamReader(Path.Combine(Application.streamingAssetsPath, TextFile.name));
        GrabNextText();
    }

    public void GrabNextText()
    {
        NextTextButton.gameObject.SetActive(false);

        if (textReader != null && !textReader.EndOfStream)
        {

            CharacterName = textReader.ReadLine();
            DialogueText = textReader.ReadLine();

            CharNameTextMesh.text = CharacterName;
            DialogueTextMesh.text = "";

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
        NextTextButton.gameObject.SetActive(false);
        ContinueTextButton.gameObject.SetActive(true);

    }
        IEnumerator PlayText()
    {

        foreach(char c in DialogueText)
        {
            DialogueTextMesh.text += c;
            yield return new WaitForSeconds(TextTimeDelay);
        }

        NextTextButton.gameObject.SetActive(true);
    }
}