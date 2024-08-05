using System;
using TMPro;
using UnityEngine;

public class ModuleBehaviour : MonoBehaviour
{

    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text statusText;

    private bool _status = false;

    //Module statuses are comprised of wether the module is solved and a 2 long string which is used as an indicator to the player
    protected void SetStatus(bool status, string text) {
        if (text.Length != 2)
            throw new ArgumentException("Status must be of length 2");

        statusText.text = text;

        _status = status;
        //Everytime a module is fixed the gamemanager tracks it
        if (status)
            GameManager.Instance.ModuleFixed();
    }

    protected bool GetStatus() { return _status; }

}
