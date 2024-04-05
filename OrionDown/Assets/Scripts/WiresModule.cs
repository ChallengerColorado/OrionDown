using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;

public class WiresModule : ModuleBehaviour
{
    [SerializeField] public GameObject[] topSockets;

    //dificulty system
    private int[][] wireConfig = new int[][]{
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
    };

    private int[] layout;

    private bool[] solution = { false, false, false, false, false, false, false };
    private Wire[] wires = new Wire[8];

    // Start is called before the first frame update
    void Start()
    {
        if (topSockets.Length != 8)
            throw new ArgumentException("topSockets must be of length 8.");

        layout = wireConfig[new System.Random().Next(wireConfig.Length)];
        InitializeWires();
    }

    public void ToggleWire(int position)
    {
        wires[position].WireStatus = !wires[position].WireStatus;
        CheckWires();
    }

    private void CheckWires()
    {
        for (int i = 0; i < 7; i++)
        {
            if (wires[i].WireStatus != solution[i])
                return;
        }

        Status = true;

    }

    private void InitializeWires() {
        for (int i = 0; i < layout.Length; i++) {
            wires[i] = new GameObject("Wire " + i).AddComponent<Wire>();
            // wires[i].Initialize(topSockets[i].transform, );
        }
    }

}
