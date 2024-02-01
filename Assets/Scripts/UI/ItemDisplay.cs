using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField] Sprite Fists;
    [SerializeField] Sprite Bat2;
    [SerializeField] Sprite Bat1;
    [SerializeField] Sprite Knife1;
    [SerializeField] Image TheImg;

    // Update is called once per frame
    void Update()
    {
        if (ItemTracker.CurrentItemID == 0)
        {
            TheImg.sprite = Fists;
        }
        if (ItemTracker.CurrentItemID == 1)
        {
            if(ItemTracker.CurrentItemDurability == 2)
            {
                TheImg.sprite = Bat2;
            }
            else
            {
                TheImg.sprite = Bat1;
            }
        }

        if (ItemTracker.CurrentItemID == 2)
        {
            TheImg.sprite = Knife1;
        }
    }
}
