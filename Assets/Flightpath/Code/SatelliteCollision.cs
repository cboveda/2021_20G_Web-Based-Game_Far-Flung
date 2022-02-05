using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flightpath
{
    public class SatelliteCollision : MonoBehaviour
    {
        public GameObject EventSystem;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("FlightpathAsteroid"))
            {
                EventSystem.GetComponent<LaunchManager>().OnAsteroidCollisionDetected();
            }
            if (other.CompareTag("FlightpathMars")) 
            {
                EventSystem.GetComponent<LaunchManager>().OnMarsCollisionDetected();
            }
            if (other.CompareTag("FlightpathBounds") && EventSystem)
            {
                if (other.name == "TopBoundary")
                {
                    EventSystem.GetComponent<LaunchManager>().OnSatelliteLeaveWindow(LaunchManager.TopBoundaryScriptIndex);
                }
                else if (other.name == "BottomBoundary")
                {
                    EventSystem.GetComponent<LaunchManager>().OnSatelliteLeaveWindow(LaunchManager.BottomBoundaryScriptIndex);
                }
                else if (other.name == "RightBoundary")
                {
                    EventSystem.GetComponent<LaunchManager>().OnSatelliteLeaveWindow(LaunchManager.RightBoundaryScriptIndex);
                }
                else if (other.name == "LeftBoundary")
                {
                    EventSystem.GetComponent<LaunchManager>().OnSatelliteLeaveWindow(LaunchManager.LeftBoundaryScriptIndex);
                }
                
            }
        }
    }
}