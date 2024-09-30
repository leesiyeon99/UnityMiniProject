using UnityEngine;
using UnityEngine.UI;

public class ClearImageUI : MonoBehaviour
{
    private Image clearImage;
    [SerializeField] private AudioClip clearSound;
    private AudioSource audioSource;

    private void Start()
    {
        clearImage = GetComponent<Image>();
        clearImage.gameObject.SetActive(false);

        audioSource = gameObject.AddComponent<AudioSource>();

        GameManager.Instance.OnGameClear.AddListener(Show);
    }

    public void Show()
    {
        clearImage.gameObject.SetActive(true);
        PlayClearSound();
    }

    private void PlayClearSound()
    {
        audioSource.PlayOneShot(clearSound); 
    }
}
