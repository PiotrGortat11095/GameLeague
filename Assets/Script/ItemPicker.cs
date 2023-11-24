using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    public float MaxDistance;
    public Camera Cam;
    public string Itemek;
    public Transform Camera;
    public CameraController Controller;
    public InventoryManager ekwipunek;


    private void Update()
    {
        Camera = GameObject.Find("MainCamera").transform;
        Controller = Camera.GetComponent<CameraController>();
        RaycastHit hit;
        Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, MaxDistance + Controller.distance))
        {
            if (hit.transform.TryGetComponent<ThisItem>(out ThisItem item))
            {
                if (Input.GetKeyDown(KeyCode.Z)&& !ekwipunek.IsOpen)
                {
                    NPCInteractable[] npcs = GameObject.FindObjectsOfType<NPCInteractable>();
                    foreach (NPCInteractable npc in npcs)
                    {
                        if (npc.Activequest && npc.ItemName == hit.transform.GetComponent<ThisItem>().przedmiotDoDodania.Name)
                        {
                            npc.Items++;
                            Itemek = npc.ItemName;
                            break;
                        }
                    }
                    
                    if (Itemek != hit.transform.GetComponent<ThisItem>().przedmiotDoDodania.Name)
                    {
                        ekwipunek.DodajPrzedmiot(hit.transform.GetComponent<ThisItem>().przedmiotDoDodania);
                    }
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }
}
