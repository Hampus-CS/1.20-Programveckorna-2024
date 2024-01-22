using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScript : MonoBehaviour
{
    public void LoadMenu()
    {

        SceneManager.LoadScene("MainMenu");
        ScoreTracker.Score = 0;

    }

}
