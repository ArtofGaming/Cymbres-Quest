using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider slider;

    public AudioSource mainTheme;
    public AudioSource otherTheme;
    public AudioSource battleTheme;
    public AudioSource buttonNoise;

    public float volume = 0.5f;
    public float volumeInterval;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        slider.value = volume;

    }

    public void ChangeVolume()
    {
        volume = slider.value;
        mainTheme.volume = volume;
    }

    public void LeavingStartScreen()
    {
        volumeInterval = volume / 10;
        otherTheme.Play();
        buttonNoise.volume = volume;
        StartCoroutine(CrossFadeMainTheme());
        mainTheme.Stop();
    }

    public void LeavingCustomScene()
    {
        battleTheme.Play();
        StartCoroutine(CrossFadeCustomTheme());
        otherTheme.Stop();
    }

    IEnumerator CrossFadeMainTheme()
    {
        for (float i = 0f; i < 1f; i += 0.1f)
        {
            mainTheme.volume -= volumeInterval;
            otherTheme.volume += volumeInterval;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator CrossFadeCustomTheme()
    {
        for (float i = 0f; i < 1f; i += 0.1f)
        {
            otherTheme.volume -= volumeInterval;
            battleTheme.volume += volumeInterval/2;
            yield return new WaitForSeconds(0.1f);
        }
    }


}
