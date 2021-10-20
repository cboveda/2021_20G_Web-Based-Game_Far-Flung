using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteCollision : MonoBehaviour
{
    public GameObject EventSystem;
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("FlightpathAsteroid"))
        {
            EventSystem.GetComponent<LaunchManager>().OnAsteroidCollisionDetected();
        }
    }

    private void OnBecameInvisible() {
        EventSystem.GetComponent<LaunchManager>().OnSatelliteLeaveWindow();
    }
}
