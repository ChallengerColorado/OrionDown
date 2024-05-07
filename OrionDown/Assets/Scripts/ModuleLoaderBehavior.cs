using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleLoaderBehavior : MonoBehaviour
{

    [SerializeField]
    public Transform[] presetModulePositions;

    private GameObject[] modulePrefabs;

    private static System.Random random = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        modulePrefabs = new GameObject[]
        {
            Resources.Load<GameObject>("Modules/Wires"),
            Resources.Load<GameObject>("Modules/Maze"),
            Resources.Load<GameObject>("Modules/Passwords"),
            Resources.Load<GameObject>("Modules/Heat Shield")
        };

        CreateModules(4);
    }

    private void CreateModules(int number)
    {
        Debug.Log("CreateModules");
        List<Transform> availablePositions = new List<Transform>(presetModulePositions);
        List<Transform> chosenPositions = new List<Transform>();

        for (int i = 0; i < number; i++)
        {
            int newIndex = random.Next(availablePositions.Count);
            chosenPositions.Add(availablePositions[newIndex]);
            availablePositions.RemoveAt(newIndex);
        }

        foreach (Transform t in chosenPositions)
        {
            Instantiate(modulePrefabs[random.Next(modulePrefabs.Length)], t);
        }
    }
}
