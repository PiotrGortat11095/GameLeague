using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public bool Triggernow = false;
    private float damage;
    private float skilldamage;
    private Player playerScript;
    public CapsuleCollider capsuleCollider;
    public void Start()
    {
        playerScript = FindObjectOfType<Player>();
    }
    public void Update()
    {
        damage = playerScript.damage;
        skilldamage = Mathf.Round(playerScript.damage *13/10);
        if (playerScript.alreadyattack)
        {
            capsuleCollider.enabled = true;
        }
        else
        {
            capsuleCollider.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        AiMobs enemy = other.gameObject.GetComponent<AiMobs>();
        if (enemy != null && playerScript.alreadyattack)
        {
            if (!playerScript.skill1)
            {
                if (Random.value <= playerScript.CriticalHitChance)
                {
                    enemy.TakeDamage(Mathf.Round(damage * playerScript.CriticalHitStrength));
                }
                else
                {
                    enemy.TakeDamage(damage);
                }
            }
            else if (playerScript.skill1)
            {
                if (Random.value <= playerScript.CriticalHitChance)
                {
                    enemy.TakeDamage(Mathf.Round(skilldamage * playerScript.CriticalHitStrength));
                }
                else
                {
                    enemy.TakeDamage(skilldamage);
                }
            }

        }

    }

}
