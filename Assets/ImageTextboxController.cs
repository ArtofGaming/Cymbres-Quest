using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageTextboxController : MonoBehaviour
{
    public List<Sprite> PortraitSequence;
    public Image ImageGO;
    public int ImageCounter = 0;

    public void ChangeImage()
    {
        ImageCounter++;
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
