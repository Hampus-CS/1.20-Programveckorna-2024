using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPDisplay : MonoBehaviour
{
    [SerializeField] Sprite HP5;
    [SerializeField] Sprite HP4;
    [SerializeField] Sprite HP3;
    [SerializeField] Sprite HP2;
    [SerializeField] Sprite HP1;
    [SerializeField] Image TheImg;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerCore.playerHealth == 5)
        {
            TheImg.sprite = HP5;
        }
        if (PlayerCore.playerHealth == 4)
        {
            TheImg.sprite = HP4;
        }
        if (PlayerCore.playerHealth == 3)
        {
            TheImg.sprite = HP3;
        }
        if (PlayerCore.playerHealth == 2)
        {
            TheImg.sprite = HP2;
        }
        if (PlayerCore.playerHealth == 1)
        {
            TheImg.sprite = HP1;
        }
    }
}
