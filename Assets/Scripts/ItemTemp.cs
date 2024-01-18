using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemTemp : MonoBehaviour
{
    TextMeshProUGUI TheTMP;
    void Start()
    {
        TheTMP = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (ItemTracker.CurrentItemID == 0)
        {
            TheTMP.text = "Item: " + "None";
        }
        if (ItemTracker.CurrentItemID == 1)
        {
            TheTMP.text = "Item: " + "Baseball bat " + ItemTracker.CurrentItemDurability;
        }
        if (ItemTracker.CurrentItemID == 2)
        {
            TheTMP.text = "Item: " + "Knife " + ItemTracker.CurrentItemDurability;
        }
    }
}
