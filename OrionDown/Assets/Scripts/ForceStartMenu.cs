using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForceStartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [RuntimeInitializeOnLoadMethod]
    static void ForceStart()
    {
        SceneManager.LoadScene(0);
    }
}
