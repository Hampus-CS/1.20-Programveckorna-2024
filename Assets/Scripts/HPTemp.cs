using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HPTemp : MonoBehaviour
{
    TextMeshProUGUI TheTMP;
    void Start()
    {
        TheTMP = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        TheTMP.text = "HP: " + PlayerMovement.PlayerHealth;
    }
}
