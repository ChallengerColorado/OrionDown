using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HeatShield : ModuleBehaviour
{
    [SerializeField] TMP_Text givenWordDisplay;
    [SerializeField] Button[] buttons;

    public int remainingRounds;

    private System.Random random = new System.Random();

    // Pool of words to choose from
    private static string[] words = new string[] { };
    // Maps the word read off of a button to the corresponding list of words
    private static Dictionary<string, string[]> wordLists = new Dictionary<string, string[]>() {};
    // Maps the word being displayed to the index of the button to read
    private static Dictionary<string, int> buttonToRead = new Dictionary<string, int>() {};

    private int buttonToPressIndex;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.AddListener(() => ButtonPress(i));
        }

        remainingRounds = 3;
        InitializeRound();
    }

    private void InitializeRound()
    {
        // The word appearing on the button that the user must read
        int wordForListIndex = random.Next(words.Length);
        string wordForList = words[wordForListIndex];

        // The word appearing on the button that the user must press
        string wordToPress = wordLists[wordForList][random.Next(wordLists[wordForList].Length)];
        int wordToPressIndex = 0;

        // Find index of wordToPress in words
        for (int i = 0; i < words.Length; i++)
        {
            if (wordToPress == words[i])
            {
                wordToPressIndex = i;
                break;
            }
        }

        // List to store the indices in words of all of the words to appear on buttons
        List<int> buttonWordIndices = new List<int>() { wordForListIndex };

        if (wordForListIndex != wordToPressIndex)
            buttonWordIndices.Add(wordToPressIndex);

        // Fill remaining buttons with randomly-chosen words
        for (int i = buttonWordIndices.Count; i < 6; i++)
        {
            // Index of next word to add
            int newIndex = random.Next(words.Length - i);
            foreach (int j in buttonWordIndices)
            {
                // Skip over already-chosen words
                if (newIndex >= j)
                    newIndex++;
            }

            buttonWordIndices.Add(newIndex);
        }

        // Randomize order of words on buttons
        int[] shuffledButtonWordIndices = new int[6];

        for (int i = 0; i < shuffledButtonWordIndices.Length; i++)
        {
            int nextItem = random.Next(buttonWordIndices.Count);
            shuffledButtonWordIndices[i] = buttonWordIndices[nextItem];
            buttonWordIndices.Remove(nextItem);

            if (nextItem == wordToPressIndex)
                buttonToPressIndex = i;
        }

        // Get the actual words associated with the indices in shuffledButtonWordIndices and assign them to the buttons
        for (int i = 0; i < shuffledButtonWordIndices.Length; i++)
        {
            buttons[i].GetComponentInChildren<TextMeshPro>().text = words[shuffledButtonWordIndices[i]];
        }

        // Possible words to display in order to direct the user to read the correct button
        List<string> displayCandidates = new List<string>();

        foreach (string key in buttonToRead.Keys)
        {
            if (buttonToRead[key] == buttonToPressIndex)
                displayCandidates.Add(key);
        }

        // Select random candidate
        givenWordDisplay.text = displayCandidates[random.Next(displayCandidates.Count)];
    }

    void ButtonPress(int buttonIndex)
    {
        if (buttonIndex == buttonToPressIndex)
        {
            EndRound();
        }
    }

   private void EndRound()
    {
        if (--remainingRounds == 0)
            Status = true;
        else
            InitializeRound();
    }
}
