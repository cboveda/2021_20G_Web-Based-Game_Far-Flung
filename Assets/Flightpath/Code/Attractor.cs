using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public static float minDistance = 1f;
    Rigidbody body;
    public bool affected;
    
    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Attractor[] attractors = FindObjectsOfType<Attractor>();
        foreach (Attractor a in attractors) {
            if (a != this && a.affected) {
                Attract(a);
            }
        }
    }

    private void Attract(Attractor a) {
        Rigidbody bodyToAttract = a.body;
        Vector3 direction = body.position - bodyToAttract.position;
        float distance = direction.magnitude;

        if (distance > Attractor.minDistance) {
            float magnitude = (body.mass * bodyToAttract.mass) / Mathf.Pow(distance, 2);
            Vector3 force = direction.normalized * magnitude;
            bodyToAttract.AddForce(force);
        }
    }
}
