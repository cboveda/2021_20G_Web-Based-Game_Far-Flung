using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{
    public float xMagnitude;
    public float yMagnitude;
    
    Rigidbody body;
    bool launched;
    
    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody>();
        launched = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!launched) {
            body.AddForce(new Vector3 (xMagnitude, yMagnitude, 0f), ForceMode.VelocityChange);
            launched = true;
        }
    }

    public bool hasLaunched() {
        return launched;
    }
}
