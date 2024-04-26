using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class Passwords : MonoBehaviour
{
    private System.Random random = new System.Random();

    [SerializeField]
    public TMP_Text[] charDisplays;

    private char[][] characters = {
        new char[6],
        new char[6],
        new char[6],
        new char[6],
        new char[6]
    };

    private static char[] alphabet = new char[] {'a','b','c','d','e','h','i','j','k','l','n','q','r','s','t','u','v','y','z'};
    private static string[] words = {
        "orion",
        "orbit",
        "oxide",
        "optic",
        "admin",
        "about",
        "after",
        "agian",
        "below",
        "right" 
    };

    private int[] currentIndices = new int[5];

    private int[] solutionIndices = new int[5];


    private char[] wchars = new char[5];
    private char newchar;
    private void InitializeRound()
    {
        wchars = words[random.Next(words.Length)].ToCharArray();
        for (int i = 0; i < characters.Length; i++)
        {
            Debug.Log(wchars[i] + "wchar");
            characters[i][0] = wchars[i];
            Debug.Log(characters[i][0] + "charecters");
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

    private int remainingRounds = 3;
    public bool status = false;
    public void CheckPassword()
    {
        if (currentIndices.SequenceEqual(solutionIndices)){
            if (--remainingRounds == 0)
            {
                status = true;
                return;
            }
            InitializeRound();
        }
    }
}
