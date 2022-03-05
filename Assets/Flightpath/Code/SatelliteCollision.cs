using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flightpath
{
    /*
        Satellite collision handler.
        Author: Chris Boveda
    */
    public class SatelliteCollision : MonoBehaviour
    {
        public LaunchManager launchManager;
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Collision detected.");
            if (other.CompareTag("FlightpathAsteroid"))
            {
                launchManager.OnAsteroidCollisionDetected();
            }
            if (other.CompareTag("FlightpathMars")) 
            {
                launchManager.OnMarsCollisionDetected();
            }
            if (other.CompareTag("FlightpathBounds"))
            {
                Debug.Log("I'm outside bounds!");
                if (other.name == "TopBoundary")
                {
                    launchManager.OnSatelliteLeaveWindow(LaunchManager.TopBoundaryScriptIndex);
                }
                else if (other.name == "BottomBoundary")
                {
                    launchManager.OnSatelliteLeaveWindow(LaunchManager.BottomBoundaryScriptIndex);
                }
                else if (other.name == "RightBoundary")
                {
                    launchManager.OnSatelliteLeaveWindow(LaunchManager.RightBoundaryScriptIndex);
                }
                else if (other.name == "LeftBoundary")
                {
                    launchManager.OnSatelliteLeaveWindow(LaunchManager.LeftBoundaryScriptIndex);
                }
                
            }
        }
    }
}