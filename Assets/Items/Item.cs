using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NowyPrzedmiot",menuName = "Ekwipunek/NowyPrzedmiot")]
public class Item : ScriptableObject
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


}
