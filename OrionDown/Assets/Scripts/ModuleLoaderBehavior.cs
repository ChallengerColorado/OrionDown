using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleLoaderBehavior : MonoBehaviour
{

    [SerializeField]
    public Transform[] presetModulePositions;

    private GameObject[] modulePrefabs;

    private static System.Random random = new System.Random();

    private static List<int> prefabIndicies = new List<int>();  

    // Start is called before the first frame update
    void Start()
    {
        modulePrefabs = new GameObject[]
        {
            Resources.Load<GameObject>("Modules/Propulsion"),
            Resources.Load<GameObject>("Modules/Radiation Protection"),
            Resources.Load<GameObject>("Modules/Life Support"),
            Resources.Load<GameObject>("Modules/Heat Shield"),
            Resources.Load<GameObject>("Modules/Keypad")
        };

        CreateModules(5);
    }

    private void CreateModules(int number)
    {
        List<Transform> availablePositions = new List<Transform>(presetModulePositions);
        List<Transform> chosenPositions = new List<Transform>();

        for (int i = 0; i < number; i++)
        {
            int newIndex = random.Next(availablePositions.Count);
            chosenPositions.Add(availablePositions[newIndex]);
            availablePositions.RemoveAt(newIndex);
        }
        int newIndex2;
        for (int i = 0; i < modulePrefabs.Length; i++){
        prefabIndicies.Add(i);
        }
        foreach (Transform t in chosenPositions)
        {
            if (prefabIndicies.Count == 0){
                for (int j = 0; j < modulePrefabs.Length; j++){
                prefabIndicies.Add(j);
                }
            }
            newIndex2 = random.Next(prefabIndicies.Count);
            Instantiate(modulePrefabs[prefabIndicies[newIndex2]], t);
            prefabIndicies.RemoveAt(newIndex2);
        }
    }
}
