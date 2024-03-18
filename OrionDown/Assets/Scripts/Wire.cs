using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : ModuleBehaviour
{
    private WireType type;

    private int _offset;
    private int Offset
    {
        get
        {
            return _offset;
        }
        set
        {
            if (Mathf.Abs(value) > 2)
            {
                throw new ArgumentException("WireOffset must be a value from -2 to 2.");
            }
        }
    }
    

    private bool _wireStatus = true;
    public bool WireStatus {
        get
        {
            return _wireStatus;
        }
        set
        {
            _wireStatus = value;

            // set shape
        }
    }

    public void Initialize(Transform socketTransform, int wireOffset, WireType wireType)
    {
        transform.SetParent(socketTransform, false);
        Offset = wireOffset;
        type = wireType;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum WireType
    {
        Smooth,
        Twisted
    }

}
