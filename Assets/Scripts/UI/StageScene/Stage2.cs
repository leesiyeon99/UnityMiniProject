using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage2 : MonoBehaviour
{
    private Button stage2Button;
    [SerializeField] private AudioClip clickSound;
    private AudioSource audioSource;

    void Start()
    {
        stage2Button = GetComponent<Button>();
        audioSource = gameObject.AddComponent<AudioSource>();
        stage2Button.onClick.AddListener(OnStageButtonClick);
    }

    private void OnStageButtonClick()
    {
        PlayClickSound(); 
        SceneController.Instance.LoadGameScene(2);
        AudioManager.Instance.PlayBGM();
    }

    private void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound); 
    }
}
