using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDeleter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<DragObject>() == null || (collision.gameObject.GetComponent<DragObject>() != null && collision.gameObject.GetComponent<DragObject>().isHeld)) return;
        Destroy(collision.gameObject);
    }

    private void OnCollisionStay(Collision collision){
        OnCollisionEnter(collision);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
