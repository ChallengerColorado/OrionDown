using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager
{
    private static GameManager _instance;
    public static GameManager Instance {
        get
        {
            if (_instance == null) _instance = new GameManager();
            
            return _instance;
        }
    }

    public enum Difficulty
    {
        Easy,
        Medium,
        Difficult
    }

    GameManager()
    {
        if (_instance != null)
        {
            throw new InvalidOperationException("Only one instance of the GameManager may exist");
        }
        _instance = this;
    }

    public void BeginGame(Difficulty difficulty)
    {
        Debug.Log("Start with difficulty: " + difficulty.ToString());
    }
}
