using UnityEngine;

public class DragObject : MonoBehaviour
{
    [HideInInspector]
    public bool isHeld = false; // redundant for convyor object HeldState
    [HideInInspector]
    public DropSlot currentSlot; // remove this as not needed

    public string itemType; // move to ConveyorObject
}


/* COMMON MERGE - DELETE FILE */