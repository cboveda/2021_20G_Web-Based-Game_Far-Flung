using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public static float MinDistance = 1f;
    Rigidbody Body;
    public bool Affected;

    // Start is called before the first frame update
    public void Start()
    {
        Body = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Attractor[] attractors = FindObjectsOfType<Attractor>();
        foreach (Attractor a in attractors)
        {
            if (a != this && a.Affected)
            {
                Attract(a);
            }
        }
    }

    private void Attract(Attractor a)
    {
        Rigidbody bodyToAttract = a.Body;
        Vector3 direction = Body.position - bodyToAttract.position;
        float distance = direction.magnitude;
        distance = (distance < MinDistance) ? MinDistance : distance;
        float magnitude = (Body.mass * bodyToAttract.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * magnitude;
        bodyToAttract.AddForce(force);
    }
}
