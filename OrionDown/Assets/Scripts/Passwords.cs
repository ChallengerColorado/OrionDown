using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Passwords : MonoBehaviour
{
    [SerializeField]
    public TMP_Text[] charDisplays;

    private char[][] characters = {
        new char[] { 'A', 'B', 'C' },
        new char[] { 'A', 'B', 'C' },
        new char[] { 'A', 'B', 'C' },
        new char[] { 'A', 'B', 'C' },
        new char[] { 'A', 'B', 'C' }
    };

    private int[] currentIndices = new int[5];

    private int[] solutionIndices = new int[5];

    private void Start()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            charDisplays[i].text = characters[i][currentIndices[i]].ToString();
        }
    }

    // Update is called once per frame
    void Update(){
       
        Debug.Log(characters[0][currentIndices[0]].ToString());
        Debug.Log(currentIndices[0].ToString());
    }
    public void Cycle(int buttonPressed)
    {
        currentIndices[buttonPressed] = (currentIndices[buttonPressed] + 1) % characters[buttonPressed].Length;

        charDisplays[buttonPressed].text = characters[buttonPressed][currentIndices[buttonPressed]].ToString();
    }

    public void ReverseCycle(int buttonPressed)
    {
        if (currentIndices[buttonPressed] == 0)
            currentIndices[buttonPressed] = characters[buttonPressed].Length - 1;
        else
            currentIndices[buttonPressed] = (currentIndices[buttonPressed] - 1);

        charDisplays[buttonPressed].text = characters[buttonPressed][currentIndices[buttonPressed]].ToString();
    }
}
