using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    public float flash = 10f;
    public GameObject sprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flash > 0f)
        {
            sprite.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, flash / 10);
        }
        else
        {
            sprite.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 0f);
        }

        flash--;
        flash = Mathf.Clamp(flash, 0f, 100f);
    }
}
