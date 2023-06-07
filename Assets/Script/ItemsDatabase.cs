using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDatabase : MonoBehaviour
{
    public List<Item> Items = new List<Item>();
    public List<Item> PlayerItems = new List<Item>();

    public static ItemsDatabase Instance;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }


}
[System.Serializable]
public class Item
{
    public enum ItemType
    {
        Weapon, Food, Armor
    }

    public string Name;
    public string Description;
    public int ID;
    public ItemType Type;
    public Sprite Icon;

    public Item(string Name, string Description, int ID, ItemType Type, Sprite Icon)
    {
        this.Name = Name;
        this.Description = Description;
        this.ID = ID;
        this.Type = Type;
        this.Icon = Icon;
    }

}
