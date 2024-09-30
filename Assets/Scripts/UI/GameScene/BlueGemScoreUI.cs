using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlueGemScoreUI : MonoBehaviour
{
    private TextMeshProUGUI redGemScore;
    public int blueGemCount = 0;

    private void Start()
    {
        redGemScore = GetComponent<TextMeshProUGUI>();
    }

    public void Score()
    {
        blueGemCount++;
        redGemScore.text = $"{blueGemCount}";
        GameManager.Instance.BlueGemScore();
    }
}
