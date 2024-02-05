using UnityEngine;
using UnityEngine.UIElements;

public class Scale : MonoBehaviour
{
    public float scale_x = 1f;
    public float scale_y = 1f;
    public float flip = 1;
    public float scale = 1f;

    // Start is called before the first frame update
    void Start()
    {
        scale_x = scale;
        scale_y = scale;
    }

    // Update is called once per frame
    void Update()
    {
        scale_x += (scale - scale_x) * 0.035f;
        scale_y += (scale - scale_y) * 0.035f;

        transform.localScale = new Vector2(scale_x*flip, scale_y);
    }
}
