using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraPan : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera capsuleCamera;
    private Vector3 cameraRotation = new Vector3 (0f,0f,0f);
    [SerializeField] private float xSensitivity = 1f;
    [SerializeField] private float ySensitivity = 1f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (capsuleCamera.m_Priority == 11) {
            cameraRotation.x -= xSensitivity * Input.GetAxis("Vertical") * Time.deltaTime * 50;
            cameraRotation.y += ySensitivity * Input.GetAxis("Horizontal") * Time.deltaTime * 50;
            if (cameraRotation.x < -90f) {
                cameraRotation.x = -90f;
        }
            if (cameraRotation.x > 90f) {
                cameraRotation.x = 90f;
        }
        transform.rotation = Quaternion.Euler(cameraRotation);
        }
    }
}
