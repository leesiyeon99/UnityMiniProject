using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public enum GameState { Ready, Running, GameOver, GameClear }

    public static GameManager Instance { get; private set; }

    [SerializeField] GameState curState;
    [SerializeField] TextMeshProUGUI redGemScore;
    [SerializeField] TextMeshProUGUI blueGemScore;

    int redGemCount = 0;
    int blueGemCount = 0;

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

    private void Start()
    {
        curState = GameState.Ready;
    }


    private void Update()
    {
        if (curState == GameState.Ready && Input.anyKeyDown)
        {
            GameStart();
        }
        else if (curState == GameState.GameOver)
        {
            SceneManager.LoadScene("GameScene");
            curState = GameState.Running;
        }
        else if (curState == GameState.GameClear)
        {
            SceneController.Instance.LoadStageScene();
        }
        redGemScore.text = $"{redGemCount}";
        blueGemScore.text = $"{blueGemCount}";

    }

    public void GameStart()
    {
        curState = GameState.Running;
    }

    public void GameOver()
    {
        curState = GameState.GameOver;
    }

    public void GameClear()
    {
        curState = GameState.GameOver;
    }

    public int RedGemScore()
    {
        redGemCount++;
        return redGemCount;
    }

    public int BlueGemScore()
    {
        blueGemCount++;
        return blueGemCount;
    }
}
