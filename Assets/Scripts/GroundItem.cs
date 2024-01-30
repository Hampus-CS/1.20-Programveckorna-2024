using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundItem : MonoBehaviour
{
    bool PlayerOnItem = false;
    SpriteRenderer TheSR;
    [SerializeField] public int ThisItemID;
    [SerializeField] Sprite BatSprite;
    [SerializeField] Sprite KnifeSprite;

    [SerializeField] AudioSource batPickUpSource;
    [SerializeField] AudioSource knifePickUpSource;
    //IDs:
    //1: Bat
    //2: Knife
    void Start()
    {
        TheSR = gameObject.GetComponent<SpriteRenderer>();
        if (ThisItemID == 1)
        {
            TheSR.sprite = BatSprite;
        }
        if (ThisItemID == 2)
        {
            TheSR.sprite = KnifeSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PlayerOnItem && ThisItemID != 0)
        {

            TheSR.color = new Color(1, 1, 1, 0);
            ItemTracker.CurrentItemID = ThisItemID;
            ItemTracker.Timer = 5;
            ItemTracker.Delay = true;

            if (ItemTracker.CurrentItemID == 1)
            {
                ItemTracker.CurrentItemDurability = 2;

                batPickUpSource.Play();
            }
            if (ItemTracker.CurrentItemID == 2)
            {
                ItemTracker.CurrentItemDurability = 1;

                knifePickUpSource.Play();
            }
            ThisItemID = 0;
            Destroy(gameObject, 2);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && ThisItemID != 0)
        {
            PlayerOnItem = true;
            TheSR.color = new Color(0.5f, 0.5f, 1);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && ThisItemID != 0)
        {
            PlayerOnItem = false;
            TheSR.color = new Color(1, 1, 1);
        }
    }

}