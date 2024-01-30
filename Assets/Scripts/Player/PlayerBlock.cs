using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    PlayerCore thePlayerCore;
    Transform theTransform;
    Rigidbody2D theRigidbody;

    void Start()
    {
        thePlayerCore = gameObject.GetComponent<PlayerCore>();
        theTransform = gameObject.GetComponent<Transform>();
        theRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (thePlayerCore.currentState == 0 || thePlayerCore.currentState == 2)
        {
            if (Input.GetMouseButton(1) && thePlayerCore.IsGrounded())
            {
                thePlayerCore.currentState = 2;

            }
            else
            {
                thePlayerCore.currentState = 0;
            }
        }
    }

    public int BlockDamage(int Dmg)
    {
        if (thePlayerCore.currentState == 2)
        {
            if (ItemTracker.CurrentItemID == 0)
            {
                return Dmg - 1;
            }
            else if (ItemTracker.CurrentItemID == 1)
            {
                ItemTracker.CurrentItemDurability--;
                if (ItemTracker.CurrentItemDurability <= 0)
                {
                    ItemTracker.CurrentItemID = 0;
                }
                return Dmg - 2;
            }
            else if (ItemTracker.CurrentItemID == 2)
            {
                ItemTracker.CurrentItemDurability--;
                if (ItemTracker.CurrentItemDurability <= 0)
                {
                    ItemTracker.CurrentItemID = 0;
                }
                return Dmg - 2;
            }
            else
            {
                return Dmg;
            }
        }
        else
        {
            return Dmg;
        }

    }
}
