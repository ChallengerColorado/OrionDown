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
    List<int> solution = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update(){
        

        Debug.Log(pos1[c1].ToString());
        Debug.Log(c1.ToString());
    }
    public void Cycle(int buttonPressed)
    {
       if (buttonPressed == 0){
            if (c1 < pos1.Count - 1) {
                c1 += 1;
            }
            else {
                c1 = 0;
            }
        }
        else if (buttonPressed == 1){
            if (c2 < pos2.Count - 1) {
                c2 += 1;
            }
            else {
                c2 = 0;
            }
        }
        else if (buttonPressed == 2){
            if (c3 < pos3.Count - 1) {
                c3 += 1;
            }
            else {
                c3 = 0;
                }
        }
        else if (buttonPressed == 3){
            if (c4 < pos4.Count - 1) {
                c4 += 1;
            }
            else {
                c4 = 0;
            }
        }
        else if (buttonPressed == 4){
            if (c5 < pos5.Count - 1) {
                c5 += 1;
            }
            else {
                c5 = 0;
            }
        }
        char1Text.text = pos1[c1].ToString();
        char2Text.text = pos2[c2].ToString();
        char3Text.text = pos3[c3].ToString();
        char4Text.text = pos4[c4].ToString();
        char5Text.text = pos5[c5].ToString();

    }
    public void ReverseCycle(int buttonPressed)
    {
        if (buttonPressed == 0){
            if (c1 > 0) {
                c1 -= 1;
            }
            else {
                c1 = pos1.Count - 1;
            }
        }
        else if (buttonPressed == 1){
            if (c2 > 0) {
                c2 -= 1;
            }
            else {
                c2 = pos2.Count - 1;
            }
        }
        else if (buttonPressed == 2){
            if (c3 > 0) {
                c3 -= 1;
            }
            else {
                c3 = pos3.Count - 1;
                }
        }
        else if (buttonPressed == 3){
            if (c4 > 0) {
                c4 -= 1;
            }
            else {
                c4 = pos4.Count - 1;
            }
        }
        else if (buttonPressed == 4){
            if (c5 > 0) {
                c5 -= 1;
            }
            else {
                c5 = pos5.Count - 1;
            }
        }
        char1Text.text = pos1[c1].ToString();
        char2Text.text = pos2[c2].ToString();
        char3Text.text = pos3[c3].ToString();
        char4Text.text = pos4[c4].ToString();
        char5Text.text = pos5[c5].ToString();

    }
}
