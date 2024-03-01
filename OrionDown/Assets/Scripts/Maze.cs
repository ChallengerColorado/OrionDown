using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Maze : MonoBehaviour
{
    enum move{
        Left,
        Up,
        Down,
        Right
    }
    List<move> mazepath = new List<move>();
    List<move> mazeSolution = new List<move>();
    Dictionary<move, move> oposites = new Dictionary<move, move>(){
                                {move.Left, move.Right},
                                {move.Up, move.Down},
                                {move.Down, move.Up},
                                {move.Right, move.Left} }; 
    // Start is called before the first frame update
    void Start()
    {

    }
    void MazePositioningSystem(move lastmove){
        if (mazepath.Last() == oposites[lastmove]){
            mazepath.RemoveAt(mazepath.Count - 1);
        }
        else{
            mazepath.Add(lastmove);
        };

    }

    void mazeEnd(){
        if (mazepath == mazeSolution){
            //mazeIsFixed = true;
            Debug.Log("Hooray");
        }
        else{
            resetmaze();
        }
    }

    void resetmaze(){
        mazepath.Clear();

    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(mazepath);
    }
}
