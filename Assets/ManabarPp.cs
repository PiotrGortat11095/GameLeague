using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManabarPp : MonoBehaviour
{
    [SerializeField] private Image manabarSprite;
    [SerializeField] private Text manabarText;

    public void UpdateManaBar(float Pmana, float PcurrentMana)
    {
        manabarSprite.fillAmount = PcurrentMana / Pmana;
        manabarText.text = "Mana: " + PcurrentMana;
    }


}
