using UnityEngine;
using UnityEngine.UIElements;

public class Scale : MonoBehaviour
{
    public float scale_x = 1f;
    public float scale_y = 1f;

    // Start is called before the first frame update
    void Start()
    {
        scale_x = 1f;
        scale_y = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        scale_x += (1 - scale_x) * 0.035f;
        scale_y += (1 - scale_y) * 0.035f;

        transform.localScale = new Vector2(scale_x, scale_y);
    }
}
