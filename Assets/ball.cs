using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    public Rigidbody fire;
    public Transform spawnPoint;
    public Camera mainCamera;

    public int multipiler;

    public void FireBall()
    {
        Rigidbody fireInstance;
        fireInstance = Instantiate(fire, spawnPoint.position, fire.transform.rotation) as Rigidbody;
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, (Screen.height / 2) + Screen.height / 10));

        fireInstance.AddForce(ray.direction * multipiler);

        fireInstance.gameObject.AddComponent<FireDestroyer>();


    }

}
public class FireDestroyer : MonoBehaviour
{
    private int damage = 3;
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

