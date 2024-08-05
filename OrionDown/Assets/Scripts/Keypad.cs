using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : ModuleBehaviour
{

    [SerializeField] public RawImage[] buttonImages;

    private System.Random random = new System.Random();

    //Each int represents a symbol that will be displayed and all 4 sybmols displayed will be from the same list
    private List<List<int>> allSymbols = new List<List<int>>{
        new List<int>(){ 1,  2,  3,  4,  5,  6,  7,  8},
        new List<int>(){ 5,  9, 10, 11, 12, 13,  2, 14},
        new List<int>(){15, 10, 16, 17,  4, 18, 19, 20},
        new List<int>(){ 4, 16,  9,  6, 13, 18,  7, 12},
        new List<int>(){ 8, 15,  2, 10,  3, 14, 19, 12},
    };
    private List<int> buttonIndices;
    
    private List<int> symbolsUsed;

    private int tempIndex;

    private int symbolIndex;
    private List<int> symbols;

    // Start is called before the first frame update
    void Start()
    {
       InitializeRound();
    }

    private void InitializeRound()
    {
        buttonIndices = new List<int>();
        symbolsUsed = new List<int>();

        tempIndex = -1;

        //Index of List of symbols for check solution
        symbolIndex = random.Next(allSymbols.Count);
        //List of symbols to be used
        symbols = new List<int>(allSymbols[symbolIndex]);
        
        //Symbols to be displayed from list
        for (int i = 0; i < 4; i++) {
            int newindex = random.Next(symbols.Count);
            symbolsUsed.Add(symbols[newindex]);
            symbols.RemoveAt(newindex);

            buttonImages[i].color = Color.white;
            buttonImages[i].texture = Resources.Load<Texture2D>("Symbols/" + symbolsUsed[i].ToString("D2"));
        }
    }

    
    public void ButtonPress(int buttonIndex) {
        //Highlights pressed button for player
        if (!buttonIndices.Contains(buttonIndex))
        {
            buttonImages[buttonIndex].color = Color.gray;
            //Stores pressed buttons
            buttonIndices.Add(buttonIndex);
            //Checks when all butons are selected
            if (buttonIndices.Count > 3 && GetStatus() == false)
            {
                CheckSolution();
            }
        }
    }

    private void CheckSolution() {
        for (int i = 0; i < 4; i++){
            //Decodes from button index to the position of the symbol on the button to position in symbols
            int nextButtonIndex = allSymbols[symbolIndex].FindIndex(symbolsUsed[buttonIndices[i]].Equals);

            //checks if the symbol appears later in the list than the previous
            if (tempIndex > nextButtonIndex)
            {
                InitializeRound();
                return;
            }

            tempIndex = nextButtonIndex;
        }

        SetStatus(true, "BB");
    }
}
