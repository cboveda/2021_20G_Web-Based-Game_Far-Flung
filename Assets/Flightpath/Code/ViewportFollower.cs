using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flightpath
{
    public class ViewportFollower : MonoBehaviour
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
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y - 56, 0);
        }
    }
}
