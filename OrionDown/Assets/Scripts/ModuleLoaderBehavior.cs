using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModuleLoaderBehavior : MonoBehaviour
{

    [SerializeField]
    public Transform[] presetMainControlModulePositions;
    [SerializeField]
    public Transform[] presetPropulsionModulePositions;

    private GameObject[] mainControlModulePrefabs;
    private GameObject propulsionModulePrefab;

    private static System.Random random = new System.Random();


    // Start is called before the first frame update
    void Start()
    {
        mainControlModulePrefabs = new GameObject[]
        {
            Resources.Load<GameObject>("Modules/Radiation Protection"),
            Resources.Load<GameObject>("Modules/Life Support"),
            Resources.Load<GameObject>("Modules/Heat Shield"),
            Resources.Load<GameObject>("Modules/Keypad")
        };

        propulsionModulePrefab = Resources.Load<GameObject>("Modules/Propulsion");

        CreateModules(3, 1);
    }

    private void CreateModules(int mainControlNumber, int propulsionNumber)
    {
        List<int> availableMainControlModuleIndices = new List<int>();
        List<int> chosenMainControlModuleIndices = new List<int>();

        for (int i = 0; i < mainControlNumber; i++)
        {
            // if all module options have been exhausted, refresh option list
            if (availableMainControlModuleIndices.Count == 0) {
                availableMainControlModuleIndices = Enumerable.Range(0, mainControlModulePrefabs.Length - 1).ToList();
            }

            int newIndex = random.Next(availableMainControlModuleIndices.Count);
            chosenMainControlModuleIndices.Add(availableMainControlModuleIndices[newIndex]);
            availableMainControlModuleIndices.RemoveAt(newIndex);

        }

        List<Transform> availableMainControlPositions = new List<Transform>(presetMainControlModulePositions);

        foreach (int i in chosenMainControlModuleIndices)
        {
            int newPositionIndex = random.Next(availableMainControlPositions.Count);
            Debug.Log("new index: " + newPositionIndex + ", count: " + availableMainControlPositions.Count);
            Transform newPosition = availableMainControlPositions[newPositionIndex];
            availableMainControlPositions.RemoveAt(newPositionIndex);

            Instantiate(mainControlModulePrefabs[i], newPosition);
        }

        
        List<Transform> availablePropulsionPositions = new List<Transform>(presetPropulsionModulePositions);

        for (int i = 0; i < propulsionNumber; i++)
        {
            int newPositionIndex = random.Next(availablePropulsionPositions.Count);
            Transform newPosition = availablePropulsionPositions[newPositionIndex];
            availablePropulsionPositions.RemoveAt(newPositionIndex);

            Instantiate(propulsionModulePrefab, newPosition);
        }
    }
}
