using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : MonoBehaviour
{

    private System.Random random = new System.Random();

    private List<List<string>> allSymbols = new List<List<string>>{
    private List<string> symbols1 = new List<string>(){"a", "b", "c", "d"};
    private List<string> symbols2 = new List<string>(){"a", "b", "c", "e"};
    private List<string> symbols3 = new List<string>(){"e", "b", "c", "d"};
    private List<string> symbols4 = new List<string>(){"a", "e", "c", "d"};
    private List<string> symbols5 = new List<string>(){"a", "b", "e", "d"};
    }
    private List<int> buttonIdicies = new List<int>;
    
    private List<string> symbolsUsed = new List<string>;

    private int tempIndex = -1;

    private bool complete = false;

    // Start is called before the first frame update
    void Start()
    {
    
       InitializeRound();
    }

    private void InitializeRound()
    {
        private int symbolIndex = system.Next(allsymbols.Count)
        private List<string> symbols = new List<string>(allSymbols[symbolIndex]);
        
        for (int i = 0; i i < 4; i++){
            private int newindex = system.Next(symbols.Count);
            symbolsUsed.Add(symbols[newindex]);
            symbols.RemoveAt(newindex);
        }
    }
    private void ButtonPress(int buttonIndex) {
        buttonIndices.Add(buttonIndex);
        if (buttonIndex.Count > 3 and complete = false;){
            CheckSolution();
        }
    }

    private void CheckSolution() {
        for (int i = 0; i <4; i++){
            if (tempIndex > allSymbols[symbolIndex].BinarySearch(symbolsUsed[buttonIndices[i]]);){
                tempIndex = -1;
                buttonIndices.Clear();
                return;
            }
                tempIndex = allSymbols[symbolIndex].BinarySearch(symbolsUsed[buttonIndices[i]]);
        }
        SetStatus(true, "BB");
        complete = true;
        return;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
