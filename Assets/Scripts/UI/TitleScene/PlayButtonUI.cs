using UnityEngine;
using UnityEngine.UI;

public class PlayButtonUI : MonoBehaviour
{
    private Button playButton;
    [SerializeField] private AudioClip clickSound;
    private AudioSource audioSource;

    void Start()
    {
        playButton = GetComponent<Button>();
        audioSource = gameObject.AddComponent<AudioSource>();
        playButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnExitButtonClick()
    {
        PlayClickSound();
        SceneController.Instance.LoadStageScene();
    }

    private void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
}
