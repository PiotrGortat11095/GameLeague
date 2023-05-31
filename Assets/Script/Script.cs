using System.Collections;
using System.Collections.Generic;
using System.Drawing.Imaging;
using UnityEngine;

public class Script : MonoBehaviour
{
    public GameObject Player;
    private Menu menu;
    void Update()
    {
        menu = Player.GetComponent<Menu>();
        menu.enabled = true;
    }
}
