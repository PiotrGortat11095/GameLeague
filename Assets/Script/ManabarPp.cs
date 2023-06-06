using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManabarPp : MonoBehaviour
{
    [SerializeField] private Image manabarSprite;
    [SerializeField] private Text manabarText;
    private Menu menu;
    public Transform MEnu1;
    public void Start()
    {
        MEnu1 = GameObject.Find("Player").transform;
        menu = MEnu1.GetComponent<Menu>();
    }
    public void UpdateManaBar(float Pmana, float PcurrentMana)
    {
        manabarSprite.fillAmount = PcurrentMana / Pmana;
        if (menu.wizard1)
        {
            manabarText.text = "Mana: " + PcurrentMana;
            manabarSprite.color = Color.blue;
        }
        else if (menu.warrior1)
        {
            manabarText.text = "Stamina: " + PcurrentMana;
            manabarSprite.color = Color.green;
        }
    }


}
