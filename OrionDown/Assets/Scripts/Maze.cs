using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Maze : ModuleBehaviour
{
    public enum move{
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
    public void MazePositioningSystem(move lastmove){
        Debug.Log('1');
        if (mazepath.Count() == 0){
            mazepath.Add(lastmove);
        }
        else if (mazepath.Last() == oposites[lastmove]){
            mazepath.RemoveAt(mazepath.Count - 1);
            Debug.Log('2');
        }
        else{
            mazepath.Add(lastmove);
        };

    }

    public void MazeEnd(){
        Debug.Log("5");
        if (mazepath.SequenceEqual(mazeSolution)){
            //mazeIsFixed = true;
            Debug.Log("Hooray");
            Status = true;
        }
        else{
            mazepath.Clear();
        }
    }
    // Update is called once per frame
    void Update()
    {
        string moves = "";
        foreach(move i in mazepath){
            moves += i.ToString();
        }
        Debug.Log(moves);
    }
}
