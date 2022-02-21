using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorObject : MonoBehaviour
{
    public Vector3 Beginning;
    public Vector3 Destination;
    public float LERP_Speed;
    public ConverorSystem HostConveyor;

    private bool AttachmentState;

    void Start() {

        AttachmentState = true;

    }

    // Update is called once per frame
    void Update() {

        if ( AttachmentState ) { // if attached to the host conveyor
        
            // move towards destinations

            // if destination reched ->  HostConveyor.EndObjectTravel( this );
        
        }

    }


}
