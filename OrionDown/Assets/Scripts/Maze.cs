using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;
using TMPro;

public class Maze : ModuleBehaviour
{
    [SerializeField] public Image mapBackground;
    [SerializeField] public Image blink;
    [SerializeField] public TMP_Text invalidText;
    [SerializeField] public TMP_Text resetText;

    private const float blinkDuration = .5f;
    private (int, int) blinkStartTile = (5, 5);
    private (int, int) _blinkTile;
    private (int, int) BlinkTile
    {
        get
        {
            return _blinkTile;
        }

        set
        {
            _blinkTile = value;
            blink.rectTransform.anchoredPosition = new Vector2(TilePosition(_blinkTile.Item1), TilePosition(_blinkTile.Item2));
        }
    }

    private const int ImageMapSize = 850;
    private const int ImageTileSize = 80;
    private const int TileNumber = 8;
    private const float ImageSpaceSize = (ImageMapSize - TileNumber * ImageTileSize) / (float) (TileNumber + 1);


    private float invalidTextBlinkDuration = .5f;
    private int invalidTextBlinkNumber = 3;
    private float resetTextDuration = 1.5f;


    public enum move{
        Left,
        Up,
        Down,
        Right
    }
    List<move> mazepath = new List<move>();
    List<move> mazeSolution = new List<move>() {move.Left, move.Up, move.Left, move.Down, move.Right};
    Dictionary<move, move> oposites = new Dictionary<move, move>(){
                                {move.Left, move.Right},
                                {move.Up, move.Down},
                                {move.Down, move.Up},
                                {move.Right, move.Left} };
    
    // Start is called before the first frame update
    void Start()
    {
        BlinkTile = blinkStartTile;
    }
    public void MazePositioningSystem(move lastmove){
        if (mazepath.Count() == 0){
            mazepath.Add(lastmove);
        }
        else if (mazepath.Last() == oposites[lastmove]){
            mazepath.RemoveAt(mazepath.Count - 1);
        }
        else{
            mazepath.Add(lastmove);
        };

    }

    public void MazeEnd(){
        StartCoroutine(VerifyPath());

        /*if (mazepath.SequenceEqual(mazeSolution)){
            //mazeIsFixed = true;
            Debug.Log("Hooray");
            Status = true;
        }
        else{
            mazepath.Clear();
        }*/
    }
    // Update is called once per frame
    void Update()
    {
        string moves = "";
        foreach(move i in mazepath){
            moves += i.ToString();
        }
    }

    private float TilePosition(int tileCoord)
    {
        float imageTilePosition = ImageSpaceSize + tileCoord*(ImageTileSize + ImageSpaceSize);
        return mapBackground.rectTransform.rect.width * imageTilePosition / ImageMapSize;
    }

    private IEnumerator VerifyPath()
    {
        if (Status)
            yield break;

        blink.gameObject.SetActive(true);
        yield return new WaitForSeconds(blinkDuration);

        foreach (var movePair in mazepath.Zip(mazeSolution, Tuple.Create))
        {
            blink.gameObject.SetActive(false);
            yield return new WaitForSeconds(blinkDuration);

            if (movePair.Item1 != movePair.Item2)
            {
                mazepath = new List<move>();
                BlinkTile = blinkStartTile;
                StartCoroutine(DisplayInvalidMessage());
                yield break;
            }

            MoveBlink(movePair.Item1);
            blink.gameObject.SetActive(true);
            yield return new WaitForSeconds(blinkDuration);
        }

        Status = true;
    }

    private IEnumerator DisplayInvalidMessage()
    {
        for (int i = 0; i < invalidTextBlinkNumber; i++)
        {
            invalidText.gameObject.SetActive(true);
            yield return new WaitForSeconds(invalidTextBlinkDuration);


            invalidText.gameObject.SetActive(false);
            yield return new WaitForSeconds(invalidTextBlinkDuration);
        }

        resetText.gameObject.SetActive(true);
        yield return new WaitForSeconds(resetTextDuration);
        resetText.gameObject.SetActive(false);
    }

    private void MoveBlink(move m)
    {
        switch (m)
        {
            case move.Left:
                BlinkTile = (BlinkTile.Item1 - 1, BlinkTile.Item2);
                return;
            case move.Up:
                BlinkTile = (BlinkTile.Item1, BlinkTile.Item2 + 1);
                return;
            case move.Down:
                BlinkTile = (BlinkTile.Item1, BlinkTile.Item2 - 1);
                return;
            case move.Right:
                BlinkTile = (BlinkTile.Item1 + 1, BlinkTile.Item2);
                return;
            default:
                return;
        }
    }

}
