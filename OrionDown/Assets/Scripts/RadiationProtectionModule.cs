using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;
using TMPro;
using System.Collections.Specialized;

public class RadiationProtectionModule : ModuleBehaviour
{
    [SerializeField] public Image mapBackground; // maze background image object
    [SerializeField] public Image blink; // blinking tile object
    [SerializeField] public TMP_Text invalidText; // error text object
    [SerializeField] public TMP_Text resetText; // maze reset text object

    private List<string> statuses = new List<string>(){"AE","%4","8Þ","Ð!","Q§","<Y"}; // possible statuses corresponding to non-finished states
    private int mazeIndex = 0; // identifies which maze layout is being used
    private const float blinkDuration = .5f; // how long one phase of the blink cycle lasts
    private (int, int) blinkStartTile = (5, 5); // maze starting position
    // current position in maze coordinates of blinking tile
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
            // move tile object to reflect new value
            blink.rectTransform.anchoredPosition = new Vector2(TilePosition(_blinkTile.Item1), TilePosition(_blinkTile.Item2));
        }
    }

    private const float ImageMapSize = 400; // size of maze image
    private const float ImageTileSize = 37.647f; // size of each tile
    private const int TileNumber = 8; // number of tiles per row/column
    private const float ImageSpaceSize = (ImageMapSize - TileNumber * ImageTileSize) / (float) (TileNumber + 1); // calculate how much padding must be between the tiles


    private const float invalidTextBlinkDuration = .5f; // how long one phase of the error text blink cycle lasts
    private const int invalidTextBlinkNumber = 3; // how many times the error text blinks
    private const float resetTextDuration = 1.5f; // how long the maze reset text is visible for

    private System.Random random = new System.Random();

    public enum Move{
        Left,
        Up,
        Down,
        Right
    }

    // Stores all possible target paths. Each difficulty has a list of paths to choose from. Each path is stored as a sequence of moves, a starting position, and the index of
    // the maze layout the path exists in
    private Dictionary<GameManager.Difficulty, List<(List<Move>, (int, int), int)>> presetPaths = new Dictionary<GameManager.Difficulty, List<(List<Move>, (int, int), int)>> ()
    {
        { GameManager.Difficulty.Easy, new List<(List<Move>, (int, int), int)> () // all paths for the easy mode
            {
                ( new List<Move>() { Move.Up, Move.Right, Move.Down, Move.Right, Move.Down, Move.Right}, (0, 5), 0), // each possible path as a (Moves, startPosition, mazeIndex) tuple
                (new List<Move>() { Move.Down, Move.Right, Move.Down, Move.Left, Move.Down, Move.Left}, (6, 7), 1)
            }
        },
        { GameManager.Difficulty.Medium, new List<(List<Move>, (int, int), int)> ()
            {
                ( new List<Move>() { Move.Up, Move.Right, Move.Up, Move.Right, Move.Right, Move.Down, Move.Down, Move.Right, Move.Right }, (0, 0), 2),
                ( new List<Move>() { Move.Right, Move.Up, Move.Left, Move.Left, Move.Down, Move.Left, Move.Up, Move.Up, Move.Left, Move.Up }, (5, 3), 3)
            }
        },
        { GameManager.Difficulty.Difficult, new List<(List<Move>, (int, int), int)> ()
            {
                ( new List<Move>() { Move.Down, Move.Right, Move.Down, Move.Right, Move.Up, Move.Up, Move.Up, Move.Right, Move.Up, Move.Right, Move.Up, Move.Left, Move.Up, Move.Right, Move.Right }, (2, 3), 4),
                ( new List<Move>() { Move.Up, Move.Left, Move.Left, Move.Left, Move.Down, Move.Left, Move.Down, Move.Right, Move.Down, Move.Down, Move.Right, Move.Down, Move.Left, Move.Left, Move.Up }, (7, 6), 5)
            }
        }
    };

    private List<Move> mazepath = new List<Move>(); // path entered by user
    private List<Move> mazeSolution; // target path

    // maps each direction to the opposite direction
    private Dictionary<Move, Move> opposites = new Dictionary<Move, Move>(){
                                {Move.Left, Move.Right},
                                {Move.Up, Move.Down},
                                {Move.Down, Move.Up},
                                {Move.Right, Move.Left} };
    
    // Start is called before the first frame update
    void Start()
    {
        // determine the set of paths to choose from based on game difficulty
        List<(List<Move>, (int, int), int)> possiblePaths = presetPaths[GameManager.Instance.currentDifficulty];

        // select a path at random and extract its data
        (List<Move>, (int, int), int) chosenPath = possiblePaths[random.Next(possiblePaths.Count)];
        mazeSolution = chosenPath.Item1;
        blinkStartTile = chosenPath.Item2;
        mazeIndex = chosenPath.Item3;

        BlinkTile = blinkStartTile; // initialize blinking tile position to starting position associated with path
        SetStatus(false, statuses[mazeIndex]); // set the status based on which maze layout is being used
    }

    // update path to include lastmove
    public void MazePositioningSystem(Move lastmove){
        if (mazepath.Count() == 0){
            mazepath.Add(lastmove);
        }
        else if (mazepath.Last() == opposites[lastmove]){
            // if lastmove is in the opposite direction of the previous move, undo that move instead of adding lastmove
            mazepath.RemoveAt(mazepath.Count - 1);
        }
        else{
            mazepath.Add(lastmove);
        };

    }

    // arrow button behavior
    public void OnButtonPress(int direction)
    {
        MazePositioningSystem((Move) direction);
    }

    // submit button behavior
    public void MazeEnd(){
        StartCoroutine(VerifyPath());
    }

    // determine the actual in-game position relative to the maze image object corresponding to a given tile 
    private float TilePosition(int tileCoord)
    {
        float imageTilePosition = ImageSpaceSize + tileCoord*(ImageTileSize + ImageSpaceSize);
        return mapBackground.rectTransform.rect.width * imageTilePosition / ImageMapSize;
    }

    // determines in real time whether or not the entered path is valid
    private IEnumerator VerifyPath()
    {
        // disallow path verification if module is already solved
        if (GetStatus())
            yield break;

        yield return new WaitForSeconds(blinkDuration);

        // analyze entered path and target path in parallel
        foreach (var movePair in ZipPaths(mazepath, mazeSolution))
        {
            // invisible phase of blink cycle
            blink.gameObject.SetActive(false);
            yield return new WaitForSeconds(blinkDuration);

            // if the next move of the entered path does not match the next move of the target path, reset maze and display error messages
            if (movePair.Item1 != movePair.Item2)
            {
                mazepath = new List<Move>();
                BlinkTile = blinkStartTile;
                StartCoroutine(DisplayInvalidMessage());
                yield break;
            }

            // otherwise, apply the next move to the tile's position and execute the visible phase of the blink cycle
            MoveBlink(movePair.Item1.Value);
            blink.gameObject.SetActive(true);
            yield return new WaitForSeconds(blinkDuration);
        }
        
        // if paths were identical, module is solved
        SetStatus(true, "BB");
    }

    // display error messages marking an incorrect path
    private IEnumerator DisplayInvalidMessage()
    {
        // execute error text blink cycle invalidTextBlinkNumber times
        for (int i = 0; i < invalidTextBlinkNumber; i++)
        {
            invalidText.gameObject.SetActive(true);
            yield return new WaitForSeconds(invalidTextBlinkDuration);


            invalidText.gameObject.SetActive(false);
            yield return new WaitForSeconds(invalidTextBlinkDuration);
        }

        // display reset text for resetTextDuration
        resetText.gameObject.SetActive(true);
        yield return new WaitForSeconds(resetTextDuration);
        resetText.gameObject.SetActive(false);
        blink.gameObject.SetActive(true); // make starting tile visible again
    }

    // update the blinking tile's position in accordance with the given direction
    private void MoveBlink(Move m)
    {
        switch (m)
        {
            case Move.Left:
                BlinkTile = (BlinkTile.Item1 - 1, BlinkTile.Item2);
                return;
            case Move.Up:
                BlinkTile = (BlinkTile.Item1, BlinkTile.Item2 + 1);
                return;
            case Move.Down:
                BlinkTile = (BlinkTile.Item1, BlinkTile.Item2 - 1);
                return;
            case Move.Right:
                BlinkTile = (BlinkTile.Item1 + 1, BlinkTile.Item2);
                return;
            default:
                return;
        }
    }

    // take two paths and return a sequence of tuples, each containing the corresponding move from each
    // path, or null if there are no moves remaining in one of the paths
    private IEnumerable<Tuple<Move?, Move?>> ZipPaths(IEnumerable<Move> first, IEnumerable<Move> second)
    {
        IEnumerator firstEnumerator = first.GetEnumerator();
        IEnumerator secondEnumerator = second.GetEnumerator();

        List<Tuple<Move?,Move?>> list = new List<Tuple<Move?, Move?>>();

        bool firstNext = firstEnumerator.MoveNext();
        bool secondNext = secondEnumerator.MoveNext();

        while (firstNext || secondNext)
        {
            list.Add(Tuple.Create<Move?, Move?>((firstNext ? (Move) firstEnumerator.Current : null), (secondNext ? (Move) secondEnumerator.Current : null)));

            firstNext = firstEnumerator.MoveNext();
            secondNext = secondEnumerator.MoveNext();
        }

        return (IEnumerable<Tuple<Move?, Move?>>) list;
    }
}
