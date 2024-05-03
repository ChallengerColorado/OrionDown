using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class GameManager : MonoBehaviour
{
    private static System.Random random = new System.Random();
    private GameObject[] modulePrefabs;

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

        modulePrefabs = new GameObject[]
        {
            Resources.Load<GameObject>("Modules/Wires"),
            Resources.Load<GameObject>("Modules/Maze"),
            Resources.Load<GameObject>("Modules/Passwords"),
            Resources.Load<GameObject>("Modules/Heat Shield")
        };
    }

    public void StartGame(Difficulty difficulty)
    {
        Debug.Log("Start with difficulty: " + difficulty.ToString());

        SceneManager.LoadScene(1);

        CreateModules(4);

        GameTimer = new Timer(300);
        StartCoroutine(GameTimer.Run);
    }

    public void PauseGame()
    {
        // GameTimer.SetPaused(true);
        SceneManager.LoadSceneAsync(3);
    }

    public void UnpauseGame()
    {
        // GameTimer.SetPaused(false);
        SceneManager.LoadSceneAsync(1);
    }

    public void StopGame(bool finished)
    {
        StopCoroutine(GameTimer.Run);
        SceneManager.LoadSceneAsync(finished ? 2 : 0);
    }

    private void CreateModules(int number)
    {
        List<Transform> presetPositions = GetPresetModulePositions();
        List<Transform> chosenPositions = new List<Transform>();

        for (int i = 0; i < number; i++)
        {
            int newIndex = random.Next(presetPositions.Count);
            chosenPositions.Add(presetPositions[newIndex]);
            presetPositions.RemoveAt(newIndex);
        }

        foreach (Transform t in chosenPositions)
        {
            Instantiate(modulePrefabs[random.Next(modulePrefabs.Length)]);
        }
    }

    private List<Transform> GetPresetModulePositions()
    {
        List<Transform> positions = new List<Transform>();

        for (int i = 1; i < 11; i++)
        {
            positions.Add(GameObject.Find("Module Position " + i).transform);
        }

        return positions;
    }
}
