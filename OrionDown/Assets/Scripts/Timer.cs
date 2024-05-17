using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Timer
{

    public int TotalSeconds { get; private set; }
    public int RemainingSeconds { get; private set; }

    private bool paused = false;

    // either 
    public IEnumerator Run { get; private set; }

    public Timer(int timeLimitSeconds)
    {
        TotalSeconds = timeLimitSeconds;
        RemainingSeconds = timeLimitSeconds;

        Run = RunCoroutine();
    }

    private IEnumerator RunCoroutine()
    {
        while (RemainingSeconds > 0)
        {
            if (!paused) {
            RemainingSeconds--;
            }
            yield return new WaitForSeconds(1);
        }
        GameManager.Instance.StopGame(true, false);
    }

    public void SetPaused(bool paused)
    {
        this.paused = paused;
    }

}
