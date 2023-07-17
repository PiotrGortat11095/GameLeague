using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillPrefab : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    static public GameObject ThisSkill;
    [SerializeField] private Image CD;
    static public bool ifDrop;
    static public float CDTime;
    static public float currentCDTime;
    static public bool skillbool;
    static public bool SkillP = true;
    GameObject Skill;
    public bool CDbool;
    public void Start()
    {
        CDbool = true;
        CDTime = 500;
        currentCDTime = 0;
    }
    public void Update()
    {
            Invoke(nameof(CD2), 1/2);

        CD.fillAmount = currentCDTime / CDTime;
        if(currentCDTime == 0)
        {
            skillbool = true;
        }
        else
        {
            skillbool = false;
        }
    }

    public void CD2()
    {
        if(currentCDTime > 0)
        {
            currentCDTime-= 2;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        ThisSkill = this.gameObject;
        Transform Skillposition = GameObject.Find("Canvas").transform;
        ThisSkill.transform.SetParent(Skillposition, false);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        ifDrop = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (ifDrop == false)
        {
            Destroy(this.gameObject);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

}
