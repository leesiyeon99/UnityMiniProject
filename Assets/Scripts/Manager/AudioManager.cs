using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource bgm2;
    [SerializeField] AudioSource sfx;

    [SerializeField] AudioClip bgmClip;
    [SerializeField] AudioClip bgm2Clip;
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

    public void PlayBGM2()
    {
        bgm2.Play();
    }

    public void StopBGM()
    {
        if (!bgm.isPlaying) { return; }
        bgm.Stop();
    }

    public void StopBGM2()
    {
        if (!bgm.isPlaying) { return; }
        bgm2.Stop();
    }

    public void PlaySFX(int index)
    {
        sfx.clip= afxClips[index] ;
        sfx.Play();
    }
}
