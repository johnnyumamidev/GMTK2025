using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    [SerializeField] string startingLevelName;
    [SerializeField] string currentSceneName;
    
    void Start() {
        if(startingLevelName != "")
            SceneManager.LoadSceneAsync(startingLevelName, LoadSceneMode.Additive);
        currentSceneName = startingLevelName;
    }

    public void LoadScene(string sceneName) {
        SceneManager.UnloadSceneAsync(currentSceneName);
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        currentSceneName = sceneName;
    }
}
