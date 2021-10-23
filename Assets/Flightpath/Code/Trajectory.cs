using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{

    private float _power;
    public Launch Satellite;


    void Start()
    {
        transform.position = new Vector3(Satellite.StartX, Satellite.StartY, 0);
        transform.up = Satellite.GetLaunchDirection();
    }
    void Update()
    {
        transform.up = Satellite.GetLaunchDirection();
    }
}
