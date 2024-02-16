using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{

    private void Awake()
    {
        // ensure only one GameObject
        if (GameManager.instance.StateManager != null)
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

}
