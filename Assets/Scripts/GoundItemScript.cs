using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoundItemScript : MonoBehaviour
{
    bool PlayerOnItem = false;
    SpriteRenderer TheSR;
    void Start()
    {
        TheSR = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PlayerOnItem)
        {
            Destroy(gameObject);
            ItemTracker.CurrentItemID = 1;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player Character")
        {
            PlayerOnItem = true;
            TheSR.color = new Color(0.5f, 0.5f, 1);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player Character")
        {
            PlayerOnItem = false;
            TheSR.color = new Color(1, 1, 1);
        }
    }
}
