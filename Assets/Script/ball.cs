using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.Build;
using UnityEngine;

public class ball : MonoBehaviour
{
    public Rigidbody fire;
    public Rigidbody magma;
    public Rigidbody meteor;
    public Transform spawnPoint;
    public Transform player;
    public Camera mainCamera;
    public float magmaLifetime = 5f;
    private Player playerScript;
    public LayerMask ThisLayers;
    public bool Enemy;

    public int multipiler;
    public float maxMeteorDistance = 30f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerScript = player.GetComponent<Player>();
    }
    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out _, maxMeteorDistance, ThisLayers))
        {
            Enemy = true;
        }
        else
        {
            Enemy = false;
        }
    }
    public void FireBall()
    {
        Rigidbody fireInstance;
        fireInstance = Instantiate(fire, spawnPoint.position, fire.transform.rotation) as Rigidbody;

        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y + 10, mainCamera.transform.position.z));
        Vector3 direction = worldMousePosition - spawnPoint.position;

        fireInstance.AddForce(direction.normalized * multipiler);
        fireInstance.gameObject.AddComponent<FireDestroyer>();
    }
    public int mana;

    public void MagmaWall()
    {
        if (playerScript.PcurrentMana >= 5)
        {
            mana = 5;
        playerScript.Mana(mana);

            Rigidbody magmaInstance;
            Vector3 spawnPoint2 = player.position + player.forward * 2f;
            magmaInstance = Instantiate(magma, spawnPoint2, magma.transform.rotation) as Rigidbody;
            magmaInstance.transform.rotation = Quaternion.Euler(0f, mainCamera.transform.rotation.eulerAngles.y, 0f);
            Destroy(magmaInstance.gameObject, magmaLifetime);
        }
    }


    public void Meteor()
    {
        if (playerScript.PcurrentMana >= 10)
        {

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxMeteorDistance, ThisLayers))
            {
                mana = 10;
                playerScript.Mana(mana);
                Vector3 targetPosition = hit.point;
                targetPosition.y += 11f;

                if (Vector3.Distance(targetPosition, player.position) > maxMeteorDistance)
                {
                    targetPosition = player.position + (targetPosition - player.position).normalized * maxMeteorDistance;
                }


                Rigidbody meteorInstance;
                meteorInstance = Instantiate(meteor, targetPosition, meteor.transform.rotation) as Rigidbody;
                meteorInstance.gameObject.AddComponent<FireDestroyer2>();
                meteorInstance.AddForce(0, -1500000000, 0);
                Destroy(meteorInstance.gameObject, magmaLifetime);
            }

        }

    }
}

public class FireDestroyer : MonoBehaviour
{
    private Player playerScript;
    private int damage;

    private void Start()
    {
        playerScript = FindObjectOfType<Player>();
        damage = 10 + playerScript.damageboost * 2;
    }

    private void OnCollisionEnter(Collision collision)
    {
        AiMobs enemy = collision.gameObject.GetComponent<AiMobs>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            Destroy(gameObject);
        }
    }
}

public class FireDestroyer2 : MonoBehaviour
{
    private Player playerScript;
    private int damage;
    private void Start()
    {
        playerScript = FindObjectOfType<Player>();
        damage = 25 + playerScript.damageboost * 4;
    }

    private void OnCollisionEnter(Collision collision)
    {
        AiMobs enemy = collision.gameObject.GetComponent<AiMobs>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }
}


