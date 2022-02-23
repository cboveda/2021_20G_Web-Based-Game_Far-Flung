using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentGhost : MonoBehaviour
{
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.down * (rotationSpeed * Time.deltaTime));
    }
}
