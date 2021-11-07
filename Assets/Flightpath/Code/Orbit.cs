using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flightpath
{
    public class Orbit : MonoBehaviour
    {
        public GameObject center;
        public float speed;

        public bool orbitting;

        void Start()
        {
            orbitting = false;
        }

        void Update()
        {
            if (orbitting)
            {
                transform.RotateAround(center.transform.position, Vector3.forward, speed * Time.deltaTime);
            }
        }
    }
}