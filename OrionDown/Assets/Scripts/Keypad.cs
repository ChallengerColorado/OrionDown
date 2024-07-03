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

    private List<List<int>> allSymbols = new List<List<int>>{
        new List<int>(){1, 2, 3, 4},
        new List<int>(){1, 2, 3, 5},
        new List<int>(){5, 2, 3, 4},
        new List<int>(){1, 5, 3, 4},
        new List<int>(){1, 2, 5, 4}
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

        symbolIndex = random.Next(allSymbols.Count);
        symbols = new List<int>(allSymbols[symbolIndex]);
        
        for (int i = 0; i < 4; i++) {
            int newindex = random.Next(symbols.Count);
            symbolsUsed.Add(symbols[newindex]);
            symbols.RemoveAt(newindex);

            buttonImages[i].color = Color.white;
            buttonImages[i].texture = Resources.Load<Texture2D>("Symbols/" + symbolsUsed[i].ToString("D2"));
            //Debug.Log("Symbols/" + symbolsUsed[i].ToString("D2"));
        }
    }
    public void ButtonPress(int buttonIndex) {
        if (!buttonIndices.Contains(buttonIndex))
        {
            buttonImages[buttonIndex].color = Color.gray;

            buttonIndices.Add(buttonIndex);
            if (buttonIndices.Count > 3 && GetStatus() == false)
            {
                CheckSolution();
            }
        }
    }

    private void CheckSolution() {
        for (int i = 0; i < 4; i++){

            int nextButtonIndex = allSymbols[symbolIndex].FindIndex(symbolsUsed[buttonIndices[i]].Equals);

            Debug.Log("Temp: " + tempIndex + ", Search: " + nextButtonIndex);

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
