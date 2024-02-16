using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Timer
{

    private int totalSeconds;
    private int remainingSeconds;

    private bool paused = false;

    // either 
    public IEnumerator Run { get; private set; }

    public Timer(int timeLimitSeconds)
    {
        totalSeconds = timeLimitSeconds;
        remainingSeconds = timeLimitSeconds;

        Run = RunCoroutine();
    }

    private IEnumerator RunCoroutine()
    {
        while (remainingSeconds > 0 && !paused)
        {
            yield return new WaitForSeconds(1);
            remainingSeconds--;
            Debug.Log($"Time Left: {remainingSeconds}/{totalSeconds}");
        }
    }

    public void SetPaused(bool paused)
    {
        this.paused = paused;
    }

}
