using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private SceneController sceneController;

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

        sceneController = new SceneController();
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
