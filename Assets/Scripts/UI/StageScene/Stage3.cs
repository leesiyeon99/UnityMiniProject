using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage3 : MonoBehaviour
{
    private Button stage3Button;
    [SerializeField] private AudioClip clickSound;
    private AudioSource audioSource;

    void Start()
    {
        stage3Button = GetComponent<Button>();
        audioSource = gameObject.AddComponent<AudioSource>();
        stage3Button.onClick.AddListener(OnStageButtonClick);
    }

    private void OnStageButtonClick()
    {
        PlayClickSound(); 
        SceneController.Instance.LoadGameScene(3);
        AudioManager.Instance.PlayBGM();
    }

    private void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound); 
    }
}
