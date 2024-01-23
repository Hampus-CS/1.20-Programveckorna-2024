using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public int animation_state = 0;

    public Animator anim;
    PlayerCore thePlayerCore;
    PlayerAttack thePlayerAttack;

    // Start is called before the first frame update
    void Start()
    {
        thePlayerCore = GetComponentInParent<PlayerCore>();
        thePlayerAttack = GetComponentInParent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (thePlayerCore.currentState != 2)
        {
            if (thePlayerAttack.attackTimer <= 0f)
            {
                if (Input.GetAxisRaw("Horizontal") != 0f)
                {
                    animation_state = 1;
                }
                else
                {
                    animation_state = 0;
                }
            }
            else
            {
                if (ItemTracker.CurrentItemID == 0)
                {
                    if (thePlayerCore.punchIndex == 1) animation_state = 2;
                    else animation_state = 3;
                }

                if (ItemTracker.CurrentItemID == 1)
                {
                    animation_state = 4;
                }
            }
        }
        else
        {
            if (ItemTracker.CurrentItemID == 0)
            {
                animation_state = 5;
            }

            if (ItemTracker.CurrentItemID == 1)
            {
                animation_state = 6;
            }
        }




        anim.SetInteger("AnimationState", (int)animation_state);
    }
}
