using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image _healthbarSprite;

    public Transform target; // The target object to look at

    void Update()
    {
        if (target != null) // Check if the target is not null
        {
            // Rotate this object to look at the target
            transform.LookAt(target);
        }
    }

public void UpdateHealthBar(float health, float currentHealth)
    {
        _healthbarSprite.fillAmount = currentHealth / health;
    }
}
