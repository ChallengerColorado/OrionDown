using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadiationProtectionButton : MonoBehaviour
{
    [SerializeField] private GameObject m_maze;

    private RadiationProtectionModule m_mazescript;
    [SerializeField] private RadiationProtectionModule.Move lastmove;
    // Start is called before the first frame update
    void Start()
    {
        m_mazescript =  m_maze.GetComponent<RadiationProtectionModule>();
    }
    public void OnButtonPress() {
        m_mazescript.MazePositioningSystem(lastmove);
        Debug.Log("3");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
