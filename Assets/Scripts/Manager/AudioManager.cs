using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Range(0,1)]
    public float musicVolume;
    [Range(0,1)]
    public float soundVolume;

    public AudioSource musicAudio;
    public AudioSource soundAudio;

    public AudioClip[] backgroundMusic;
    public AudioClip bounceBall;
    public AudioClip colliderObstacle;

    private void Awake() 
    {
        if( instance == null)
        {
            instance = this;
        }
        else if( instance != null)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayBackgroundMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayBackgroundMusic()
    {
        if(musicAudio && backgroundMusic != null && backgroundMusic.Length > 0)
        {
            int randomMusic = Random.Range(0, backgroundMusic.Length);
            if( backgroundMusic[randomMusic] )
            musicAudio.clip = backgroundMusic[randomMusic];
            musicAudio.volume = musicVolume;
            musicAudio.Play();
        }
    }

    public void PlaySound(AudioClip sound)
    {
        if(soundAudio && sound)
        {
            soundAudio.PlayOneShot(sound);
            soundAudio.volume = soundVolume;
        }
    }
    
    public void BouncingBallSound()
    {
        PlaySound(bounceBall);
    }

    public void ColliderObstacle()
    {
        PlaySound(colliderObstacle);
    }
    
    public void Stop()
    {
        if( musicAudio != null)
        {
            musicAudio.Stop();
        }
    }
}
