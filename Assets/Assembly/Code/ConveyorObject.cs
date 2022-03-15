using UnityEngine;

public class ConveyorObject : MonoBehaviour
{
    private Vector3 Beginning;
    private Vector3 Destination;
    public float LERP_Speed;
    public ConveyorSystem HostConveyor;

    private bool AttachmentState;
    private float TimeZero;
    private float Distance;

    public void InitalizeConveyorObject( Vector3 start, Vector3 end ) {

        Beginning = start;
        Destination = end;

        AttachmentState = true;
        gameObject.SetActive(true);
        TimeZero = Time.time;
        Distance = Vector3.Distance( Beginning, Destination );
        transform.position = Beginning;
    }

    // Update is called once per frame
    void Update() {

        if ( AttachmentState ) { // if attached to the host conveyor

            float distance_covered = (Time.time - TimeZero) * LERP_Speed;
            float journey_fraction = distance_covered / Distance;
            transform.position = Vector3.Lerp(Beginning, Destination, journey_fraction); // move with respect to time

            if ( Vector3.Distance( Beginning, transform.position ) >= (Distance - 0.1) ) { // if we have reached the end of the conveyor
                gameObject.SetActive(false);
                HostConveyor.EndObjectTravel( this );
                AttachmentState = false; // no longer attached to conveyor
            }
        }
    }

    public void OnPickUp() { // when the item is picked up

        if ( AttachmentState ) {

            Debug.Log( "ON PICKUP CALLED" );

            HostConveyor.DetachFromConveyorSystem( this );
            AttachmentState = false;
        }
    }

    public void OnDrop() { // when the item is released by the user

        // if in hitbox of host conveyor && !AttachmentState {
            HostConveyor.AttachToConveyorSystem( this );
            AttachmentState = true;            
    }

}
