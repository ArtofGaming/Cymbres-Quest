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
    public int counter1 = 0;
    public int counter2 = 0;
    public bool check = false;
    public bool check2 = false;

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


    #endregion

    private void Start()
    {
        StartText();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (check2 == true)
            {
                check = true;
            }
            else if (check2 == false && counter < 3)
            {
                GrabNextText();
                counter++;
            } else if (counter2 == 2 || counter2 == 5)
            {
                GrabNextText();
                counter2++;
            }
        }
    }

    public void TextTrigger()
    {
        if(counter1 < 2)
        {
            GrabNextText();
            counter1++;
        }
    }

    public void DamageTrigger()
    {
        if(counter2 ==  0)
        {
            counter2++;
            GrabNextText();
        } else if (counter2 == 3)
        {
            counter2++;
        } else if (counter2 == 4)
        {
            GrabNextText();
        }
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
            check2 = true;
            StartCoroutine(PlayText());
            check2 = false;

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
            if (check == true)
            {
                DialogueTextMesh.text = DialogueText;
                check = false;
                yield break;
            }
            else
            {
                DialogueTextMesh.text += c;
                yield return new WaitForSeconds(TextTimeDelay);
            }
        }
        if(counter2 == 1 || counter2 == 4)
        {
            counter2++;
        }
    }
}
