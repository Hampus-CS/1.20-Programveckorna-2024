using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public LayerMask mask;
    public GameObject sprite;
    bool grounded = false;
    float speed = 0f;
    public ParticleSystem dust;
    public GameObject slash;
    public Transform id;
    public GameObject screen_shake;
    float flip;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        speed += ((Input.GetAxisRaw("Horizontal") * 4f) - speed) * 0.035f;

        float mouse_side = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        mouse_side = Mathf.Clamp(mouse_side, -1, 1);
        if (mouse_side != -1 && mouse_side != 1) mouse_side = 1;

        if (mouse_side != 0) flip = mouse_side;

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

        if (Input.GetKeyDown("w") && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 13f);

            sprite.GetComponent<Scale>().scale_x = 0.25f;
            sprite.GetComponent<Scale>().scale_y = 2f;
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject d = Instantiate(slash, transform.position, Quaternion.identity);
            d.GetComponent<SlashCode>().creator = id;
            d.GetComponent<SlashCode>().flip = flip;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}