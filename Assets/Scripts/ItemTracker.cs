using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTracker : MonoBehaviour
{
    public static int CurrentItemID;
    public static int CurrentItemDurability = 0;
    public static bool Delay;
    public static int Timer;
    //Item ID list: 
    //Fists: 0
    //Bat: 1
    //Knife: 2
    void Start()
    {
        CurrentItemID = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Timer > 0)
        {
            Timer--;
        }
        if(Timer == 0)
        {
            Delay = false;
        }
    }
}
