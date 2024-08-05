using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Class to keep track of the state and properties in of an individual wire in the propulsion module
 */
public class Wire : ModuleBehaviour
{
    private WireType type;


    // stores how many spaces farther to the right the lower socket is than the upper socket
    private int _offset;
    private int Offset
    {
        get
        {
            return _offset;
        }
        set
        {
            // check to make sure that the value matches one of the provided wire models
            if (Mathf.Abs(value) > 2)
            {
                throw new ArgumentException("WireOffset must be a value from -2 to 2.");
            }
        }
    }
    
    // stores whether or not the wire is activated or deactivated
    private bool _wireStatus = true;
    public bool WireStatus {
        get
        {
            return _wireStatus;
        }
        set
        {
            _wireStatus = value;
        }
    }

    public void Initialize(Transform socketTransform, int wireOffset, WireType wireType)
    {
        transform.SetParent(socketTransform, false);
        Offset = wireOffset;
        type = wireType;
    }

    public enum WireType
    {
        Smooth,
        Twisted
    }

}
