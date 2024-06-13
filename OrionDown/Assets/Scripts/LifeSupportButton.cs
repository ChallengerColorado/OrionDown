using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeSupportButton : MonoBehaviour
{
    [SerializeField] private GameObject m_passwords;
    [SerializeField] int buttonPressed;
    private LifeSupportModule m_passwordScript;
    // Start is called before the first frame update
    void Start()
    {
        m_passwordScript =  m_passwords.GetComponent<LifeSupportModule>();
    }
    public void OnButtonPress() {
        m_passwordScript.Cycle(buttonPressed);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
