using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public HealthbarP Phealthbar;
    public ManabarPp Pmanabar;
    public Expbar Pexpbar;
    public float Phealth = 100;
    public float Pmana = 100;
    public float exp = 5;
    public bool alreadyattack = false;
    public bool Activequest = false;
    [HideInInspector]public int damageboost = 0;
    [HideInInspector]public float lvl = 1;
    private float expp;
    [HideInInspector]public float currentexp = 0;
    private float PcurrentHealth;
    private int i = 1;
    public float PcurrentMana;
    [HideInInspector]public float HealthRegeneration = 2f;
    [HideInInspector]public float ManaRegeneration = 1f;
    public Camera mainCamera;
    public LayerMask ThisLayers;
    public bool visible;

    private void Start()
    {

        PcurrentHealth = Phealth;
        PcurrentMana = Pmana;
        InvokeRepeating("RegenerateMana", ManaRegeneration, ManaRegeneration);
        InvokeRepeating("RegenerateHealth", HealthRegeneration, HealthRegeneration);
    }
    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, ThisLayers))
        {
            AiMobs enemy = hit.collider.GetComponent<AiMobs>();
            if (enemy != null)
            {
                if (enemy.Boss)
                {
                    visible = true;
                }
            }
            
        }
        else
        {
            visible = false;
        }
        Phealthbar.UpdateHealthBar(Phealth, PcurrentHealth);
        Pmanabar.UpdateManaBar(Pmana, PcurrentMana);
        Pexpbar.UpdateExpBar(exp, currentexp);
        Pexpbar.UpdateLevel(lvl);
        if (currentexp == exp)
        {
            exp = exp * 4 / 3;
            currentexp = 0;
            lvl++;
        }
        else if(currentexp > exp) 
        {
            expp = currentexp - exp;
            expp = Mathf.Round(expp * 100f) / 100f;
            exp = exp * 4 / 3;
            currentexp = expp;
            lvl++;
        }
        if(i < lvl)
        {
            Phealth += 5;
            Pmana += 2;
            i++;
            PcurrentHealth = Phealth;
            PcurrentMana = Pmana;
            damageboost += 1;
        }
        exp = Mathf.Round(exp * 100f) / 100f;
    }
    public void AttackStart()
    {
        alreadyattack = true;
    }
    public void AttackEnd()
    {
        alreadyattack = false;
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
