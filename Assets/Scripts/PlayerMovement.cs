using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public LayerMask mask;
    public GameObject sprite;
    bool grounded = false;
    float speed = 0f;
    int Grounds = 0;
    [SerializeField] float PlayerSpeed;
    [SerializeField] float PlayerJumpHeight;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        speed = 0;
        //speed += ((Input.GetAxisRaw("Horizontal") * 4f) - speed) * 0.035f;
        if (Input.GetKey(KeyCode.D))
        {
            speed++;
        }
        if (Input.GetKey(KeyCode.A))
        {
            speed--;
        }

        rb.velocity = new Vector2(speed * PlayerSpeed, rb.velocity.y);



        if (Physics2D.Raycast(transform.position, Vector2.down, 0.25f, mask))
        {
            if (grounded == false)
            {
                //sprite.GetComponent<Scale>().scale_x = 2f;
                //sprite.GetComponent<Scale>().scale_y = 0.25f;
            }

            grounded = true;
        }
        else
        {
            grounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && Grounds !=0)
        {
            rb.velocity = new Vector2(rb.velocity.x, PlayerJumpHeight);

            //sprite.GetComponent<Scale>().scale_x = 0.25f;
            //sprite.GetComponent<Scale>().scale_y = 2f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<GroundScript>())
        {
            Grounds++;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<GroundScript>())
        {
            Grounds--;
        }
    }
}
