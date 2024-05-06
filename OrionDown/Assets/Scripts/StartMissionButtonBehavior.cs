using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartMission : MonoBehaviour
{

    public TMP_InputField codeInputField;
    public TMP_Text errorText;
    
    public void OnButtonPress() {
        if (codeInputField.text.Length == 0)
        {
            errorText.text = "Please input a code";
        }
        else
        {
            GameManager.Instance.StartGame(codeInputField.text);
        }
    }


}
