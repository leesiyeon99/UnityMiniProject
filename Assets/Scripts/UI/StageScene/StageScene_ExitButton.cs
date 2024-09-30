using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageScene_ExitButton : MonoBehaviour
{
    private Button exitButton;
    [SerializeField] private AudioClip clickSound;
    private AudioSource audioSource;

    void Start()
    {
        exitButton = GetComponent<Button>();
        audioSource = gameObject.AddComponent<AudioSource>();
        exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnExitButtonClick()
    {
        PlayClickSound(); 
        SceneController.Instance.LoadTitleScene();
    }

    private void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
}
