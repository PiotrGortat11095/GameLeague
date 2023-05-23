using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Expbar : MonoBehaviour
{
    [SerializeField] private Image expbarSprite;
    [SerializeField] private Text expbarText;
    [SerializeField] private Text levelText;

    public void UpdateExpBar(float exp, float currentexp)
    {
        expbarSprite.fillAmount = currentexp / exp;
        expbarText.text = currentexp + " / " + exp;
    }
    public void UpdateLevel(float lvl)
    {
        levelText.text = lvl.ToString();
    }


}
