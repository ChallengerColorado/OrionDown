using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PropulsionModule : ModuleBehaviour
{
    private Transform[] topSockets;

    private GameObject[] smoothWirePrefabs;
    private GameObject[] twistedWirePrefabs;

    private Dictionary<WireColor, Material> colorMaterials;

    private List<int> buttonToWire = new List<int>();

    
    //dificulty system
    private static Dictionary<GameManager.Difficulty, (WireSpec[], bool[])[]> wireConfig = new Dictionary<GameManager.Difficulty, (WireSpec[], bool[])[]>()
    {
        {  GameManager.Difficulty.Easy, new (WireSpec[], bool[])[]
            {
                (new WireSpec[]
                    {
                        new WireSpec(WireColor.Blue, WireColor.Black, 0, true),
                        null,
                        null,
                        null,
                        new WireSpec(WireColor.Red, WireColor.Red, 2, false),
                        null,
                        null,
                        null
                    },
                 new bool[] { false, false }),
                (new WireSpec[] 
                    {
                        null,
                        null,
                        null,
                        null,
                        new WireSpec(WireColor.Blue, WireColor.Blue, 1, false),
                        null,
                        null,
                        new WireSpec(WireColor.White, WireColor.Black, 0, true)
                    },
                 new bool[] { false, false })
            }
        },
        {  GameManager.Difficulty.Medium, new (WireSpec[], bool[])[]
            {
                (new WireSpec[]
                    {
                        null,
                        new WireSpec(WireColor.Black, WireColor.Black, 1, true),
                        null,
                        null,
                        null,
                        null,
                        null,
                        new WireSpec(WireColor.Red, WireColor.Black, 2, false)
                    },
                 new bool[] { false, false }),
                (new WireSpec[]
                    {
                        null,
                        null,
                        new WireSpec(WireColor.White, WireColor.Blue, 2, false),
                        new WireSpec(WireColor.Red, WireColor.Blue, 1, true),
                        null,
                        null,
                        null,
                        null
                    },
                 new bool[] { false, false })
            }
        },
        {  GameManager.Difficulty.Difficult, new (WireSpec[], bool[])[]
            {
                (new WireSpec[]
                    {
                        null,
                        new WireSpec(WireColor.White, WireColor.White, 0, true),
                        null,
                        new WireSpec(WireColor.Black, WireColor.Red, 2, true),
                        null,
                        null,
                        null,
                        null
                    },
                 new bool[] { false, false }),
                (new WireSpec[]
                    {
                        new WireSpec(WireColor.Blue, WireColor.Black, 0, true),
                        null,
                        null,
                        new WireSpec(WireColor.White, WireColor.Red, 2, true),
                        null,
                        null,
                        null,
                        null
                    },
                 new bool[] { false, false })
            }
        }
    };

    private bool[] solution;
    private WireSpec[] layout;

    private List<Wire> wires = new List<Wire>();

    // Start is called before the first frame update
    void Start()
    {

        topSockets = new Transform[]
        {
            transform.Find("Module Base/Top_Socket_0"),
            transform.Find("Module Base/Top_Socket_1"),
            transform.Find("Module Base/Top_Socket_2"),
            transform.Find("Module Base/Top_Socket_3"),
            transform.Find("Module Base/Top_Socket_4"),
            transform.Find("Module Base/Top_Socket_5"),
            transform.Find("Module Base/Top_Socket_6"),
            transform.Find("Module Base/Top_Socket_7")
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

        (WireSpec[], bool[])[] possiblePresets = wireConfig[GameManager.Instance.currentDifficulty];
        (WireSpec[], bool[]) chosenPreset = possiblePresets[new System.Random().Next(possiblePresets.Length)];
        layout = chosenPreset.Item1;
        solution = chosenPreset.Item2;

        SetStatus(false, "@#");
        InitializeWires();
    }

    private Image image;
    public void ToggleWire(int position)
    {
        if(buttonToWire.Contains(position)){
        wires[buttonToWire.IndexOf(position)].state = !wires[buttonToWire.IndexOf(position)].state;
        Debug.Log("Wire state" + buttonToWire.IndexOf(position) + wires[buttonToWire.IndexOf(position)].state);;
        CheckWires();}
    }

    private void CheckWires()
    {
        for (int i = 0; i < 2; i++)
        {
            if (wires[i].state != solution[i])
                return;
        }

        SetStatus(true, "BB");

    }

    private void InitializeWires() {
        Debug.Log("Initializing wires...");

        for (int i = 0; i < layout.Length; i++) {
            if (layout[i] != null){
                wires.Add(new Wire(layout[i], topSockets[i], this));
                buttonToWire.Add(i);
            }
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
        public bool state = true;

        public Wire(WireSpec spec, Transform parent, PropulsionModule module)
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
