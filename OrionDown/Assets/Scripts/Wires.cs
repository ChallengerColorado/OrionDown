using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wires : ModuleBehaviour
{
    //dificulty system
    private Dictionary<int, int[]> WireConfig = new Dictionary<int, int[]>(){
        {1, new int[] {0,3,0,3,0,6,0}},
        {2, new int[] {3,1,0,4,0,2,0}},
        {3, new int[] {4,5,0,3,0,6,0}},
        {4, new int[] {4,5,0,3,0,6,0}},
        {5, new int[] {5,5,0,3,0,6,0}},
        {6, new int[] {5,5,0,3,0,6,0}},
        {7, new int[] {5,5,0,3,0,6,0}},
        {8, new int[] {6,5,0,3,0,6,0}},
        {9, new int[] {6,5,0,3,0,6,0}},
        {10, new int[] {6,5,0,3,0,6,0}}
    };

    private bool[] solution = { false, false, false, false, false, false, false };
    private bool[] currentStates = { false, false, false, false, false, false, false };

    // Start is called before the first frame update
    void Start()
    {

    }

    public void ToggleWire(int position)
    {
        currentStates[position] = !currentStates[position];
        CheckWires();
    }

    private void CheckWires()
    {
        for (int i = 0; i < 7; i++)
        {
            if (currentStates[i] != solution[i])
                return;
        }

        Status = true;

    }

}
