using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private static Dictionary<string, Difficulty> CodeToDifficulty = new Dictionary<string, Difficulty>()
    {
        { "1", Difficulty.Easy },
        { "2", Difficulty.Medium },
        { "3", Difficulty.Difficult }
    };

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
    public void StartGame(string code)
    {
        Difficulty difficulty = CodeToDifficulty[code];

        Debug.Log("Start with difficulty: " + difficulty.ToString());

        SceneManager.LoadScene(1);

        GameTimer = new Timer(300);
        StartCoroutine(GameTimer.Run);
    }

    public void PauseGame()
    {
        GameTimer.SetPaused(true);
    }

    public void StopGame(bool finished)
    {
        StopCoroutine(GameTimer.Run);
        SceneManager.LoadScene(finished ? 2 : 0);
    }

}
