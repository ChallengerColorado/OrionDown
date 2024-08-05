using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ModuleCamera1Switch : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vcam;
    private static GameObject capsuleCameraObject;

    private CinemachineVirtualCamera capsuleCamera;

    // Start is called before the first frame update
    void Start()
    {
        capsuleCameraObject = GameObject.Find("Capsule Camera");
        capsuleCamera = capsuleCameraObject.GetComponent<CinemachineVirtualCamera>();
    }
    private void OnMouseDown() {
        //Switch to module camera on press of module
        capsuleCamera.m_Priority = 10;
        vcam.m_Priority = 11;

    }
    // Update is called once per frame
    void Update()
    {

    }
}
