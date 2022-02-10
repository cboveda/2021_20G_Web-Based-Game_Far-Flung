using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMaker : MonoBehaviour
{

    public float cubeFrequency = 1;
    private float nextCubeTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        createCube();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextCubeTime)
        {
            nextCubeTime += cubeFrequency;
            createCube();
        }
    }

    void createCube()
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.SetPositionAndRotation(transform.position, transform.rotation);
        cube.AddComponent<DragObject>();
        Rigidbody cubeRig = cube.AddComponent<Rigidbody>();
        switch (Random.Range(0, 10))
        {
            case int n when n < 1:
                cube.GetComponent<MeshRenderer>().material.color = Color.red;
                break;
            case int n when n < 2:
                cube.GetComponent<MeshRenderer>().material.color = Color.blue;
                break;
            case int n when n < 3:
                cube.GetComponent<MeshRenderer>().material.color = Color.green;
                break;
            default:
                break;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }
}
