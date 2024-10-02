using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class StageData
{
    public int stageIndex;
    public bool isCleared;


    // 게임을 재시작하거나 타이틀 화면을 나갔다가 돌아와도 플레이어의 스테이지 클리어 상태를 유지
    public void Save()
    {
        PlayerPrefs.SetInt("StageCleared" + stageIndex, isCleared ? 1 : 0);
    }

    public void Load()
    {
        isCleared = PlayerPrefs.GetInt("StageCleared" + stageIndex, 0) == 1;
    }
}

public class StageManager : MonoBehaviour
{
    public List<StageData> stages;
    public List<Button> buttons;

    private void Start()
    {
        stages = new List<StageData>
        {
            new StageData { stageIndex = 0, isCleared = false },
            new StageData { stageIndex = 1, isCleared = false },
            new StageData { stageIndex = 2, isCleared = false },
            new StageData { stageIndex = 3, isCleared = false },
        };
        foreach (var stage in stages)
        {
            stage.Load();
        }
        FindStageButtons();

        UpdateButtons();
    }

    private void FindStageButtons()
    {
        GameObject[] stageObjects = GameObject.FindGameObjectsWithTag("Stage");
        buttons = new List<Button>();

        foreach (GameObject obj in stageObjects)
        {
            Button button = obj.GetComponent<Button>();
            if (button != null)
            {
                buttons.Add(button);
            }
        }
    }

    public void ClearStage(int clearStage)
    {
        stages[clearStage].isCleared = true;
        stages[clearStage].Save();
        UpdateButtons();

    }

    private void UpdateButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (i == 0)
            {
                buttons[i].gameObject.SetActive(true);
            }
            else if (i < stages.Count && stages[i-1].isCleared)
            {
                buttons[i].gameObject.SetActive(true);
            }
            else
            {
                buttons[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnApplicationQuit()
    {
        foreach (var stage in stages)
        {
            stage.isCleared = false; 
            stage.Save(); 
        }
    }
}
