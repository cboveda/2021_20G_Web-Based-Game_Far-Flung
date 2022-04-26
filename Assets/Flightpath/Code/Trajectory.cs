using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flightpath
{
    /*
        Encapsulates information and behavior related to the Satellite trajectory before launch.
        Author: Chris Boveda
    */
    public class Trajectory : MonoBehaviour
    {

        private float _power;
        public Launch Satellite;

        private float _powerMax;
        private float _powerMin;
        private float _baseScaleY;



        void Start()
        {
            transform.position = new Vector3(Satellite.StartX, Satellite.StartY, -200);
            transform.up = Satellite.GetLaunchDirection();
            _baseScaleY = transform.localScale.y;
        }
        void Update()
        {
            transform.up = Satellite.GetLaunchDirection();
            _power = Satellite.GetPower();
            transform.localScale = new Vector3(transform.localScale.x,
                _baseScaleY * (1 + ((_power - _powerMin) / (_powerMax - _powerMin))),
                transform.localScale.z);
        }

        public void SetPowerRange(float min, float max)
        {
            _powerMin = min;
            _powerMax = max;
        }
    }
}