using System.Collections;
using System.Collections.Generic;
using UnityEngine;using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public Vector2 _slotOffset;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition =
                GetComponent<RectTransform>().anchoredPosition + _slotOffset;
            // GetComponent<RectTransform>().SetAsFirstSibling();
        }
    }
}
