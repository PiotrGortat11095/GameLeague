using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public bool Triggernow = false;
    public int damage = 7;
    private Player playerScript;
    public CapsuleCollider capsuleCollider;
    public void Start()
    {
        playerScript = FindObjectOfType<Player>();
    }
    public void Update()
    {
        damage = 10 + playerScript.damageboost * 2;
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
                enemy.TakeDamage(damage);
            }
            else if (playerScript.skill1)
            {
                enemy.TakeDamage(damage+damage/2);
            }

        }

    }

}
