using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GameState { Ready, Running, GameOver, GameClear }

    public static GameManager Instance { get; private set; }

    [SerializeField] GameState curState;

    private bool player1Goal = false;
    private bool player2Goal = false;

    int redGemCount = 0;
    int blueGemCount = 0;

    public UnityEvent OnGameClear;

    [SerializeField] int currentStageIndex = 0;

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
        if (curState == GameState.Ready)
        {
            GameReady();
        }
        if (curState == GameState.Running)
        {
            GameStart();
        }
        else if (curState == GameState.GameOver)
        {
            PlayerDied();
            curState = GameState.Running;
        }
        else if (curState == GameState.GameClear)
        {
            StartCoroutine(GameClearRoutine());
            curState = GameState.Ready;
        }

    }

    IEnumerator GameClearRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        SceneController.Instance.LoadStageScene();
        AudioManager.Instance.PlayBGM2();

    }

    public void GameReady()
    {
        curState = GameState.Ready;
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
        OnGameClear.Invoke();
        ReStart();
    }

    public void ReStart()
    {
        player1Goal = false;
        player2Goal = false;
        redGemCount = 0;
        blueGemCount = 0;
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
        // 플레이어의 각각에 맞는 색 위의 문과 충돌 중 && 스테이지에 있는 모든 보석 다 먹기
        Debug.Log($"Player1Goal: {player1Goal}, Player2Goal: {player2Goal}, RedGems: {redGemCount}, BlueGems: {blueGemCount}");

        if (player1Goal && player2Goal)
        {
            if (redGemCount >= 3 && blueGemCount >= 3)
            {
                GameClear();
                StartCoroutine(stageupdata());
            }
        }
    }

    IEnumerator stageupdata()
    {
        yield return new WaitForSeconds(2);

        StageManager stageManager = FindObjectOfType<StageManager>();

        if (stageManager != null)
        {
            stageManager.ClearStage(currentStageIndex-1);
            Debug.Log("StageClear");
        }
    }

    public void PlayerDied()
    {
        ReStart();
        SceneController.Instance.LoadGameScene(currentStageIndex);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 플레이어가 죽었을 때 있던 씬에서 재로딩되도록 씬의 인덱스 저장해서 사용
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene1")
        {
            currentStageIndex = 1;
        }
        else if (scene.name == "GameScene2")
        {
            currentStageIndex = 2;
        }
    }
}
