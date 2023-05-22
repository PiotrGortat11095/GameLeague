using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarP : MonoBehaviour
{
    [SerializeField] private Image healthbarSprite;
    [SerializeField] private Text healthbarText;

    public void UpdateHealthBar(float Phealth, float PcurrentHealth)
    {
        healthbarSprite.fillAmount = PcurrentHealth / Phealth;
        healthbarText.text = "Health: " + PcurrentHealth;
    }


}


