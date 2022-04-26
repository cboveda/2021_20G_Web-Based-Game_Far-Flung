using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource music;
    private static MusicPlayer singlePlayer = null;
    private AudioClip clip;
    private const float maxVolume = 0.5f;
    
    public static MusicPlayer Instance
    {
        get
        { 
            return singlePlayer;

        }
    }

    private void Awake()
    {
        if(singlePlayer != null && singlePlayer != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            singlePlayer = this;
            
        }
        singlePlayer.music = GetComponent<AudioSource>();
        singlePlayer.clip = singlePlayer.music.clip;
        DontDestroyOnLoad(transform.gameObject);
        
    }

    public void StartMusic()
    {
        if(singlePlayer.music.isPlaying) return;
        float randStart = Random.Range(0f, singlePlayer.clip.length - 30);
        singlePlayer.music.time = randStart;
        singlePlayer.music.Play();
        StartCoroutine(singlePlayer.FadeAudioIn());

    }

    public void StopMusic()
    {
        singlePlayer.music.volume = 0f;
        singlePlayer.music.Stop();
    }

    public IEnumerator FadeAudioIn()
    {
        float currentTime = 0;
        while (currentTime < 2)
        {
            currentTime += Time.deltaTime;
            singlePlayer.music.volume = Mathf.Lerp(0f, maxVolume, currentTime / 2);
            yield return null;
        }
        yield break;
    }
}
