using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KanpsackItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private RectTransform rectTransform;
    private Transform transParent;
    private CanvasGroup canvasGrounp;

    void Awake()
    {
        rectTransform = this.GetComponent<RectTransform>();
        canvasGrounp = this.GetComponent<CanvasGroup>();
    }




    /// <summary>
    /// 当开始拖拽的时刻执行此方法
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        print("开始拖拽");
        rectTransform.position = eventData.position;
        transParent = rectTransform.parent;
        rectTransform.parent = transParent.parent;
        canvasGrounp.blocksRaycasts = false;

    }



    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }



    public void OnEndDrag(PointerEventData eventData)
    {
        print("结束拖拽");

        if (eventData.pointerEnter == null)
        {
            rectTransform.parent = transParent;
            rectTransform.localPosition = Vector3.zero;
            canvasGrounp.blocksRaycasts = true;
            return;
        }
        //获取鼠标在拖拽结束后检测到的格子对象
        GameObject pointerObj = eventData.pointerEnter;




        if (pointerObj.tag == "Check")
        {
            if (pointerObj.transform.childCount > 0)
            {
                Transform m_tempParent = pointerObj.transform;
                pointerObj.transform.GetChild(0).parent = transParent;
                this.transform.parent = m_tempParent;

                this.transform.localPosition = Vector3.zero;
                pointerObj.transform.GetChild(0).localPosition = Vector3.zero;
            }
            else
            {
                rectTransform.parent = pointerObj.transform;
                rectTransform.localPosition = Vector3.zero;
            }
        }
        else if (pointerObj.tag == "Props")
        {
            Transform m_tempParent = pointerObj.transform.parent;
            pointerObj.transform.parent = transParent;
            this.transform.parent = m_tempParent;

            this.transform.localPosition = Vector3.zero;
            pointerObj.transform.localPosition = Vector3.zero;
        }
        else
        {
            rectTransform.parent = transParent;
            rectTransform.localPosition = Vector3.zero;
        }

        canvasGrounp.blocksRaycasts = true;

    }


}
