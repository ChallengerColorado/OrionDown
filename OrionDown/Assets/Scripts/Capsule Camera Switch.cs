using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CapsuleCameraSwitch : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera capsuleCamera;
    private GameObject vcam1Object;
    private CinemachineVirtualCamera vcam1;
    private GameObject vcam2Object;
    private CinemachineVirtualCamera vcam2;
    private GameObject vcam3Object;
    private CinemachineVirtualCamera vcam3;
    private GameObject vcam4Object;
    private CinemachineVirtualCamera vcam4;

    // Start is called before the first frame update
    void Start()
    {
        vcam1Object = GameObject.Find("Propulsion(Clone)/Propulsion Camera");
        vcam1 = vcam1Object.GetComponent<CinemachineVirtualCamera>();
        vcam2Object = GameObject.Find("Radiation Protection(Clone)/Radiation Protection Camera");
        vcam2 = vcam2Object.GetComponent<CinemachineVirtualCamera>();
        vcam3Object = GameObject.Find("Life Support(Clone)/Life Support Camera");
        vcam3 = vcam3Object.GetComponent<CinemachineVirtualCamera>();
        vcam4Object = GameObject.Find("Heat Shield(Clone)/Heat Shield Camera");
        vcam4 = vcam4Object.GetComponent<CinemachineVirtualCamera>();

        capsuleCamera.m_Priority = 11;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
        vcam1.m_Priority = 10;
        vcam2.m_Priority = 10;
        vcam3.m_Priority = 10;
        vcam4.m_Priority = 10;
        capsuleCamera.m_Priority = 11;
    }
    }
}
