using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] HealthbarP Phealthbar;
    public float Phealth = 10;
    private float PcurrentHealth;

    private void Start()
    {
        PcurrentHealth = Phealth;
    }
    private void Update()
    {
        Phealthbar.UpdateHealthBar(Phealth, PcurrentHealth);
    }
    public void TakeDamage(int AIdamage)
    {
        PcurrentHealth -= AIdamage;
        if (PcurrentHealth <= 0)
        {
            transform.position = new Vector3(209, 112, 396);
            PcurrentHealth = Phealth;
        }
    }

}
