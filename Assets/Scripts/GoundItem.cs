using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoundItem : MonoBehaviour
{
    bool PlayerOnItem = false;
    SpriteRenderer TheSR;
    int ThisItemID;
    //IDs:
    //1: Bat
    void Start()
    {
        TheSR = gameObject.GetComponent<SpriteRenderer>();
        ThisItemID = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PlayerOnItem)
        {
            Destroy(gameObject);
            ItemTracker.CurrentItemID = ThisItemID;
            if (ItemTracker.CurrentItemID == 1)
            {
                ItemTracker.CurrentItemDurability = 2;
            }
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