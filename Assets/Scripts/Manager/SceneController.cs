using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitScene()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void LoadStageScene()
    {
        LoadScene("StageScene");
    }

    public void LoadTitleScene()
    {
        LoadScene("TitleScene");
    }

    public void LoadGameScene()
    {
        LoadScene("GameScene");
    }
}
