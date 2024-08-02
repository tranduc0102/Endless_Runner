using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Header("----------------------AudioSource----------------------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;
    
    [Header("----------------------AudioClip------------------------")]
    public AudioClip jump;
    public AudioClip coin;
    public AudioClip gamePlay;
    public AudioClip musicLobby;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        musicSource.PlayOneShot(musicLobby);
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayMusicSource(AudioClip clip)
    {
        musicSource.Stop();
        musicSource.PlayOneShot(clip);
    }
}
