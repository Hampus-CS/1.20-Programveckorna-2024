using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightningScreen : MonoBehaviour
{
    public Image screen;
    public float alpha = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(alpha > 0f) alpha -= 0.1f;

        screen.color = new Color(1f, 1f, 1f, alpha);
    }
}
