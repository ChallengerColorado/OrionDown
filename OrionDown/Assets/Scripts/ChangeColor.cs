using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    [SerializeField]
    public bool startingStatus;

    [SerializeField] public Color onColor;
    [SerializeField] public Color offColor;

    private bool status;

    // Start is called before the first frame update
    void Start()
    {
        updateColor();
    }

    private void updateColor()
    {
        if (status)
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", onColor);
        else
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", offColor);
    }

    public void toggleColor()
    {
        status = !status;
        updateColor();
    }
}
