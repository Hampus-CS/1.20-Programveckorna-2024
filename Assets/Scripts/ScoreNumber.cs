using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreNumber : MonoBehaviour
{
    TextMeshProUGUI TheTMP;
    // Start is called before the first frame update
    void Start()
    {
        TheTMP = gameObject.GetComponent<TextMeshProUGUI>();
        TheTMP.text = ""+ScoreTracker.Score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
