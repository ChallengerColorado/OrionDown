using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// class to keep track of the time remaining in the game
public sealed class Timer
{

    public int TotalSeconds; // total amount of time in game
    public int RemainingSeconds; // time left before game ends

    private bool paused = false; // whether or not the game is paused

    // stores the RunCoroutine
    public IEnumerator Run { get; private set; }

    public Timer(int timeLimitSeconds)
    {
        TotalSeconds = timeLimitSeconds;
        RemainingSeconds = timeLimitSeconds; // start with full amount of time

        Run = RunCoroutine();
    }

    private IEnumerator RunCoroutine()
    {
        while (RemainingSeconds > 0)
        {
            // decrement number of remaining seconds every second while game is unpaused
            if (!paused) {
                RemainingSeconds--;
            }
            yield return new WaitForSeconds(1);
        }

        // once time runs out, end the game with a loss
        GameManager.Instance.StopGame(true, false);
    }

    public void SetPaused(bool paused)
    {
        this.paused = paused;
    }

}
