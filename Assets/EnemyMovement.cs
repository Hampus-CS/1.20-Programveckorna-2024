using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public LayerMask mask;
    public GameObject sprite;
    bool grounded = false;
    float speed = 0f;
    public ParticleSystem dust;
    float flash = 0f;
    public GameObject id;
    public GameObject screen_shake;
    public GameObject player;

    int hp = 5;
    bool hit = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        flash--;
        flash = Mathf.Clamp(flash, 0, 100f);

        float playerSide = (player.transform.position.x - transform.position.x);
        playerSide = Mathf.Clamp(playerSide, -1, 1);

        speed += ((2f * playerSide) - speed) * 0.1f;

        rb.velocity = new Vector2(speed, rb.velocity.y);

        if (Physics2D.Raycast(transform.position, Vector2.down, 0.25f, mask))
        {
            if (grounded == false)
            {
                sprite.GetComponent<Scale>().scale_x = 2f;
                sprite.GetComponent<Scale>().scale_y = 0.25f;

                Instantiate(dust, transform.position, Quaternion.identity);
            }

            grounded = true;
        }
        else
        {
            grounded = false;
        }
        /*
        if (Input.GetKeyUp("w") && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 15f);

            sprite.GetComponent<Scale>().scale_x = 0.25f;
            sprite.GetComponent<Scale>().scale_y = 2f;
        }
        */

        if (flash > 0f)
        {
            sprite.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f, 0f);
        }
        else
        {
            sprite.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f, 1f);

            hit = false;
        }

        if (hp <= 0) Destroy(id);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Attack" && hit == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y+5f);

            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 5f);

            sprite.GetComponent<Scale>().scale_x = 0.25f;
            sprite.GetComponent<Scale>().scale_y = 2f;

            hp--;

            speed = -speed * 50f;

            flash = 30f;
            hit = true;

            screen_shake.GetComponent<CameraCode>().shake = 2f;
        }
    }
}