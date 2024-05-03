using System;
using System.Collections;
using System.Collections.Generic;
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

        SceneManager.LoadScene(1);

        GameTimer = new Timer(300);
        StartCoroutine(GameTimer.Run);
    }

    public void PauseGame()
    {
        GameTimer.SetPaused(true);
        SceneManager.LoadSceneAsync(3);
    }

    public void UnpauseGame()
    {
        GameTimer.SetPaused(false);
        SceneManager.LoadSceneAsync(1);
    }

    public void StopGame(bool finished)
    {
        StopCoroutine(GameTimer.Run);
        SceneManager.LoadSceneAsync(finished ? 2 : 0);
    }

}
