using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Passwords : MonoBehaviour
{
    public TMP_Text char1Text;
    public TMP_Text char2Text;
    public TMP_Text char3Text;
    public TMP_Text char4Text;
    public TMP_Text char5Text;
    List<char> pos1 = new List<char>{'A','B','C'};

    List<char> pos2 = new List<char>{'A','B','C'};

    List<char> pos3 = new List<char>{'A','B','C'};

    List<char> pos4 = new List<char>{'A','B','C'};

    List<char> pos5 = new List<char>{'A','B','C'};

    int c1 = 0;
    int c2 = 0;
    int c3 = 0;
    int c4 = 0;
    int c5 = 0;
    List<int> guess = new List<int>();
    List<int> solution = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
    guess.Add(c1);
    guess.Add(c2);
    guess.Add(c3);
    guess.Add(c4);
    guess.Add(c5);
    }
    // Update is called once per frame
    void Update(){
        /*char2Text.text = pos2[c2].ToString();
        char3Text.text = pos3[c3].ToString();
        char4Text.text = pos4[c4].ToString();
        char5Text.text = pos5[c5].ToString();*/
        Debug.Log(pos1[c1].ToString());
        Debug.Log(c1.ToString());
    }
    public void Cycle(int buttonPressed)
    {
        guess[buttonPressed] += 1;
        char1Text.text = pos1[c1].ToString();
        char2Text.text = pos2[c2].ToString();
        char3Text.text = pos3[c3].ToString();
        char4Text.text = pos4[c4].ToString();
        char5Text.text = pos5[c5].ToString();

    }
    public void ReverseCycle(int buttonPressed)
    {
        int newguess = guess[buttonPressed];
        newguess += 1;
        guess.RemoveAt(buttonPressed);
        guess.Insert(buttonPressed, newguess);
        char1Text.text = pos1[c1].ToString();
        char2Text.text = pos2[c2].ToString();
        char3Text.text = pos3[c3].ToString();
        char4Text.text = pos4[c4].ToString();
        char5Text.text = pos5[c5].ToString();

    }
}
