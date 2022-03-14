using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flightpath
{
    /*
        Component moves the parent object in tandem with the target, the satellite.
        Author: Chris Boveda
    */
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField]
        private GameObject target;
        // Update is called once per frame
        private void Start() 
        {
            target = GameObject.FindWithTag("FlightpathSatellite");
        }

        void Update()
        {
            transform.position = target.transform.position - new Vector3(0,0,100);
        }
    }
}