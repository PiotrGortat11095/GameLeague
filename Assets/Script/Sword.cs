using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public bool Triggernow = false;
    public int damage = 7;
    private Player playerScript;
    public void Start()
    {
        playerScript = FindObjectOfType<Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        AiMobs enemy = other.gameObject.GetComponent<AiMobs>();
        if (enemy != null && playerScript.alreadyattack)
        {
            enemy.TakeDamage(damage);
            Debug.Log("elo1");
        }
        Debug.Log(enemy);
    }
}
