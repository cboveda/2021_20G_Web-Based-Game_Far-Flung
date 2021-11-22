using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraPositionController : MonoBehaviour
{
    public CinemachineVirtualCamera currentVirtualCamera;
    public CinemachineTrackedDolly currentCameraDolly;

    private float cameraSpeed = 0.001f;
    // Start is called before the first frame update
    void Start()
    {
        currentCameraDolly = currentVirtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
    }

    // Update is called once per frame
    void Update()
    {
        currentCameraDolly.m_PathPosition += cameraSpeed;
        
    }

    public void AdjustCurrentCamera(CinemachineVirtualCamera camera)
    {
        currentVirtualCamera = camera;
        currentCameraDolly = currentVirtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        currentCameraDolly.m_PathPosition = 0.0f;
    }

}
