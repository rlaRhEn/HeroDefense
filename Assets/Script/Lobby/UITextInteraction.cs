using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UITextInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [System.Serializable]
    private class OnClickEvent : UnityEvent { }
    //Text UI를 클릭했을 때 호출하고 싶은 메소드 등록
    [SerializeField] OnClickEvent onClickEvent;

    //색상이 바뀌고 터치가 되는 text
    Text text;
    //
    Image image;
    private void Awake()
    {
        text = GetComponentInChildren<Text>();
        image = GetComponent<Image>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        text.fontStyle = FontStyle.Bold;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0.7f);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        text.fontStyle = FontStyle.Normal;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0.4f);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        onClickEvent?.Invoke();
        Debug.Log("click");
    }
}
