using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GameState { Ready, Running, GameOver, GameClear }

    public static GameManager Instance { get; private set; }

    [SerializeField] GameState curState;
    [SerializeField] TextMeshProUGUI redGemScore;
    [SerializeField] TextMeshProUGUI blueGemScore;
    [SerializeField] Image clearImage;

    private bool player1Goal = false;
    private bool player2Goal = false;

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
        clearImage.gameObject.SetActive(false);
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
            clearImage.gameObject.SetActive(true);
            StartCoroutine(GameClearRoutine());
            curState = GameState.Ready;
        }

        Score();
    }

    IEnumerator GameClearRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        SceneController.Instance.LoadStageScene();

    }
    private void Score()
    { 
        if (redGemScore == null || blueGemScore == null ) return;
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
        curState = GameState.GameClear;
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

    public void SetPlayer1Goal(bool reached)
    {
        player1Goal = reached;
        CheckGameClear(); 
    }

    public void SetPlayer2Goal(bool reached)
    {
        player2Goal = reached;
        CheckGameClear(); 
    }

    private void CheckGameClear()
    {
        if (player1Goal && player2Goal)
        {
            GameClear();
        }
    }
}
