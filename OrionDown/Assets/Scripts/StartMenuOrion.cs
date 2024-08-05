using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuOrion : MonoBehaviour
{
    private Vector3 orionRotation = new Vector3 (-50f,100f,0f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        orionRotation.z -= Time.deltaTime * 10;

        transform.rotation = Quaternion.Euler(orionRotation);

    }
}
