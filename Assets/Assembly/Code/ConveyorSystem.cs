using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConveyorSystem : MonoBehaviour {

    public ConveyorObject[] SystemItems;

    public int ConveyorCapacity = 5;
    public float ConveyorSpeed = 0.1f;
    public float ConveyorLength = 20;

    private Queue<ConveyorObject> conveyor_object_backlog = new Queue<ConveyorObject>(); 
    private List<ConveyorObject> conveyor_objects_in_use = new List<ConveyorObject>();

    private Vector3 ConveyorEndPosition;
    private Vector3 ConveyorStartPosition;

    void Start() {

        ConveyorStartPosition = transform.position;
        ConveyorEndPosition = ConveyorStartPosition + (transform.forward * ConveyorLength);

        foreach (ConveyorObject co in SystemItems) {

            ConveyorObject co_e = Instantiate<ConveyorObject>(co);
            co_e.gameObject.SetActive(false);

            conveyor_object_backlog.Enqueue( co_e );
        }

        BoxCollider gc = gameObject.GetComponent<BoxCollider>();
        gc.center = new Vector3(0, -0.5f, (ConveyorLength / 2));
        gc.size = new Vector3(3f, 0.1f, ConveyorLength);

        float interval = (ConveyorLength / ConveyorCapacity) / ConveyorSpeed;
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

                // Debug.Log( " Started object in motion... ");

                ConveyorObject c_object = conveyor_object_backlog.Dequeue();

                c_object.LERP_Speed = ConveyorSpeed;
                c_object.HostConveyor = this;
                c_object.InitalizeConveyorObject(ConveyorStartPosition, ConveyorEndPosition);

                conveyor_objects_in_use.Add( c_object );
            }

            yield return new WaitForSeconds( interval );
        }
    }

    private void OnDrawGizmos() {

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.5f);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere((transform.position + (transform.forward * ConveyorLength)), 0.5f);
    }
}
