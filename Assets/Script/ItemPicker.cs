using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    public float MaxDistance;
    public Camera Cam;

    private void Update()
    {
        if (InventoryManager.Instance.IsOpen) return;
        RaycastHit hit;
        Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, MaxDistance))
        {
            if(hit.transform.TryGetComponent<ItemObject>(out ItemObject item)) 
            {
                if(Input.GetMouseButtonDown(1))
                {
                    ItemsDatabase.Instance.PlayerItems.Add(ItemsDatabase.Instance.Items[item.ItemID]);
                    Destroy(item.gameObject);
                }
            }
        }
    }
}
