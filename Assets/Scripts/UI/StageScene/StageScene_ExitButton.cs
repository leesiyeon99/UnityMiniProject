using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageScene_ExitButton : MonoBehaviour
{
    private Button exitButton;

    void Start()
    {
        exitButton = GetComponent<Button>();
        exitButton.onClick.AddListener(SceneController.Instance.LoadTitleScene);
    }
}
