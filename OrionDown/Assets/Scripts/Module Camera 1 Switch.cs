using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ModuleCamera1Switch : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vcam;
    [SerializeField] private CinemachineVirtualCamera capsuleCamera;

    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnMouseDown() {
        capsuleCamera.m_Priority = 10;
        vcam.m_Priority = 11;

    }
    // Update is called once per frame
    void Update()
    {

    }
}
