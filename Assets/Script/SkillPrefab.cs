using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillPrefab : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    static public GameObject ThisSkill;
    static public bool ifDrop;
    static public bool SkillP = true;
    GameObject Skill;
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
