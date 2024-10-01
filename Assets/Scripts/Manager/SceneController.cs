using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitScene()
    {
        StartCoroutine(LoadQuitSceneRoutine());
    }

    IEnumerator LoadQuitSceneRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void LoadStageScene()
    {
        StartCoroutine(LoadStageSceneRoutine());
    }

    IEnumerator LoadStageSceneRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        LoadScene("StageScene");
        GameManager.Instance.GameReady();
        AudioManager.Instance.StopBGM(); 
    }

    public void LoadTitleScene()
    {
        StartCoroutine (LoadTitleSceneRoutine());
    }


    IEnumerator LoadTitleSceneRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        LoadScene("TitleScene");
        GameManager.Instance.GameReady();
        AudioManager.Instance.StopBGM();
    }

    public void LoadGameScene(int stageIndex)
    {
        StartCoroutine(LoadGameSceneRoutine(stageIndex));
    }

    IEnumerator LoadGameSceneRoutine(int stageIndex)
    {
        yield return new WaitForSeconds(0.1f);
        string sceneName = "GameScene" + stageIndex;
        SceneManager.LoadScene(sceneName);
        GameManager.Instance.GameStart();
        AudioManager.Instance.PlayBGM();
        AudioManager.Instance.StopBGM2();
    }
}
