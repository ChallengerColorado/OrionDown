using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerText : MonoBehaviour
{

    public TMPro.TextMeshProUGUI m_TextMeshPro;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int remainingSeconds = GameManager.Instance.GameTimer.RemainingSeconds;

        int timerMinutes = remainingSeconds / 60;
        int timerSeconds = remainingSeconds % 60;


        m_TextMeshPro.text = $"{timerMinutes}:{timerSeconds:D2}";
    }
}
