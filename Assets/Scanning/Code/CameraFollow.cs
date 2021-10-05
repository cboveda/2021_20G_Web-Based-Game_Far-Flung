using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject lead;
    public float smoothTime = 0.3f;
    public float turnspeed = 0.6f;
    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 target = lead.transform.position - (lead.transform.forward * 10f) + (lead.transform.up * 4f);
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, lead.transform.rotation, turnspeed);
    }
}
