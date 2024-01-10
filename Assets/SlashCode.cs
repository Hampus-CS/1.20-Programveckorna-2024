using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SlashCode : MonoBehaviour
{
    public Transform creator;
    public GameObject id;
    float Timer = 50f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer--;

        if (Timer <= 0f) Destroy(id);

        transform.position = new Vector2(creator.position.x+1f, creator.position.y+0.5f);
    }
}
