using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CapsuleCameraSwitch : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera capsuleCamera;
    [SerializeField] private CinemachineVirtualCamera vcam1;
    // Start is called before the first frame update
    void Start()
    {
        capsuleCamera.m_Priority = 11;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
        vcam1.m_Priority = 10;
        capsuleCamera.m_Priority = 11;
    }
    }
}
