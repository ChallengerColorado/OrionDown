using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static GameManager;


// button that begins the game
public class StartMission : MonoBehaviour
{
    
    public TMP_InputField codeInputField;
    public TMP_Text errorText; // text object for displaying error about incorrect code

    // maps codes that the user inputs to game difficulties
    private static Dictionary<string, Difficulty> codeToDifficulty = new Dictionary<string, Difficulty>()
    {
        { "1", Difficulty.Easy },
        { "2", Difficulty.Medium },
        { "3", Difficulty.Difficult }
    };

    public void OnButtonPress() {
        // if no code was entered, display an error
        if (codeInputField.text.Length == 0)
            errorText.text = "Please input a code";
        // if the code is not one of the predefined codes, display an error
        else if (!codeToDifficulty.ContainsKey(codeInputField.text))
                errorText.text = "Invalid code";
        // otherwise, get the corresponding difficulty and begin the game with 4 modules
        else
            GameManager.Instance.StartGame(codeToDifficulty[codeInputField.text]);
            GameManager.Instance.modulesBroken = 4;
    }


}
