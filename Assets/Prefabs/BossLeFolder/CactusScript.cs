using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusScript : MonoBehaviour
{
    public SpriteRenderer sprite_renderer;
    public Sprite sprite_1;
    public Sprite sprite_2;
    public Sprite sprite_3;
    public Sprite sprite_4;
    float chance = 0f;
    float speed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        chance = Random.Range(0f, 100f);

        transform.position = new Vector2(transform.position.x + Random.Range(-10f, 10f), transform.position.y);

        if(chance > 0f)
        {
            sprite_renderer.sprite = sprite_1;
        }

        if (chance > 25f)
        {
            sprite_renderer.sprite = sprite_2;
        }

        if (chance > 50f)
        {
            sprite_renderer.sprite = sprite_3;
        }

        if (chance > 75f)
        {
            sprite_renderer.sprite = sprite_4;
        }
    }

    private void Update()
    {
        transform.position = new Vector2(transform.position.x - speed*0.1f, transform.position.y);

        if(transform.position.x <= 19)
        {
            transform.position = new Vector2(51f+Random.Range(0f,15f),transform.position.y);

            chance = Random.Range(0f, 100f);

            if (chance > 0f)
            {
                sprite_renderer.sprite = sprite_1;
            }

            if (chance > 25f)
            {
                sprite_renderer.sprite = sprite_2;
            }

            if (chance > 50f)
            {
                sprite_renderer.sprite = sprite_3;
            }

            if (chance > 75f)
            {
                sprite_renderer.sprite = sprite_4;
            }
        }
    }
}
