using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;

public class WiresModule : ModuleBehaviour
{
    private Transform[] topSockets;

    private GameObject[] smoothWirePrefabs;
    private GameObject[] twistedWirePrefabs;

    private Dictionary<WireColor, Material> colorMaterials;

    private List<int> buttonToWire = new List<int>();

    private GameObject buttonObject1;
    private GameObject buttonObject2;
    private GameObject buttonObject3;
    private GameObject buttonObject4;
    private GameObject buttonObject5;
    private GameObject buttonObject6;
    private GameObject buttonObject7;
    private GameObject buttonObject8;

    private List<GameObject> buttonObjects = new List<GameObject>(){};
    
    

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
        buttonObject1 = GameObject.Find("Puzzle_UI/Button");
        buttonObject2 = GameObject.Find("Puzzle_UI/Button_(1)");
        buttonObject3 = GameObject.Find("Puzzle_UI/Button_(2)");
        buttonObject4 = GameObject.Find("Puzzle_UI/Button_(3)");
        buttonObject5 = GameObject.Find("Puzzle_UI/Button_(4)");
        buttonObject6 = GameObject.Find("Puzzle_UI/Button_(5)");
        buttonObject7 = GameObject.Find("Puzzle_UI/Button_(6)");
        buttonObject8 = GameObject.Find("Puzzle_UI/Button_(7)");
        
        buttonObjects.Add(buttonObject1);
        buttonObjects.Add(buttonObject2);
        buttonObjects.Add(buttonObject3);
        buttonObjects.Add(buttonObject4);
        buttonObjects.Add(buttonObject5);
        buttonObjects.Add(buttonObject6);
        buttonObjects.Add(buttonObject7);
        buttonObjects.Add(buttonObject8);

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

        InitializeWires();
    }

    public void ToggleWire(int position)
    {
        buttonObjects[position].GetComponent<Renderer>().material.color = Color.red;
        if(buttonToWire.Contains(position)){
        wires[buttonToWire.IndexOf(position)].status = !wires[buttonToWire.IndexOf(position)].status;
        Debug.Log("Wire Status" + buttonToWire.IndexOf(position) + wires[buttonToWire.IndexOf(position)].status);;
        CheckWires();}
    }

    private void CheckWires()
    {
        for (int i = 0; i < 2; i++)
        {
            if (wires[i].status != solution[i])
                return;
        }

        Debug.Log("Hooray");

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
