using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FloatingText : MonoBehaviour
{
    public float DestroyTime = 3f;

    void Start()
    {
        Destroy(gameObject, DestroyTime);
    }


}
