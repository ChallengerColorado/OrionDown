using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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


    //Difficulty controls the number of rounds and/or the difficulty of one round for each module
    public enum Difficulty
    {
        Easy,
        Medium,
        Difficult
    }

    //All states the game can be in
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

    //Create instance on Awake
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

    //On game start the timer is started at 5 minutes and it loads the main game scene
    public void StartGame(Difficulty difficulty)
    {
        currentDifficulty = difficulty;

        SceneManager.LoadScene(1);

        GameTimer = new Timer(300);
        StartCoroutine(GameTimer.Run);
    }

    //Run when a module is fixed, tracks how many are still broken
    public void ModuleFixed(){
        modulesBroken -= 1;
        if (modulesBroken < 1){
            //Win condition
            StopGame(true, true);
        }
    }

    //Stops timer and pulls up pause screen(currently unused)
    public void PauseGame()
    {
        GameTimer.SetPaused(true);
        SceneManager.LoadSceneAsync(3);
    }

    //Stops timer and pulls up pause screen(currently unused)
    public void UnpauseGame()
    {
        GameTimer.SetPaused(false);
        SceneManager.LoadSceneAsync(1);
    }

    //Determines when and how the game ends win or lose
    public void StopGame(bool finished, bool win)
    {
        StopCoroutine(GameTimer.Run);
        if (finished){
            SceneManager.LoadSceneAsync(2);
            currentMissionstatus = win ? Missionstatus.Won : Missionstatus.Lost;
        } else
            SceneManager.LoadSceneAsync(0);
    }
}
