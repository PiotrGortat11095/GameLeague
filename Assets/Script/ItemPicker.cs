using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    public float MaxDistance;
    public Camera Cam;
    public InventoryManager ekwipunek;

    private void Update()
    {
        RaycastHit hit;
        Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, MaxDistance))
        {
            if (hit.transform.TryGetComponent<ThisItem>(out ThisItem item))
            {
                if (Input.GetMouseButtonDown(1))
                {
                    ekwipunek.DodajPrzedmiot(hit.transform.GetComponent<ThisItem>().przedmiotDoDodania);
                    Destroy(item.gameObject);
                }
            }
        }
    }
}
