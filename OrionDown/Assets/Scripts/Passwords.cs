using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passwords : MonoBehaviour
{
    List<char> pos1 = new List<char>{'a','b','c'};

    List<char> pos2 = new List<char>{'a','b','c'};

    List<char> pos3 = new List<char>{'a','b','c'};

    List<char> pos4 = new List<char>{'a','b','c'};

    List<char> pos5 = new List<char>{'a','b','c'};

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
    void Update()
    {
    
    }
    void Cycle(int buttonPressed)
    {
        guess[buttonPressed] += 1;
    }
}
