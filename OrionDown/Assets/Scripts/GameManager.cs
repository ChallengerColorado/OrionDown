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

    private Timer timer;

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

    public void BeginGame(Difficulty difficulty)
    {
        Debug.Log("Start with difficulty: " + difficulty.ToString());

        timer = new Timer(300);
        StartCoroutine(timer.Run());
    }

}
