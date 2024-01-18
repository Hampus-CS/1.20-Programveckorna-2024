using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItem : MonoBehaviour
{
    bool PlayerOnItem = false;
    SpriteRenderer TheSR;
    [SerializeField] public int ThisItemID;
    //IDs:
    //1: Bat
    //2: Knife
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
            ItemTracker.CurrentItemID = ThisItemID;
            ItemTracker.Timer = 5;
            ItemTracker.Delay = true;
            if (ItemTracker.CurrentItemID == 1)
            {
                ItemTracker.CurrentItemDurability = 2;
            }
            if (ItemTracker.CurrentItemID == 2)
            {
                ItemTracker.CurrentItemDurability = 1;    
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