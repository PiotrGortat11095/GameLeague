using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image _healthbarSprite;

    public Transform target; 

    void Update()
    {
        if (target != null)
        {
            
            transform.LookAt(target);
        }
    }

public void UpdateHealthBar(float health, float currentHealth)
    {
        _healthbarSprite.fillAmount = currentHealth / health;
    }
}
