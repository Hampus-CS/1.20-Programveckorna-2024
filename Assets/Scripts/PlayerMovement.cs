using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public LayerMask mask;
    public GameObject sprite;
    bool grounded = false;
    float speed = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        speed += ((Input.GetAxisRaw("Horizontal") * 4f) - speed) * 0.035f;

        rb.velocity = new Vector2(speed, rb.velocity.y);

        if (Physics2D.Raycast(transform.position, Vector2.down, 0.25f, mask))
        {
            if (grounded == false)
            {
                sprite.GetComponent<Scale>().scale_x = 2f;
                sprite.GetComponent<Scale>().scale_y = 0.25f;
            }

            grounded = true;
        }
        else
        {
            grounded = false;
        }

        if (Input.GetKeyUp("w") && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 15f);

            sprite.GetComponent<Scale>().scale_x = 0.25f;
            sprite.GetComponent<Scale>().scale_y = 2f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
