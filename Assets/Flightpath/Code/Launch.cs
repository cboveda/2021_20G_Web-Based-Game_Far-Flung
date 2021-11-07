using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flightpath
{
    public class Launch : MonoBehaviour
    {
        private Vector3 _launchDirection;
        private float _launchPower;
        private Rigidbody _body;
        private Attractor _attractor;
        private bool _launched;
        public float StartX;
        public float StartY;

        // Start is called before the first frame update
        public void Start()
        {
            _body = this.GetComponent<Rigidbody>();
            _attractor = this.GetComponent<Attractor>();
            InitializePosition();
        }

        public void InitializePosition()
        {
            StopLaunch();
            this.transform.position = new Vector3(StartX, StartY, 0);
        }
        public void DoLaunch()
        {
            if (!_launched)
            {
                _attractor.Affected = true;
                _body.AddForce(_launchDirection * _launchPower, ForceMode.VelocityChange);
                _launched = true;
            }
        }

        public void ResetLaunch()
        {
            InitializePosition();
        }

        public void StopLaunch()
        {
            _attractor.Affected = false;
            _launched = false;
            _body.velocity = Vector3.zero;
        }

        public void SetAngle(float angle)
        {
            float xDirection = Mathf.Cos(angle * (3.14f / 180f));
            float yDirection = Mathf.Sin(angle * (3.14f / 180f));
            _launchDirection = new Vector3(xDirection, yDirection, 0);
        }

        public void SetPower(float power)
        {
            _launchPower = power;
        }

        public float GetAngle()
        {
            return Vector3.Angle(_launchDirection, Vector3.right);
        }

        public float GetPower()
        {
            return _launchPower;
        }

        public Vector3 GetLaunchDirection()
        {
            return _launchDirection;
        }
    }
}