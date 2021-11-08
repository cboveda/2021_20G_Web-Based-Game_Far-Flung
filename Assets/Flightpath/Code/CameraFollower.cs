using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flightpath
{
    public class CameraFollower : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            transform.position = transform.parent.transform.gameObject.transform.position;
        }
    }
}