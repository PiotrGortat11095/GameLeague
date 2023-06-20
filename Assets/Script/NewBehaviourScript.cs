using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Camera Camera;

    public void Update()
    {
        transform.LookAt(Camera.main.transform.position);
        Vector3 rotationInEuler = transform.rotation.eulerAngles;
        rotationInEuler.x = 0;
        transform.rotation = Quaternion.Euler(rotationInEuler);
    }
}
