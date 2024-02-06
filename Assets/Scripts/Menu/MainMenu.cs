using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class mainMenu : MonoBehaviour
{

    public MainMenuMusicManager musicManager;

    public void Play()
    {
        
        if (musicManager != null)
        {
        
            musicManager.StopMainMenuMusic();
            SceneManager.LoadScene(1);
        
        }
    
    }

    public void PlayBoss()
    {

        if (musicManager != null)
        {

            musicManager.StopMainMenuMusic();
            SceneManager.LoadScene(3);

        }

    }

    public void Quit()
    {

        Debug.Log("Player has quit the game");
        Application.Quit();

    }

}