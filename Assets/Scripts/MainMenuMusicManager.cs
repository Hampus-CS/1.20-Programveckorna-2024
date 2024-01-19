using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusicManager : MonoBehaviour
{
    public AudioSource menuMusic;

    void Start()
    {
        PlayMainMenuMusic();
    }

    public void PlayMainMenuMusic()
    {
        if (menuMusic != null && !menuMusic.isPlaying)
        {
            menuMusic.Play();
        }
    }

    public void StopMainMenuMusic()
    {
        if (menuMusic != null && menuMusic.isPlaying)
        {
            menuMusic.Stop();
        }
    }

}