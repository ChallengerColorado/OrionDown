using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TransitionButtonBehavior : MonoBehaviour
{

    public void GoToMenu()
    {
        GameManager.Instance.StopGame(false, false);
    }

    public void Pause()
    {
        GameManager.Instance.PauseGame();
    }

    public void Unpause()
    {
        GameManager.Instance.UnpauseGame();
    }

    public void GoToLose()
    {
        GameManager.Instance.StopGame(true, false);
    }
public void GoToWin()
    {
        GameManager.Instance.StopGame(true, true);
    }
}
