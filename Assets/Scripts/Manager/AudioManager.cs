using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource sfx;

    [SerializeField] AudioClip bgmClip;
    [SerializeField] AudioClip[] afxClips;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void PlayBGM()
    {
        bgm.Play();
    }

    public void StopBGM()
    {
        if (!bgm.isPlaying) { return; }
        bgm.Stop();
    }

    public void PlaySFX(int index)
    {
        sfx.clip= afxClips[index] ;
        sfx.Play();
    }

    public void OnButton() // �÷��̾ �����Ҷ� ��ư�� �۵��ϴ� �����߻�
    {
        if (bgm.isPlaying)
        {
            bgm.Stop();
        }
        else
        {
            bgm.Play();
        }
    }
}
