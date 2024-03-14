using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Drag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    Image image;
    public static Vector2 DefaultPos;
    public Image partyImg;
    Vector2 partyPos;

    Color originalColor;

    void Awake()
    {
        image = GetComponent<Image>();// 이미지 컴포넌트가져옴
        partyPos = new Vector2(partyImg.transform.position.x, partyImg.transform.position.y);
    }
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        DefaultPos = this.transform.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector2 currentPos = eventData.position;
        this.transform.position = currentPos;
        // 회색으로 변경하고자 하는 alpha 값 (0: 완전 투명, 1: 완전 불투명)

        // 새로운 색상을 만들어서 alpha 값을 조절합니다.
        //Color greyedOutColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);

        // 새로운 색상을 이미지에 적용합니다.
        image.color = Color.gray;

    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Color originColor = new Color(1,1,1, 1);
        image.color = originColor;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Vector2.Distance(this.transform.position, partyPos) < 10)
        {
            this.transform.position = partyPos;
        }

        else { this.transform.position = DefaultPos; }

    }
}
//출처: https://krapoi.tistory.com/entry/Unity-게임-개발-드래그-앤-드롭 [코딩 작업장:티스토리]
