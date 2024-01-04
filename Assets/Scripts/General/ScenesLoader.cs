using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesLoader : MonoBehaviour
{
    public static ScenesLoader Instance { get; private set; }

    private const string START_SCENE_NAME = "Start Scene";
    private const string GAME_SCENE_NAME = "Game Scene";

    private void Awake() 
    { 
        // Singleton setup
        if (Instance != null && Instance != this) { Destroy(this); } else { Instance = this; } 
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(START_SCENE_NAME, LoadSceneMode.Single);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(GAME_SCENE_NAME, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}