using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static SceneController sceneController { get; private set; }
    public static AudioManager audioManager { get; private set; }

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

            sceneController = new SceneController();
            audioManager = new AudioManager();
        }

    }

    public void LoadStageScene()
    {
        sceneController.LoadScene("StageScene");
    }

    public void LoadTitleScene()
    {
        sceneController.LoadScene("TitleScene");
    }
}
