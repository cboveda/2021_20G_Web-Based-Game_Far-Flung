using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConverorSystem : MonoBehaviour
{

    public GameObject[] conveyor_object_backlog; // objects that are cycled through 
    private GameObject[] conveyor_object_inuse; // objects on the conveyor belt currently

    public Vector3 ConveyorStartPosition;
    public Vector3 ConveyorEndPosition;

    /*
        Takes a conveyor object away from the conveyor system, allowing it to. 
    */
    public void DetachFromConveyorSystem( ConveyorObject c_object ) {

    }
    
    /* 
        Attaches or Reattaches an object to the convory system at the position of the object.
    */
    public void AttachToConveyorSystem( ConveyorObject c_object ) {

    }

    /* 
        When a conveyor object gets to the end of travel, it calls this to be pulled off the conveyor and put back into the backlog
    */
    public void EndObjectTravel( ConveyorObject c_object ) {

    }

    /* 
        Takes the next object from the backlog, tunes it, and places it in game space.
    */
    public void StartNextObjectTravel() {

    }


}
