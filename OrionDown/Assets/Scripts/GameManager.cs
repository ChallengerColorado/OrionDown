using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public sealed class GameManager : ScriptableSingleton<GameManager>
{

    public enum Difficulty
    {
        Easy,
        Medium,
        Difficult
    }

    private GameObject _stateManager;
    public GameObject StateManager
    {
        get
        {
            return _stateManager ?? new GameObject("[State Manager]", typeof(StateManager));
        }
    }

    private Timer timer;

    public void BeginGame(Difficulty difficulty)
    {
        Debug.Log("Start with difficulty: " + difficulty.ToString());

        timer = new Timer(300);
        StateManager.GetComponent<StateManager>().BeginCoroutine(timer.Run());
    }

    private sealed class Timer
    {

        private int remainingSeconds;

        public Timer(int timeLimitSeconds) {
            remainingSeconds = timeLimitSeconds;
        }
        public IEnumerator Run()
        {
            while (remainingSeconds > 0)
            {
                yield return new WaitForSeconds(1);
                remainingSeconds--;
                Debug.Log($"Time Left: {remainingSeconds}");
            }
        }

    }

}
