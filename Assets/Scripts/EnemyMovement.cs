using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public LayerMask mask;
    public GameObject sprite;
    //bool grounded = false;
    public float speed = 0f;
    public ParticleSystem dust;
    public float flash = 0f;
    public GameObject id;
    public GameObject screen_shake;
    public GameObject player;
    [SerializeField] LayerMask PMask;
    [SerializeField] float EnemySpeed;
    [SerializeField] Transform TheT;
    [SerializeField] GameObject Punch;
    [SerializeField] GameObject BatSwing;
    [SerializeField] SpriteRenderer TheSR;
    bool PunchDirRight;
    int PunchTimer = 0;
    int state = 0;
    Color[] EColors = { new Color(0, 0.7f, 0.1f), new Color(0.4f, 0.7f, 0) };
    bool grounded = false;
    public float mouse_side = 1;
    public GameObject blood;
    public GameObject flash_sprite;
    public float knockback = 0f;
    float flip = 0;

    [SerializeField] int ItemID;
    //States:
    //0: Idle
    //1: Fighting
    //2: Winding Attack
    //3: Attacking
    public int hp = 3;
    //bool hit = false;

    // Start is called before the first frame update
    void Start()
    {
        TheSR.color = EColors[ItemID];
    }

    // Update is called once per frame
    void Update()
    {
        

        //float playerSide = (player.transform.position.x - transform.position.x);
        //playerSide = Mathf.Clamp(playerSide, -1, 1);

        //speed += ((2f * playerSide) - speed) * 0.1f;

        //rb.velocity = new Vector2(speed, rb.velocity.y);

        
        /*
        if (Input.GetKeyUp("w") && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 15f);

            sprite.GetComponent<Scale>().scale_x = 0.25f;
            sprite.GetComponent<Scale>().scale_y = 2f;
        }
        */

        if (hp <= 0) Destroy(id);
        if (state == 2)
        {
            TheSR.color = new Color(1, 1, 0);
        }
        else
        {
            TheSR.color = EColors[ItemID];
        }
    }
    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //   if(collision.tag == "Attack" && hit == false)
    //    {
            //rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y+5f);

            //rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 5f);

            //sprite.GetComponent<Scale>().scale_x = 0.25f;
            //sprite.GetComponent<Scale>().scale_y = 2f;

    //        hp--;

            //speed = -speed * 50f;

    //        flash = 30f;
    //        hit = true;

    //        screen_shake.GetComponent<CameraController>().shake = 15f;
    //    }
    //}

    private void FixedUpdate()
    {
        if (PunchTimer <= 0f)
        {
            mouse_side = speed;
        }
        else
        {
            mouse_side = 0;
        }
        mouse_side = Mathf.Clamp(mouse_side, -1, 1);
        if (mouse_side != -1 && mouse_side != 1)
        {
            if (mouse_side < 0) mouse_side = -1;
            if (mouse_side > 0) mouse_side = 1;
        }
        if (mouse_side != 0) sprite.GetComponent<Scale>().flip = mouse_side;
        if (mouse_side != 0) flip = mouse_side;

        if (IsGrounded())
        {
            if (grounded == false)
            {
                sprite.GetComponent<Scale>().scale_x = 1.25f;
                sprite.GetComponent<Scale>().scale_y = 0.75f;
            }

            grounded = true;
        }
        else
        {
            grounded = false;
        }

        if(flash > 0f)
        {
            flash_sprite.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, flash/10);
        }
        else
        {
            flash_sprite.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 0f);
        }

        flash--;
        flash = Mathf.Clamp(flash, 0f, 100f);
        RaycastHit2D hitL = Physics2D.Raycast(new Vector2(TheT.position.x - 0.6f, TheT.position.y+1), Vector2.left, 5, PMask);
        RaycastHit2D hitR = Physics2D.Raycast(new Vector2(TheT.position.x + 0.6f, TheT.position.y+1), Vector2.right, 5, PMask);
        speed = 0;
        if(state == 0 || state == 1)
        {
            if (hitL.collider != null)
            {
                if (ItemID == 0)
                {
                    if (hitL.distance <= 0.8)
                    {
                        PunchDirRight = false;
                        state = 2;
                        PunchTimer = 40;
                    }
                    else
                    {
                        speed = -1;
                        state = 1;
                    }
                }
                else if (ItemID == 1)
                {
                    if (hitL.distance <= 1.5)
                    {
                        PunchDirRight = false;
                        state = 2;
                        PunchTimer = 40;
                    }
                    else
                    {
                        speed = -1;
                        state = 1;
                    }
                }
                
                
                
            }
            else if (hitR.collider != null)
            {
                if (ItemID == 0)
                {
                    if (hitR.distance <= 0.8)
                    {
                        PunchDirRight = true;
                        state = 2;
                        PunchTimer = 40;
                    }
                    else
                    {
                        speed = 1;
                        state = 1;
                    }
                }
                else if (ItemID == 1)
                {
                    if (hitL.distance <= 1.5)
                    {
                        PunchDirRight = true;
                        state = 2;
                        PunchTimer = 60;
                    }
                    else
                    {
                        speed = -1;
                        state = 1;
                    }
                }

                
            }
            else
            {
                state = 0;
            }

            knockback += (0 - knockback) * 0.1f;

            rb.velocity = new Vector2((speed * EnemySpeed) - (knockback*flip), rb.velocity.y);
        }
        if (state == 2)
        {
            PunchTimer--;
            if (PunchTimer == 0)
            {
                state = 3;
                
                
                if (PunchDirRight)
                {
                    if(ItemID == 0)
                    {
                        GameObject Attack = Instantiate(Punch, new Vector2(transform.position.x + 1.1f, transform.position.y + 1), Quaternion.identity);
                        PunchTimer = 30;
                    }
                    else if (ItemID == 1)
                    {
                        GameObject Attack = Instantiate(BatSwing, new Vector2(transform.position.x + 1.6f, transform.position.y + 1), Quaternion.identity);
                        PunchTimer = 50;
                    }
                }
                else
                {
                    if (ItemID == 0)
                    {
                        GameObject Attack = Instantiate(Punch, new Vector2(transform.position.x - 1.1f, transform.position.y + 1), Quaternion.identity);
                        PunchTimer = 30;
                    }
                    else if (ItemID == 1)
                    {
                        GameObject Attack = Instantiate(BatSwing, new Vector2(transform.position.x - 1.6f, transform.position.y + 1), Quaternion.identity);
                        PunchTimer = 50;
                    }
                }
            }

            knockback += (0 - knockback) * 0.1f;

            rb.velocity = new Vector2(-(knockback * flip), rb.velocity.y);
        }
        if (state == 3)
        {
            PunchTimer--;
            if (PunchTimer == 0)
            {
                state = 0;
            }

            knockback += (0 - knockback) * 0.1f;

            rb.velocity = new Vector2(-(knockback * flip), rb.velocity.y);
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D hitL = Physics2D.Raycast(new Vector2(TheT.position.x - 0.6f, TheT.position.y), Vector2.down, 0.5f, mask);
        RaycastHit2D hitC = Physics2D.Raycast(new Vector2(TheT.position.x, TheT.position.y), Vector2.down, 0.5f, mask);
        RaycastHit2D hitR = Physics2D.Raycast(new Vector2(TheT.position.x + 0.6f, TheT.position.y), Vector2.down, 0.5f, mask);

        if (hitL.collider != null || hitC.collider != null || hitR.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}