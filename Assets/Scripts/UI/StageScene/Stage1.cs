using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1 : MonoBehaviour
{
    private Button stage1Button;
    [SerializeField] private AudioClip clickSound;
    private AudioSource audioSource;

    void Start()
    {
        stage1Button = GetComponent<Button>();
        audioSource = gameObject.AddComponent<AudioSource>();
        stage1Button.onClick.AddListener(OnStageButtonClick);
    }

    private void OnStageButtonClick()
    {
        PlayClickSound(); 
        SceneController.Instance.LoadGameScene();
        AudioManager.Instance.PlayBGM();
    }

    private void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound); 
    }
}
