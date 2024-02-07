using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject mainMenuObject;
    public GameObject creditsObject;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            creditsObject.SetActive(false);
            mainMenuObject.SetActive(true);
        }
    }
}
