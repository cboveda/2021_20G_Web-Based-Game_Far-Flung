using UnityEngine;

public class DragObject : MonoBehaviour
{
    [HideInInspector]
    public bool isHeld = false;
    [HideInInspector]
    public DropSlot currentSlot;

    public string itemType;
}
