using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CapsuleCameraSwitch : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera capsuleCamera;
    private GameObject vcam1Object;
    private GameObject vcam2Object;
    private GameObject vcam3Object;
    private GameObject vcam4Object;
    private GameObject vcam5Object;

    private static List<CinemachineVirtualCamera> cams = new List<CinemachineVirtualCamera>();

    // Start is called before the first frame update
    void Start()
    {
        vcam1Object = GameObject.Find("Propulsion(Clone)/Propulsion Camera");
        if (vcam1Object != null)
        cams.Add(vcam1Object.GetComponent<CinemachineVirtualCamera>());
        vcam2Object = GameObject.Find("Radiation Protection(Clone)/Radiation Protection Camera");
        if (vcam2Object != null)
        cams.Add(vcam2Object.GetComponent<CinemachineVirtualCamera>());
        vcam3Object = GameObject.Find("Life Support(Clone)/Life Support Camera");
        if (vcam3Object != null)
        cams.Add(vcam3Object.GetComponent<CinemachineVirtualCamera>());
        vcam4Object = GameObject.Find("Heat Shield(Clone)/Heat Shield Camera");
        if (vcam4Object != null)
        cams.Add(vcam4Object.GetComponent<CinemachineVirtualCamera>());
        vcam5Object = GameObject.Find("Keypad(Clone)/Keypad Camera");
        if (vcam5Object != null)
        cams.Add(vcam5Object.GetComponent<CinemachineVirtualCamera>());

        capsuleCamera.m_Priority = 11;

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            foreach (var cam in cams) {
                cam.m_Priority = 10;
            }
            capsuleCamera.m_Priority = 11;
        }
    }
}
