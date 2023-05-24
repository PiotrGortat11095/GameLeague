using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int AIdamage = 7;

    private void OnTriggerEnter(Collider other)
    {

        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(AIdamage);
            Destroy(gameObject);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Terrain") || other.gameObject.layer == LayerMask.NameToLayer("MagmaWall"))
        {
            Destroy(gameObject);
        }
    }
}
