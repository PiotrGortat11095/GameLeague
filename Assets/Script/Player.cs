using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] HealthbarP Phealthbar;
    [SerializeField] ManabarPp Pmanabar;
    public float Phealth = 10;
    public float Pmana = 10;
    private float PcurrentHealth;
    public float PcurrentMana;
    public float HealthRegeneration = 5f;
    public float ManaRegeneration = 5f;

    private void Start()
    {
        PcurrentHealth = Phealth;
        PcurrentMana = Pmana;
        InvokeRepeating("RegenerateMana", ManaRegeneration, ManaRegeneration);
        InvokeRepeating("RegenerateHealth", HealthRegeneration, HealthRegeneration);
    }
    private void Update()
    {
        Phealthbar.UpdateHealthBar(Phealth, PcurrentHealth);
        Pmanabar.UpdateManaBar(Pmana, PcurrentMana);
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
    public void Mana(int mana)
    {
        PcurrentMana -= mana;
    }
    private void RegenerateHealth()
    {
        if (PcurrentHealth < Phealth)
        {
            PcurrentHealth++;
        }
    }
    private void RegenerateMana()
    {
        if (PcurrentMana < Pmana)
        {
            PcurrentMana++;
        }
    }

}
