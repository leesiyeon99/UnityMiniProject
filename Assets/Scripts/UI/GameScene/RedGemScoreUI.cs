using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RedGemScoreUI : MonoBehaviour
{
    private TextMeshProUGUI redGemScore;
    public int redGemCount = 0;

    private void Start()
    {
        redGemScore = GetComponent<TextMeshProUGUI>();
    }

    public void Score()
    {
        redGemCount++;
        redGemScore.text = $"{redGemCount}";
    }
}
