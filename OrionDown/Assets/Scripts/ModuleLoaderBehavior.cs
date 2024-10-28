using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// spawns modules when the game begins
public class ModuleLoaderBehavior : MonoBehaviour
{

    [SerializeField]
    public Transform[] presetMainControlModulePositions; // preset positions for modules on the main controls
    [SerializeField]
    public Transform[] presetPropulsionModulePositions; // preset positions for propulsion modules

    private GameObject[] mainControlModulePrefabs; // prefabs for modules on the main controls
    private GameObject propulsionModulePrefab; // prefabs for propulsion modules

    private static System.Random random = new System.Random();


    // Start is called before the first frame update
    void Start()
    {
        // load prefabs
        mainControlModulePrefabs = new GameObject[]
        {
            Resources.Load<GameObject>("Modules/Radiation Protection"),
            Resources.Load<GameObject>("Modules/Life Support"),
            Resources.Load<GameObject>("Modules/Heat Shield"),
            Resources.Load<GameObject>("Modules/Keypad")
        };

        propulsionModulePrefab = Resources.Load<GameObject>("Modules/Propulsion");

        // create 3 main control modules and one propulsion module
        CreateModules(3, 1);
    }

    private void CreateModules(int mainControlNumber, int propulsionNumber)
    {
        // indices in mainControlModulePrefabs of remaining modules to choose from
        List<int> availableMainControlModuleIndices = new List<int>();
        // chosen module indices
        List<int> chosenMainControlModuleIndices = new List<int>();

        for (int i = 0; i < mainControlNumber; i++)
        {
            // if all module options have been exhausted, refresh option list
            if (availableMainControlModuleIndices.Count == 0) {
                availableMainControlModuleIndices = Enumerable.Range(0, mainControlModulePrefabs.Length).ToList();
            }

            // choose a random item from the list of available module indices
            int newIndex = random.Next(availableMainControlModuleIndices.Count);
            // add the item to the list of chosen modules
            chosenMainControlModuleIndices.Add(availableMainControlModuleIndices[newIndex]);
            // pop the item off of the list of available modules
            availableMainControlModuleIndices.RemoveAt(newIndex);

        }

        // available preset positions for main control modules
        List<Transform> availableMainControlPositions = new List<Transform>(presetMainControlModulePositions);

        foreach (int i in chosenMainControlModuleIndices)
        {
            // choose one of the remaining available transforms and remove it from the list
            int newPositionIndex = random.Next(availableMainControlPositions.Count);
            Transform newPosition = availableMainControlPositions[newPositionIndex];
            availableMainControlPositions.RemoveAt(newPositionIndex);

            // instantiate the module prefab with the chosen transform
            Instantiate(mainControlModulePrefabs[i], newPosition);
        }

        // available preset positions for propulsion modules
        List<Transform> availablePropulsionPositions = new List<Transform>(presetPropulsionModulePositions);

        for (int i = 0; i < propulsionNumber; i++)
        {
            // choose one of the remaining available transforms and remove it from the list
            int newPositionIndex = random.Next(availablePropulsionPositions.Count);
            Transform newPosition = availablePropulsionPositions[newPositionIndex];
            availablePropulsionPositions.RemoveAt(newPositionIndex);

            // instantiate the module prefab with the chosen transform
            Instantiate(propulsionModulePrefab, newPosition);
        }
    }
}
