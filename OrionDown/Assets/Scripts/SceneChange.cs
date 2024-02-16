using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void ChangeToScene(int sceneID) {
        GameManager.Instance.BeginGame(GameManager.Difficulty.Easy);
        SceneManager.LoadScene(sceneID);
    }
}
