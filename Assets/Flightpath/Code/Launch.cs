using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{
    private Vector3 launchDirection;
    private float launchPower;
    Rigidbody body;
    bool launched;
    
    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody>();
        this.GetComponent<Attractor>().affected = false;
        launched = false;
    }

    public void doLaunch()
    {
        if (!launched) {
            body.AddForce(launchDirection * launchPower, ForceMode.VelocityChange);
            launched = true;
            Debug.Log("Launched");
            this.GetComponent<Attractor>().affected = true;
        }
    }

    public bool hasLaunched() {
        return launched;
    }

    public void setAngle (float angle) {
            float xDirection = Mathf.Cos(angle * (3.14f / 180f));
            float yDirection = Mathf.Sin(angle * (3.14f / 180f));
            launchDirection = new Vector3(xDirection, yDirection, 0);
    }

    public void setPower (float power) { 
        launchPower = power;
    }
}
