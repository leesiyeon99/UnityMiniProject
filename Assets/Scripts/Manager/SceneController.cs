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

    public void LoadGameScene()
    {
        StartCoroutine(LoadGameSceneRoutine());
    }

    IEnumerator LoadGameSceneRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        LoadScene("GameScene");
        GameManager.Instance.GameStart();
        AudioManager.Instance.PlayBGM();
    }
}
