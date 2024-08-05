using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// code to run the HUD timer display
public class TimerText : MonoBehaviour
{
    // timer text object
    public TMPro.TextMeshProUGUI m_TextMeshPro;

    // Update is called once per frame
    void Update()
    {
        int remainingSeconds = GameManager.Instance.GameTimer.RemainingSeconds;

        int timerMinutes = remainingSeconds / 60;
        int timerSeconds = remainingSeconds % 60;


        m_TextMeshPro.text = $"{timerMinutes}:{timerSeconds:D2}";
    }
}
