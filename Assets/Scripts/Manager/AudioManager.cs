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
    private void Start()
    {
        PlayBGM(bgmClip);
    }

    public void PlayBGM(AudioClip clip)
    {
        bgm.clip= clip;
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

    public void OnButton() // 플레이어가 점프할때 버튼도 작동하는 문제발생
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
