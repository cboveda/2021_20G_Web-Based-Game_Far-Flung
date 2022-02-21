using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConverorSystem : MonoBehaviour {

    public Queue<ConveyorObject> conveyor_object_backlog = new Queue<ConveyorObject>(); // objects that are cycled through 
    private List<ConveyorObject> conveyor_objects_in_use = new List<ConveyorObject>(); // objects on the conveyor belt currently

    public Vector3 ConveyorStartPosition;
    public Vector3 ConveyorEndPosition;

    public int ConveyorCapacity = 5;
    public float ConveyorSpeed = 0.1f;

    void Start() {

        float distance = Vector3.Distance(ConveyorStartPosition, ConveyorEndPosition);
        float interval = (distance / ConveyorCapacity) / ConveyorSpeed;

        StartCoroutine(StartObjectTravel(interval)); // start creating objects
    }

    /*
        Takes a conveyor object away from the conveyor system, allowing it to be picked up by the user. 
    */
    public void DetachFromConveyorSystem( ConveyorObject c_object ) {

        if ( conveyor_objects_in_use.Contains( c_object ) ) {

            conveyor_objects_in_use.Remove( c_object );
        }
    }
    
    /* 
        Attaches or Reattaches an object to the convory system at the position of the object.
    */
    public void AttachToConveyorSystem( ConveyorObject c_object ) {

        if ( !conveyor_objects_in_use.Contains( c_object ) ) {

            conveyor_objects_in_use.Add( c_object );
        }
    }

    /* 
        When a conveyor object gets to the end of travel, it calls this to be pulled off the conveyor and put back into the backlog
    */
    public void EndObjectTravel( ConveyorObject c_object ) {

        if ( conveyor_objects_in_use.Contains( c_object ) ) {
            conveyor_objects_in_use.Remove( c_object ); 
            conveyor_object_backlog.Enqueue( c_object );
        }
    }

    /* 
        Takes the next object from the backlog, tunes it, and places it in game space.
    */
    private IEnumerator StartObjectTravel( float interval ) {

        while ( true ) {

            if ( conveyor_object_backlog.Count > 0 ) {

                ConveyorObject c_object = conveyor_object_backlog.Dequeue();

                c_object.Beginning = ConveyorStartPosition;
                c_object.Destination = ConveyorEndPosition;
                c_object.LERP_Speed = ConveyorSpeed;
                c_object.HostConveyor = this;
                c_object.InitalizeConveyorObject();

                conveyor_objects_in_use.Add( c_object );
            }

            yield return new WaitForSeconds( interval );
        }
    }
}
