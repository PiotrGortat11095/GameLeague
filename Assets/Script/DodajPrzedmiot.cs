using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodajPrzedmiot : MonoBehaviour
{
    public bool czyDodac;
    public Item przedmiotDoDodania;
    public InventoryManager ekwipunek;

    void PodniesPrzedmiot()
    {
        ekwipunek.DodajPrzedmiot(przedmiotDoDodania);
        czyDodac = false;
    }
    private void Update()
    {
        if(czyDodac == true)
        {
            PodniesPrzedmiot();
        }
    }
}
