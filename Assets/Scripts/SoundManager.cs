using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource musicSourceAlt; // For crossfading


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void FadeInMusic(AudioClip clip, bool loop = true, float fadeInDuration =2.5f)
    {
        musicSource.clip = clip;
        musicSource.loop = true;

        musicSource.volume = 0f;
        musicSource.Play();

        StartCoroutine(FadeAudio(musicSource, 0f, 1f, fadeInDuration));
    }

    public void FadeOutMusic(float duration = 1f)
    {
        
    }

    private IEnumerator FadeAudio(AudioSource source, float from, float to, float duration, bool stopAfterFade = false)
    {
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            source.volume = Mathf.Lerp(from, to, timer / duration);
            yield return null;
        }
        source.volume = to;

        if (stopAfterFade)
            source.Stop();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void CrossfadeMusic(AudioClip newClip, float fadeDuration = 2f)
    {
      
        AudioSource active = musicSource.isPlaying ? musicSource : musicSourceAlt;
        AudioSource inactive = (active == musicSource) ? musicSourceAlt : musicSource;

        inactive.clip = newClip;
        inactive.loop = true;
        inactive.volume = 0f;
        inactive.Play();

        StartCoroutine(FadeAudio(active, active.volume, 0f, fadeDuration, stopAfterFade: true));
        StartCoroutine(FadeAudio(inactive, 0f, 1f, fadeDuration));
    }


}
