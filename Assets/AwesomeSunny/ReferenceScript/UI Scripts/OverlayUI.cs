using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// UGS Helping Sticky UI
/// </summary>
public class OverlayUI : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler
{
    public Vector3 showPos;
    public Vector3 hidePos;
    public float t;

    public void OnPointerEnter(PointerEventData eventData)
    {
        show = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        show = false;
    }


    public bool show;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (show)
        {
            (this.transform as RectTransform).anchoredPosition  = Vector3.Lerp((this.transform as RectTransform).anchoredPosition , showPos,t);

        }
        else
        {
            (this.transform as RectTransform).anchoredPosition = Vector3.Lerp((this.transform as RectTransform).anchoredPosition, hidePos,t);
        }
    }
}
