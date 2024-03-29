using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStatus : MonoBehaviour
{
    private bool levelCompleted = false;
    private bool levelFailed = false;
    private bool gameOver = false;

    public string levelFailedScene;
    public string levelCompletedScene;
    public string gameOverScene;

    void Update()
    {
        if (levelCompleted)
        {
            SceneManager.LoadScene(levelCompletedScene);
        }
        
        if (levelFailed)
        {
            SceneManager.LoadScene(levelFailedScene);
        }
        
        if (gameOver)
        {
            SceneManager.LoadScene(gameOverScene);
        }
    }

    public void SetLevelComplete(bool complete)
    {
        levelCompleted = complete;
    }
    
    public void SetLevelFailed(bool failed)
    {
        levelFailed = failed;
    }
    
    public void SetGameOver(bool isGameOver)
    {
        gameOver = isGameOver;
    }
}
