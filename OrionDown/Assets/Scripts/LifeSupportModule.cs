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

    //Each list represents a each wheel of letters on the Life Support Module
    private char[][] letterWheels = {
        new char[6],
        new char[6],
        new char[6],
        new char[6],
        new char[6]
    };

    //The filler letters for the Life Support Module letter wheels
    //Missing letters are to prevent the player being abnle to spell more than one of the solution words
    private static char[] alphabet = new char[] {'a','b','c','d','e','h','i','j','k','l','n','q','s','t','u','v','y','y'};

    //The solution words that the player needs to sapell with the letter wheels
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

    //Tracks the letter wheel positions
    private int[] currentIndices = new int[5];


    //Tracks the correct letter wheel positions
    private int[] solutionIndices = new int[5];


    private char[] wchars = new char[5];
    private char newchar;
    private void InitializeRound()
    {
        //Status indicator for player
        SetStatus(false, remainingRounds.ToString("D2"));

        //The letters of the chosen solution word in order
        wchars = words[random.Next(words.Length)].ToCharArray();
        for (int i = 0; i < letterWheels.Length; i++)
        {
            //Solution letters are added to the letter wheels one per in order
            letterWheels[i][0] = wchars[i];
            //Random letters from the modified alphabet are added as filler
            for (int j = 1; j < letterWheels[i].Length;)
            {
                newchar = alphabet[random.Next(alphabet.Length)];
                if (!letterWheels[i].Contains(newchar)){
                letterWheels[i][j] = newchar;
                j++;
                }
            }
        }

        //What letters the letter wheels start on are randomized
        for(int i = 0; i < currentIndices.Length; i++)
        {
            currentIndices[i] = random.Next(5);
        }

        for (int i = 0; i < letterWheels.Length; i++)
        {
            charDisplays[i].text = letterWheels[i][currentIndices[i]].ToString();
        }
        
    }
    void Start() {
        InitializeRound();
    }

    // Update is called once per frame
    void Update(){
       

    }

    //Cycles the corisponding letter wheel in the positive direction
    public void Cycle(int buttonPressed)
    {
        currentIndices[buttonPressed] = (currentIndices[buttonPressed] + 1) % letterWheels[buttonPressed].Length;

        charDisplays[buttonPressed].text = letterWheels[buttonPressed][currentIndices[buttonPressed]].ToString();
    }

    //Cycles the corisponding letter wheel in the negative direction
    public void ReverseCycle(int buttonPressed)
    {
        if (currentIndices[buttonPressed] == 0)
            currentIndices[buttonPressed] = letterWheels[buttonPressed].Length - 1;
        else
            currentIndices[buttonPressed] = (currentIndices[buttonPressed] - 1);

        charDisplays[buttonPressed].text = letterWheels[buttonPressed][currentIndices[buttonPressed]].ToString();
    }

    //Checks the solution with the current letter wheel positions 
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
