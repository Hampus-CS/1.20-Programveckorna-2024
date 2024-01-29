using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundItem : MonoBehaviour
{
    bool PlayerOnItem = false;
    SpriteRenderer TheSR;
    public int ThisItemID = 1;
    [SerializeField] Sprite BatSprite;
    [SerializeField] Sprite KnifeSprite;
    //IDs:
    //1: Bat
    //2: Knife
    void Start()
    {
        if (Random.Range(0f, 100f) <= 15f)
        {
            ThisItemID = 2;
        }

        TheSR = gameObject.GetComponent<SpriteRenderer>();
        if(ThisItemID == 1)
        {
            TheSR.sprite = BatSprite;
        }
        if(ThisItemID == 2)
        {
            TheSR.sprite = KnifeSprite;
        }
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
        if (collision.gameObject.tag == "Player")
        {
            PlayerOnItem = true;
            TheSR.color = new Color(0.5f, 0.5f, 1);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerOnItem = false;
            TheSR.color = new Color(1, 1, 1);
        }
    }

}