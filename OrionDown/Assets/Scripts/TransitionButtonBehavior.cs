using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TransitionButtonBehavior : MonoBehaviour
{

    public void GoToMenu()
    {
        GameManager.Instance.StopGame(false);
    }

    public void Pause()
    {
        GameManager.Instance.PauseGame();
    }

    public void Unpause()
    {
        GameManager.Instance.UnpauseGame();
    }

    public void GoToEnd()
    {
        GameManager.Instance.StopGame(true);
    }

}
