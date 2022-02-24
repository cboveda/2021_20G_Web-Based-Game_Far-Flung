using UnityEngine;

public class CubeMaker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        createCube();
    }

    void OnTriggerExit(Collider cube)
    {
        if (cube.GetComponent<DragObject>() && cube.GetComponent<DragObject>().isHeld)
        {
            createCube();
        }
    }

    void createCube()
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.SetPositionAndRotation(transform.position, transform.rotation);
        cube.AddComponent<DragObject>();
        Rigidbody cubeRig = cube.AddComponent<Rigidbody>();
        cubeRig.constraints = RigidbodyConstraints.FreezeAll;
    }
}
