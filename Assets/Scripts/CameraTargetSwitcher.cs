using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTargetSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    
    void Start()
    {
        if (virtualCamera == null)
        {
            virtualCamera = FindFirstObjectByType<CinemachineVirtualCamera>();
        }
    }

    public void SwitchTarget(Transform newTarget)
    {
        if (virtualCamera != null)
        {
            virtualCamera.Follow = newTarget;
        }
    }
}
