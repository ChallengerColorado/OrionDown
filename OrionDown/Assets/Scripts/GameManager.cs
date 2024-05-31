using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class GameManager : MonoBehaviour
{
    public GameObject test;

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

    public enum Missionstatus
    {
        Prep,
        Inprogress,
        Lost,
        Won
    }

    public Difficulty currentDifficulty;
    public Missionstatus currentMissionstatus = Missionstatus.Prep;

    public int modulesBroken = 4;
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

        currentDifficulty = difficulty;

        SceneManager.LoadScene(1);

        GameTimer = new Timer(300);
        StartCoroutine(GameTimer.Run);
    }

    public void ModuleFixed(){
        modulesBroken -= 1;
        Debug.Log(modulesBroken);
        if (modulesBroken < 1){
            StopGame(true, true);
            Debug.Log("Hooray");
        }
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

    public void StopGame(bool finished, bool win)
    {
        StopCoroutine(GameTimer.Run);
        if (finished){
            SceneManager.LoadSceneAsync(2);
            currentMissionstatus = win ? Missionstatus.Won : Missionstatus.Lost;
        }
    }
}
