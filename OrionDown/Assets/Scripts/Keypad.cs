using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : ModuleBehaviour
{

    private System.Random random = new System.Random();

    private List<List<string>> allSymbols = new List<List<string>>{
        new List<string>(){"a", "b", "c", "d"},
        new List<string>(){"a", "b", "c", "e"},
        new List<string>(){"e", "b", "c", "d"},
        new List<string>(){"a", "e", "c", "d"},
        new List<string>(){"a", "b", "e", "d"}
    };
    private List<int> buttonIndices = new List<int>();
    
    private List<string> symbolsUsed = new List<string>();

    private int tempIndex = -1;

    private bool complete = false;

    private int symbolIndex;
    private List<string> symbols;

    // Start is called before the first frame update
    void Start()
    {
    
       InitializeRound();
    }

    private void InitializeRound()
    {
        symbolIndex = random.Next(allSymbols.Count);
        symbols = new List<string>(allSymbols[symbolIndex]);
        
        for (int i = 0; i < 4; i++) {
            int newindex = random.Next(symbols.Count);
            symbolsUsed.Add(symbols[newindex]);
            symbols.RemoveAt(newindex);
        }
    }
    private void ButtonPress(int buttonIndex) {
        buttonIndices.Add(buttonIndex);
        if (buttonIndices.Count > 3 && complete == false){
            CheckSolution();
        }
    }

    private void CheckSolution() {
        for (int i = 0; i <4; i++){
            if (tempIndex > allSymbols[symbolIndex].BinarySearch(symbolsUsed[buttonIndices[i]])){
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
