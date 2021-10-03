using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragNDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas _canvas;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        _canvasGroup.alpha = .6f;
        _canvasGroup.blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
    }
}
