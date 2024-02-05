using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ModuleCamera1Switch : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vcam1;
    [SerializeField] private CinemachineVirtualCamera capsuleCamera;

    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnMouseDown() {
        capsuleCamera.m_Priority = 10;
        vcam1.m_Priority = 11;
        Debug.Log("I am being run!");
    }
    // Update is called once per frame
    void Update()
    {

    }
}
