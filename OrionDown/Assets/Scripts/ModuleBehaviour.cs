using System;
using TMPro;
using UnityEngine;

public class ModuleBehaviour : MonoBehaviour
{

    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text statusText;

    bool _status;
    public bool Status {
        get
        {
            return _status;
        }
        protected set
        {
            _status = value;
            SetStatusText("BB");
        }
    }

    private void SetStatusText(string status) {
        if (status.Length != 2)
            throw new ArgumentException("Status must be of length 2");

        statusText.text = status;
    }

}
