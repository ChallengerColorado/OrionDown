using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
        private Vector3 cameraRotation = new Vector3 (0,0,0);
        [SerializeField] private float xSensitivity = 0.5f;
        [SerializeField] private float ySensitivity = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        cameraRotation.x -= xSensitivity * Input.GetAxis("Vertical");
        cameraRotation.y += ySensitivity * Input.GetAxis("Horizontal");
        if (cameraRotation.x < -90) {
            cameraRotation.x = -90;
        }
        if (cameraRotation.x > 90) {
            cameraRotation.x = 90;
        }
        transform.rotation = Quaternion.Euler(cameraRotation);
    }
}