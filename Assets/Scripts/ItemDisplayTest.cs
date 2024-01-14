using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemDisplayTest : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TMP;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ItemTracker.CurrentItemID == 0)
        {
            TMP.text = "None";
        }
        if (ItemTracker.CurrentItemID == 1)
        {
            TMP.text = "Baseball Bat " + ItemTracker.CurrentItemDurability;
        }
    }
}
