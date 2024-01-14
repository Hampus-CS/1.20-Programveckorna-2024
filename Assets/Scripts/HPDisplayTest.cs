using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HPDisplayTest : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TMP;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TMP.text = "HP: " + PlayerMovement.PlayerHealth;
    }
}
