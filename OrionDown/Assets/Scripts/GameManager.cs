using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public static GameManager Instance {
        get
        {
            if (_instance == null)
            {
                new GameObject("Game Manager", typeof(GameManager));
            }
            
            return _instance;
        }
    }

    public enum Difficulty
    {
        Easy,
        Medium,
        Difficult
    }

    public Timer GameTimer { get; private set; }

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame(Difficulty difficulty)
    {
        Debug.Log("Start with difficulty: " + difficulty.ToString());

        GameTimer = new Timer(300);
        StartCoroutine(GameTimer.Run);
    }

    public void PauseGame()
    {
        GameTimer.SetPaused(true);
    }

    public void StopGame()
    {
        StopCoroutine(GameTimer.Run);
    }

}
