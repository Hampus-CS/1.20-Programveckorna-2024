using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//HCS ansvarig
public class mainMenu : MonoBehaviour
{

    public void Play()
    {

        SceneManager.LoadScene("SampleScene");

    }

    public void Quit()
    {

        Application.Quit();
        Debug.Log("Player has quit the game");

    }

}