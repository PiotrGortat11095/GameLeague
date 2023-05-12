using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    
    public Rigidbody fire;
    public Transform spawnPoint;

    public int multipiler;

    public void FireBall()
    {
        Rigidbody fireInstance;
        fireInstance = Instantiate(fire, spawnPoint.position, fire.transform.rotation) as Rigidbody;
        fireInstance.AddForce(spawnPoint.forward * multipiler);

    }
}
