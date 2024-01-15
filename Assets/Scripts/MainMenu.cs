using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{

    public void Play()
    {

        SceneManager.LoadScene("TestLevelsMain");

    }

    public void Quit()
    {

        Debug.Log("Player has quit the game");
        Application.Quit();

    }

}