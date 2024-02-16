using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void ChangeToScene(int sceneID) {
        // Temporary! Replace with solution that uses GameManager directly
        if (sceneID == 1)
            GameManager.Instance.StartGame(GameManager.Difficulty.Easy);
        else
            GameManager.Instance.StopGame();

        SceneManager.LoadScene(sceneID);
    }
}
