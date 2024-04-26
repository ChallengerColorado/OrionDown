using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;

public class WiresModule : ModuleBehaviour
{
    public Transform[] topSockets;

    public GameObject[] smoothWirePrefabs;
    
    public GameObject[] twistedWirePrefabs;

    private Dictionary<WireColor, Material> colorMaterials;

    //dificulty system
    /*private int[][] wireConfig = new int[][]{
        new int[] {0,3,0,3,0,6,0},
        new int[] {3,1,0,4,0,2,0},
        new int[] {4,5,0,3,0,6,0},
        new int[] {4,5,0,3,0,6,0},
        new int[] {5,5,0,3,0,6,0},
        new int[] {5,5,0,3,0,6,0}, 
        new int[] {5,5,0,3,0,6,0},
        new int[] {6,5,0,3,0,6,0},
        new int[] {6,5,0,3,0,6,0},
        new int[] {6,5,0,3,0,6,0}
    };*/


    private static WireSpec[][] wireConfig = new WireSpec[][]
    {
        new WireSpec[] { new WireSpec(WireColor.Blue, WireColor.Black, 0, true), null, null, null, new WireSpec(WireColor.Red, WireColor.Red, 2, false), null, null, null }
    };

    private WireSpec[] layout;

    private List<bool> solution = new List<bool>();
    private List<Wire> wires = new List<Wire>();

    // Start is called before the first frame update
    void Start()
    {

        topSockets = new Transform[]
        {
            transform.Find("default/Top_Socket_0"),
            transform.Find("default/Top_Socket_1"),
            transform.Find("default/Top_Socket_2"),
            transform.Find("default/Top_Socket_3"),
            transform.Find("default/Top_Socket_4"),
            transform.Find("default/Top_Socket_5"),
            transform.Find("default/Top_Socket_6"),
            transform.Find("default/Top_Socket_7")
        };

        Debug.Log(topSockets);

        foreach (var t in topSockets)
        {
            Debug.Log(t.ToString());
        }

        smoothWirePrefabs = new GameObject[]
        {
            Resources.Load<GameObject>("Wires/Smooth 1"),
            Resources.Load<GameObject>("Wires/Smooth 2"),
            Resources.Load<GameObject>("Wires/Smooth 3")
        };


        twistedWirePrefabs = new GameObject[]
        {
            Resources.Load<GameObject>("Wires/Twisted 1"),
            Resources.Load<GameObject>("Wires/Twisted 2"),
            Resources.Load<GameObject>("Wires/Twisted 3")
        };

        colorMaterials = new Dictionary<WireColor, Material>()
        {
            { WireColor.Black, Resources.Load<Material>("Wire Materials/Wire Material Black") },
            { WireColor.Blue,  Resources.Load<Material>("Wire Materials/Wire Material Blue") },
            { WireColor.Red,   Resources.Load<Material>("Wire Materials/Wire Material Red") },
            { WireColor.White, Resources.Load<Material>("Wire Materials/Wire Material White") }
        };

        if (topSockets.Length != 8)
            throw new ArgumentException("topSockets must be of length 8.");

        layout = wireConfig[new System.Random().Next(wireConfig.Length)];
        InitializeWires();
    }

    public void ToggleWire(int position)
    {
        wires[position].status = !wires[position].status;
        CheckWires();
    }

    private void CheckWires()
    {
        for (int i = 0; i < 7; i++)
        {
            if (wires[i].status != solution[i])
                return;
        }

        Status = true;

    }

    private void InitializeWires() {
        Debug.Log("Initializing wires...");

        for (int i = 0; i < layout.Length; i++) {
            if (layout[i] != null)
                wires.Add(new Wire(layout[i], topSockets[i], this));
        }
    }

    private enum WireColor
    {
        Black,
        Blue,
        Red,
        White
    }

    private class WireSpec
    {
        public WireColor color1;
        public WireColor color2;
        public int offset;
        public bool reversed;

        public WireSpec(WireColor color1, WireColor color2, int offset, bool reversed)
        {
            this.color1 = color1;
            this.color2 = color2;
            this.offset = offset;
            this.reversed = reversed;
        }
    }

    private class Wire
    {
        public WireSpec spec;
        public GameObject gameObject;
        public bool status = true;

        public Wire(WireSpec spec, Transform parent, WiresModule module)
        {
            Debug.Log("Making wire: " + parent);
            this.spec = spec;

            if (spec.color1 == spec.color2)
            {
                gameObject = Instantiate(module.smoothWirePrefabs[spec.offset], parent);

                gameObject.GetComponent<Renderer>().material = module.colorMaterials[spec.color1];
            }
            else
            {
                gameObject = Instantiate(module.twistedWirePrefabs[spec.offset], parent);

                Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
                renderers[0].material = module.colorMaterials[spec.color1];
                renderers[1].material = module.colorMaterials[spec.color2];
            }

            if (spec.reversed)
                gameObject.transform.localScale = Vector3.Scale(gameObject.transform.localScale, new Vector3(-1, 1, 1));

            Debug.Log("Position: " + gameObject.transform.position);
        }
    }

}
