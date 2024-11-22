using UnityEngine;
using UnityEngine.EventSystems;

public class DragableWindow : MonoBehaviour, IDragHandler
{
    [SerializeField] Canvas canvas;

    private RectTransform rectTransform;

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    void Start()
    {
        rectTransform = GetComponent<RectTransform>(); 
    }

}
