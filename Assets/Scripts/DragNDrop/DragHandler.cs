using UnityEngine; 
using UnityEngine.EventSystems;

public class DragHandler: MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 lastPosition;

    private void Awake()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag (PointerEventData eventData)
    {
        lastPosition = rectTransform.anchoredPosition;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }
 
    public void OnDrag (PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
 
    public void OnEndDrag (PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        rectTransform.anchoredPosition = lastPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }
}