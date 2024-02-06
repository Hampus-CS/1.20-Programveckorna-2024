using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningCode : MonoBehaviour
{
    int timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 180;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0) Destroy(gameObject);

        timer--;
    }
}
