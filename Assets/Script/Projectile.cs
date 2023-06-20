using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private AiMobs aimobs;
    public GameObject monster;

    private void OnTriggerEnter(Collider other)
    {
        aimobs = monster.GetComponent<AiMobs>();
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null && !player.alreadyblock && !player.skill1)
        {
            player.TakeDamage(aimobs.AIdamage);
            Destroy(gameObject);
        }
        else if (player != null && player.alreadyblock)
        {
            player.Mana(aimobs.AIdamage / 3);
            Destroy(gameObject);
        }
        else if(player != null && player.skill1) 
        {
            player.TakeDamage(aimobs.AIdamage / 2);
            Destroy(gameObject);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Terrain") || other.gameObject.layer == LayerMask.NameToLayer("MagmaWall"))
        {
            Destroy(gameObject);
        }
    }
}
