using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/NewItem")]
public class Item : ScriptableObject
{

    public enum ItemType
    {
        Weapon, Food, Chest_armor, Leg_armor, Helmet, Boots, Gloves, Ring, Necklace
    }
    public string Name;
    public Sprite Description;
    public int ID;
    public ItemType Type;
    public Sprite Icon;


    public int AttackDamage;
    public float CriticalHitStrength;
    public int NutritionValue;
    public int ArmorValue;
    public int strength;
    public int intellect;
    public float CriticalHitChance;

}

