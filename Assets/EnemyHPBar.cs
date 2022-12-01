using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    public Canvas canvas;
    public Slider slider;
    public float maxHP;
    public float startingHP;
    public float nowHP;

    public void ShowHP(float damage)
    {
        maxHP = this.GetComponent<UnitInfo>().unitMaxHealth;
        startingHP = this.GetComponent<UnitInfo>().unitHealth;
        nowHP = startingHP - damage;
        slider.value = startingHP / maxHP;
        canvas.GetComponent<CanvasGroup>().alpha = 1f;
        Invoke("ChangeHP", 0.5f);
    }


    public void ChangeHP()
    {
        slider.value = nowHP / maxHP;
        Invoke("HideHP", 0.5f);
    }

    public void HideHP()
    {
        canvas.GetComponent<CanvasGroup>().alpha = 0f;
    }
}
