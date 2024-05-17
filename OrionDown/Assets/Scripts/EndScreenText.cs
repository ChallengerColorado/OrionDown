using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenText : MonoBehaviour
{

    public TMPro.TextMeshProUGUI m_TextMeshPro;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.currentMissionstatus == GameManager.Missionstatus.Lost){
            m_TextMeshPro.text = "Mission Failed";
        }
    }
}
