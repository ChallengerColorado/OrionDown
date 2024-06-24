using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HeatShield : ModuleBehaviour
{
    [SerializeField] TMP_Text givenWordDisplay;
    [SerializeField] Button[] buttons;

    private int remainingRounds;
    private Dictionary<GameManager.Difficulty, int> difficultyToRounds = new Dictionary<GameManager.Difficulty, int>()
    {
        { GameManager.Difficulty.Easy,      2 },
        { GameManager.Difficulty.Medium,    3 },
        { GameManager.Difficulty.Difficult, 4 },
    };

    private System.Random random = new System.Random();

    // Pool of words to choose from
    private static string[] words = new string[] { "right", "soyuz", "orbit", "optic", "their", "world", "would", "bezel", "oxide", "write", "there", "orion" };
    // Maps the word read off of a button to the corresponding list of words
    private static Dictionary<string, string[]> wordLists = new Dictionary<string, string[]>() {
        { "right", new string[] { "world", "bezel", "write", "orion"} },
        { "soyuz", new string[] { "bezel", "orbit", "right", "optic"} },
        { "orbit", new string[] { "soyuz", "would", "right", "their"} },
        { "optic", new string[] { "oxide", "world", "their", "write"} },
        { "their", new string[] { "optic", "orion", "bezel", "there"} },
        { "world", new string[] { "would", "oxide", "orbit", "world"} },
        { "would", new string[] { "orion", "write", "world", "right"} },
        { "bezel", new string[] { "optic", "orbit", "soyuz", "would"} },
        { "oxide", new string[] { "write", "right", "oxide", "soyuz"} },
        { "write", new string[] { "their", "world", "there", "orbit"} },
        { "there", new string[] { "world", "optic", "there", "bezel"} },
        { "orion", new string[] { "orbit", "their", "soyuz", "right"} },
    };
    // Maps the word being displayed to the index of the button to read
    private static Dictionary<string, int> buttonToRead = new Dictionary<string, int>() {
        { "right", 4 },
        { "soyuz", 1 },
        { "orbit", 3 },
        { "optic", 5 },
        { "their", 0 },
        { "world", 2 },
        { "would", 3 },
        { "bezel", 0 },
        { "oxide", 1 },
        { "write", 4 },
        { "there", 5 },
        { "orion", 2 },
    };

    private int buttonToPressIndex;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            // Necessary or the variable i will be captured in closure, so all buttons will get index 6
            // This copies the value of i at the given iteration, which will not change
            // See https://stackoverflow.com/questions/3168375/using-the-iterator-variable-of-foreach-loop-in-a-lambda-expression-why-fails
            int buttonIndex = i; 
            buttons[i].onClick.AddListener(() => ButtonPress(buttonIndex));
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().fontSize = 48;
        }

        remainingRounds = difficultyToRounds[GameManager.Instance.currentDifficulty];

        SetStatus(false, "ö*");
        InitializeRound();
    }

    private void InitializeRound()
    {
        // The word appearing on the button that the user must read
        int wordForListIndex = random.Next(words.Length);
        string wordForList = words[wordForListIndex];
        Debug.Log(wordForList);

        // The word appearing on the button that the user must press
        string[] list = wordLists[wordForList];
        string wordToPress = list[random.Next(list.Length)];
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
        SortedSet<int> buttonWordIndexSet = new SortedSet<int>() { wordForListIndex };

        if (wordForListIndex != wordToPressIndex)
            buttonWordIndexSet.Add(wordToPressIndex);

        // Fill remaining buttons with randomly-chosen words
        for (int i = buttonWordIndexSet.Count; i < 6; i++)
        {
            // Index of next word to add
            int newIndex = random.Next(words.Length - i);
            foreach (int j in buttonWordIndexSet)
            {
                // Skip over already-chosen words
                if (newIndex >= j)
                    newIndex++;
            }

            buttonWordIndexSet.Add(newIndex);
        }

        List<int> buttonWordIndexList = buttonWordIndexSet.ToList();

        // Randomize order of words on buttons
        int[] shuffledButtonWordIndices = new int[6];

        for (int i = 0; i < shuffledButtonWordIndices.Length; i++)
        {
            int nextItemIndex = random.Next(buttonWordIndexList.Count);
            shuffledButtonWordIndices[i] = buttonWordIndexList[nextItemIndex];
            buttonWordIndexList.RemoveAt(nextItemIndex);
        }

        //foreach (var i in shuffledButtonWordIndices) Debug.Log(i);

        // Get the actual words associated with the indices in shuffledButtonWordIndices and assign them to the buttons
        for (int i = 0; i < 6; i++)
        {
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = words[shuffledButtonWordIndices[i]];
        }
        bool finished = false;
        for (int i = 0; i < list.Length && !finished; i++)
        {
            for (int j = 0; j < 6 && !finished; j++){
                if (list[i] == (buttons[j].GetComponentInChildren<TextMeshProUGUI>().text))
                {
                    buttonToPressIndex = j;
                    finished = true;
                }
            }
        }

        Debug.Log("Target button: " + buttonToPressIndex);

        // Button to be read
        int buttonToReadIndex = 0;
        for (int i = 0; i < 6; i++){
                if (wordForList == (buttons[i].GetComponentInChildren<TextMeshProUGUI>().text))
                {
                    buttonToReadIndex = i;
                    break;
                }
        }
        // Possible words to display in order to direct the user to read the correct button
        List<string> displayCandidates = new List<string>();

        foreach (string key in buttonToRead.Keys)
        {
            if (buttonToRead[key] == buttonToReadIndex)
                displayCandidates.Add(key);
        }

        // Select random candidate
        givenWordDisplay.text = displayCandidates[random.Next(displayCandidates.Count)];
    }

    void ButtonPress(int buttonIndex)
    {
        Debug.Log("Button: " + buttonIndex);

        if (buttonIndex == buttonToPressIndex)
        {
            if (--remainingRounds == 0)
            {
                SetStatus(true, "BB");
                return;
            }
        }

        InitializeRound();
    }
}
