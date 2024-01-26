using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
        private Vector3 cameraRotation = new Vector3 (0,0,0);
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        cameraRotation.x -= Input.GetAxis("Vertical");
        cameraRotation.y += Input.GetAxis("Horizontal");
        transform.rotation = Quaternion.Euler(cameraRotation);
    }
}