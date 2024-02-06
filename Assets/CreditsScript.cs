using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsScript : MonoBehaviour
{
    public Text text_component;
    int number = 1;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (number == 1)
            {
                text_component.text = "CREDITS (Press Space to Continue)\n\nEverybody here did a fantastic job, and thank you very much for playing this game!";
            }

            if (number == 2)
            {
                SceneManager.LoadScene("MainMenu");
                ScoreTracker.Score = 0;
            }
            number++;
        }
    }
}
