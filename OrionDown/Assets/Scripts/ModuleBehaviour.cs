using System;
using TMPro;
using UnityEngine;

public class ModuleBehaviour : MonoBehaviour
{

    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text statusText;

    private bool _status = false;

    protected void SetStatus(bool status, string text) {
        if (text.Length != 2)
            throw new ArgumentException("Status must be of length 2");

        statusText.text = text;

        _status = status;
        if (status)
            GameManager.Instance.ModuleFixed();
    }

    protected bool GetStatus() { return _status; }

}
