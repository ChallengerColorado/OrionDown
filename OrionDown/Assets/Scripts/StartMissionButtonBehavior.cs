using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static GameManager;

public class StartMission : MonoBehaviour
{

    public TMP_InputField codeInputField;
    public TMP_Text errorText;

    private static Dictionary<string, Difficulty> codeToDifficulty = new Dictionary<string, Difficulty>()
    {
        { "1", Difficulty.Easy },
        { "2", Difficulty.Medium },
        { "3", Difficulty.Difficult }
    };

    public void OnButtonPress() {
        if (codeInputField.text.Length == 0)
            errorText.text = "Please input a code";
        else if (!codeToDifficulty.ContainsKey(codeInputField.text))
                errorText.text = "Invalid code";
        else
            GameManager.Instance.StartGame(codeToDifficulty[codeInputField.text]);
    }


}
