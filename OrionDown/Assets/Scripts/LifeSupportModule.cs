using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class LifeSupportModule : ModuleBehaviour
{
    private System.Random random = new System.Random();

    [SerializeField]
    public TMP_Text[] charDisplays = new TMP_Text[5];

    private char[][] characters = {
        new char[6],
        new char[6],
        new char[6],
        new char[6],
        new char[6]
    };

    private static char[] alphabet = new char[] {'a','b','c','d','e','h','i','j','k','l','n','q','s','t','u','v','y','y'};
    private static string[] words = {
        "orion",
        "orbit",
        "oxide",
        "optic",
        "admin",
        "about",
        "after",
        "again",
        "below",
        "bezel",
        "write",
        "right",
        "sound",
        "point",
        "there",
        "their",
        "world",
        "soyuz",
        "learn",
        "would"
    };


    private int remainingRounds = 3;

    private int[] currentIndices = new int[5];

    private int[] solutionIndices = new int[5];


    private char[] wchars = new char[5];
    private char newchar;
    private void InitializeRound()
    {
        SetStatus(false, remainingRounds.ToString("D2"));

        wchars = words[random.Next(words.Length)].ToCharArray();
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i][0] = wchars[i];
            for (int j = 1; j < characters[i].Length;)
            {
                newchar = alphabet[random.Next(alphabet.Length)];
                if (!characters[i].Contains(newchar)){
                characters[i][j] = newchar;
                j++;
                }
            }
        }

        for(int i = 0; i < currentIndices.Length; i++)
        {
            currentIndices[i] = random.Next(5);
        }

        for (int i = 0; i < characters.Length; i++)
        {
            charDisplays[i].text = characters[i][currentIndices[i]].ToString();
        }
        
    }
    void Start() {
        InitializeRound();
    }

    // Update is called once per frame
    void Update(){
       

    }
    public void Cycle(int buttonPressed)
    {
        currentIndices[buttonPressed] = (currentIndices[buttonPressed] + 1) % characters[buttonPressed].Length;

        charDisplays[buttonPressed].text = characters[buttonPressed][currentIndices[buttonPressed]].ToString();
    }

    public void ReverseCycle(int buttonPressed)
    {
        if (currentIndices[buttonPressed] == 0)
            currentIndices[buttonPressed] = characters[buttonPressed].Length - 1;
        else
            currentIndices[buttonPressed] = (currentIndices[buttonPressed] - 1);

        charDisplays[buttonPressed].text = characters[buttonPressed][currentIndices[buttonPressed]].ToString();
    }

    public void CheckPassword()
    {
        if (currentIndices.SequenceEqual(solutionIndices)){
            if (--remainingRounds == 0)
            {
                SetStatus(true, "BB");
                return;
            }
            InitializeRound();
        }
    }
}
