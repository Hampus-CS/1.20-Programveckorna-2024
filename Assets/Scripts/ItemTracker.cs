using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTracker : MonoBehaviour
{
    //CurrentItemID är variabeln som används för att hålla koll på vilket item spelaren håller i.
    public static int CurrentItemID;
    //Item ID list: 
    //Fists: 0
    //Bat: 1
    void Start()
    {
        CurrentItemID = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log(CurrentItemID);
        }
    }
}
