using System.Runtime.CompilerServices;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Background Music")]
    [SerializeField] private AudioClip bgMusic;

    [Header("AudioClip")]
    public  AudioClip death;
    public  AudioClip hit;
    public  AudioClip Attack;
    public  AudioClip win;
    public  AudioClip gameOver;
    public  AudioClip speed;
    public  AudioClip DamageBuff;
    public  AudioClip shield;
    public  AudioClip pause;
    public  AudioClip resume;

    void Awake()
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

    void Start()
    {
        if(musicSource != null && bgMusic != null)
        {
            musicSource.clip = bgMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }


    public void PlayDeath()
    {
        sfxSource.PlayOneShot(death);
    }

    public void PlayHit()
    {
        sfxSource.PlayOneShot(hit);
    }

    public void PlayAttack()
    {
        sfxSource.PlayOneShot(Attack);
    }

    public void PlayWin()
    {
        sfxSource.PlayOneShot(win);
    }

    public void PlayGameOver()
    {
        sfxSource.PlayOneShot(gameOver);
    }

    public void PlaySpeed()
    {
        sfxSource.PlayOneShot(speed);
    }

    public void PlayDamageBuff()
    {
        sfxSource.PlayOneShot(DamageBuff);
    }

    public void PlayShield()
    {
        sfxSource.PlayOneShot(shield);
    }

    public void PlayPause()
    {
        sfxSource?.PlayOneShot(pause);
    }

    public void PlayResume()
    {
        sfxSource?.PlayOneShot(resume);
    }
}