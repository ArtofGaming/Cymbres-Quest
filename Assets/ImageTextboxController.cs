using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageTextboxController : MonoBehaviour
{
    public List<Sprite> PortraitSequence;
    public Image ImageGO;
    public int ImageCounter = 0;
    private int ListCount;

    private void Start()
    {
        ListCount = PortraitSequence.Count;
    }
    public void ChangeImage()
    {
        
        ImageCounter++;
        //If Image counter goes past the index limit of the list, return.
        if (ImageCounter+1 >= PortraitSequence.Count)
        {
            Debug.Log("The PortraitSequence requires more elements");
            return;
        }

        //
        if (PortraitSequence != null && PortraitSequence[ImageCounter] != null)
        {
                ImageGO.gameObject.SetActive(true);
                ImageGO.sprite = PortraitSequence[ImageCounter];
        }
        else
        {
            ImageGO.gameObject.SetActive(false);
        }
    }
}
