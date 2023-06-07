using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int AIdamage = 7;

    private void OnTriggerEnter(Collider other)
    {

        Player player = other.gameObject.GetComponent<Player>();
        if (player != null && !player.alreadyblock && !player.skill1)
        {
            player.TakeDamage(AIdamage);
            Destroy(gameObject);
        }
        else if (player != null && player.alreadyblock)
        {
            player.Mana(AIdamage / 3);
            Destroy(gameObject);
        }
        else if(player != null && player.skill1) 
        {
            player.TakeDamage(AIdamage / 2);
            Destroy(gameObject);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Terrain") || other.gameObject.layer == LayerMask.NameToLayer("MagmaWall"))
        {
            Destroy(gameObject);
        }
    }
}
