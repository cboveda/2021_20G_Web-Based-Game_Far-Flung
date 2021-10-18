using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject lead;
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    Vector3 camfollow = new Vector3(0, 4, -10);

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 target = lead.transform.position + camfollow;
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
    }
}
