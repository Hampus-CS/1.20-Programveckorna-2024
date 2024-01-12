using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SlashCode : MonoBehaviour
{
    public Transform creator;
    public GameObject id;
    float Timer = 50f;
    public float flip = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(4 * flip, transform.localScale.y, transform.localScale.z);

        Timer--;

        if (Timer <= 0f) Destroy(id);

        transform.position = new Vector2(creator.position.x+(1f*flip), creator.position.y+0.5f);
    }
}
