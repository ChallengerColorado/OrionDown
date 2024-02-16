using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Timer 
{

    private int totalSeconds;
    private int remainingSeconds;

    public Timer(int timeLimitSeconds)
    {
        totalSeconds = timeLimitSeconds;
        remainingSeconds = timeLimitSeconds;
    }

    public IEnumerator Run()
    {
        while (remainingSeconds > 0)
        {
            yield return new WaitForSeconds(1);
            remainingSeconds--;
            Debug.Log($"Time Left: {remainingSeconds}/{totalSeconds}");
        }
    }

}
