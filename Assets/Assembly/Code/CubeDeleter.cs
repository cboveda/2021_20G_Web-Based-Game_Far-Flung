using UnityEngine;

public class CubeDeleter : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<DragObject>() == null || (collision.gameObject.GetComponent<DragObject>() != null && collision.gameObject.GetComponent<DragObject>().isHeld)) return;
        Destroy(collision.gameObject);
    }

    private void OnCollisionStay(Collision collision){
        OnCollisionEnter(collision);
    }
}
