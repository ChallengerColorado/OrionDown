using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordsButton : MonoBehaviour
{
    [SerializeField] private GameObject m_passwords;
    [SerializeField] int buttonPressed;
    private Passwords m_passwordScript;
    // Start is called before the first frame update
    void Start()
    {
        m_passwordScript =  m_passwords.GetComponent<Passwords>();
    }
    public void OnButtonPress() {
        m_passwordScript.Cycle(buttonPressed);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
