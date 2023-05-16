using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private int AIdamage = 1;
    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(AIdamage);
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            Destroy(gameObject);
        }
    }
}
