using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public List<string> _sceneNames;

    public void LoadScene(string sceneName)
    {
        //Load this scene
        SceneManager.LoadScene(sceneName);
    }
}
