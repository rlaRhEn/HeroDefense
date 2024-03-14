using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Place an UI element to a world position
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class PlaceUIElementAtWorldPosition : MonoBehaviour, UnityEngine.EventSystems.IPointerClickHandler
{
    private RectTransform rectTransform;
    private Vector2 uiOffset;
    private RectTransform canvas;
    public GameObject target;
    public Vector3 offset;
    public string hyperLink = null;
    /// <summary>
    /// Initiate
    /// </summary>
    void Start()
    { 
        // Get the rect transform
        this.rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>().GetComponent<RectTransform>();
        // Calculate the screen offset
        this.uiOffset = new Vector2((float)canvas.sizeDelta.x / 2f, (float)canvas.sizeDelta.y / 2f);
    }

    /// <summary>
    /// $$anonymous$$ove the UI element to the world position
    /// </summary>
    /// <param name="objectTransformPosition"></param>
    public void MoveTo(Vector3 objectTransformPosition)
    {
        // Get the position on the canvas
        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(objectTransformPosition);
        Vector2 proportionalPosition = new Vector2(ViewportPosition.x * canvas.sizeDelta.x, ViewportPosition.y * canvas.sizeDelta.y);

        // Set the position and remove the screen offset
        this.rectTransform.localPosition = proportionalPosition - uiOffset;
    }


    public void FixedUpdate()
    {
        MoveTo(target.transform.position + offset);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
        if(hyperLink.Length > 1)
        {
            Application.OpenURL(hyperLink);
        }
    }
}
