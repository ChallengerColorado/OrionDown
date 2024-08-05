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
    // transforms of top sockets
    private Transform[] topSockets;

    // wire prefabs
    private GameObject[] smoothWirePrefabs;
    private GameObject[] twistedWirePrefabs;
    
    // maps color to actual material used to display with that color
    private Dictionary<WireColor, Material> colorMaterials;

    // stores the indices of the buttons associated with present wires, in order
    private List<int> buttonToWire = new List<int>();


    // All possible configurations of wires. Each difficulty has a list of configurations to choose from. Each configuration is stored as an array of
    // WireSpecs, with each position in the array representing one top socket position beginning on the left and ending on the right, and a sequence of
    // booleans describing the desired status of each wire, in the order in which they appear in the WireSpec array
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
                 new bool[] { false, true }),
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
                 new bool[] { false, true })
            }
        },
        {  GameManager.Difficulty.Medium, new (WireSpec[], bool[])[]
            {
                (new WireSpec[]
                    {
                        new WireSpec(WireColor.Black, WireColor.Black, 1, true),
                        null,
                        null,
                        null,
                        new WireSpec(WireColor.Blue, WireColor.White, 0, false),
                        null,
                        null,
                        new WireSpec(WireColor.Red, WireColor.Black, 2, false)
                    },
                 new bool[] { true, false, true }),
                (new WireSpec[]
                    {
                        null,
                        null,
                        new WireSpec(WireColor.White, WireColor.Blue, 2, false),
                        new WireSpec(WireColor.Red, WireColor.Blue, 1, true),
                        null,
                        null,
                        new WireSpec(WireColor.Black, WireColor.Black, 0, true),
                        null
                    },
                 new bool[] { true, true, true })
            }
        },
        {  GameManager.Difficulty.Difficult, new (WireSpec[], bool[])[]
            {
                (new WireSpec[]
                    {
                        null,
                        new WireSpec(WireColor.White, WireColor.White, 0, false),
                        null,
                        new WireSpec(WireColor.Black, WireColor.Red, 2, true),
                        null,
                        new WireSpec(WireColor.Blue, WireColor.Blue, 1, false),
                        new WireSpec(WireColor.White, WireColor.Blue, 0, true),
                        null
                    },
                 new bool[] { true, true, false, false }),
                (new WireSpec[]
                    {
                        new WireSpec(WireColor.Blue, WireColor.Black, 0, true),
                        new WireSpec(WireColor.White, WireColor.White, 1, true),
                        null,
                        new WireSpec(WireColor.White, WireColor.Red, 2, true),
                        null,
                        null,
                        null,
                        new WireSpec(WireColor.Black, WireColor.Black, 0, false)
                    },
                 new bool[] { false, true, true, true })
            }
        }
    };

    private bool[] solution; // list of target wire states
    private WireSpec[] layout; // chosen configuration

    // all wires in chosen config
    private List<Wire> wires = new List<Wire>();

    // Start is called before the first frame update
    void Start()
    {
        // get top socket transforms
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

        // get smooth wire prefabs
        smoothWirePrefabs = new GameObject[]
        {
            Resources.Load<GameObject>("Wires/Smooth 1"),
            Resources.Load<GameObject>("Wires/Smooth 2"),
            Resources.Load<GameObject>("Wires/Smooth 3")
        };

        // get twisted wire prefabs
        twistedWirePrefabs = new GameObject[]
        {
            Resources.Load<GameObject>("Wires/Twisted 1"),
            Resources.Load<GameObject>("Wires/Twisted 2"),
            Resources.Load<GameObject>("Wires/Twisted 3")
        };

        // get materials and associate them with the corresponding colors
        colorMaterials = new Dictionary<WireColor, Material>()
        {
            { WireColor.Black, Resources.Load<Material>("Wire Materials/Wire Material Black") },
            { WireColor.Blue,  Resources.Load<Material>("Wire Materials/Wire Material Blue") },
            { WireColor.Red,   Resources.Load<Material>("Wire Materials/Wire Material Red") },
            { WireColor.White, Resources.Load<Material>("Wire Materials/Wire Material White") }
        };

        // ensure that the correct number of top sockets are presents
        if (topSockets.Length != 8)
            throw new ArgumentException("topSockets must be of length 8.");


        // determine the set of configurations to choose from based on game difficulty
        (WireSpec[], bool[])[] possiblePresets = wireConfig[GameManager.Instance.currentDifficulty];

        // select a configuration at random and extract its data
        (WireSpec[], bool[]) chosenPreset = possiblePresets[new System.Random().Next(possiblePresets.Length)];
        layout = chosenPreset.Item1;
        solution = chosenPreset.Item2;

        SetStatus(false, "@#");
        InitializeWires();
    }

    // toggle state of wire when button is pressed
    public void ToggleWire(int position)
    {
        if(buttonToWire.Contains(position)){
            wires[buttonToWire.IndexOf(position)].state = !wires[buttonToWire.IndexOf(position)].state;
        }
    }

    // check to see if the solution state has been reached
    public void CheckWires()
    {
        // disallow wire checking if module is already solved
        if (GetStatus()){
            return;
        }

        // check the state of each wire against the target states
        for (int i = 0; i < solution.Length; i++)
        {
            if (wires[i].state != solution[i])
            {
                // reduce allotted time by 30 seconds if incorrect state is present
                GameManager.Instance.GameTimer.RemainingSeconds-= 30;
                return;
            }
                
        }

        // if all wires are in the correct state, the module is solved
        SetStatus(true, "BB");
    }

    private void InitializeWires() {
        for (int i = 0; i < layout.Length; i++) {
            if (layout[i] != null){
                wires.Add(new Wire(layout[i], topSockets[i], this)); // create new Wire and store in wires
                buttonToWire.Add(i); // store the index of the button corresponding to the new wire
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

    // class representing any possible configuration of an individual wire
    private class WireSpec
    {
        // colors present on wire
        public WireColor color1;
        public WireColor color2;

        // how many spaces separate the upper and lower sockets
        public int offset;

        // whether or not the bottom socket is to the left of the top socket
        public bool reversed;

        public WireSpec(WireColor color1, WireColor color2, int offset, bool reversed)
        {
            this.color1 = color1;
            this.color2 = color2;
            this.offset = offset;
            this.reversed = reversed;
        }
    }

    // class to keep track of the state and properties in of an individual wire in the propulsion module
    private class Wire
    {
        public WireSpec spec;
        public GameObject gameObject;
        public bool state = true; // whether or not the wire is activated

        public Wire(WireSpec spec, Transform parent, PropulsionModule module)
        {
            this.spec = spec;

            // if the wire only has one color, use a smooth prefab
            if (spec.color1 == spec.color2)
            {
                gameObject = Instantiate(module.smoothWirePrefabs[spec.offset], parent);

                gameObject.GetComponent<Renderer>().material = module.colorMaterials[spec.color1];
            }
            // if the wire has two colors, use each color for one strand of a twisted wire
            else
            {
                gameObject = Instantiate(module.twistedWirePrefabs[spec.offset], parent);

                Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
                renderers[0].material = module.colorMaterials[spec.color1];
                renderers[1].material = module.colorMaterials[spec.color2];
            }

            // reflect wire object if reversed is true
            if (spec.reversed)
                gameObject.transform.localScale = Vector3.Scale(gameObject.transform.localScale, new Vector3(-1, 1, 1));
        }
    }
}
