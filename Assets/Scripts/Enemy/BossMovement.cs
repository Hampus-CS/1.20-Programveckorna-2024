using UnityEngine;
using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;
using Unity.VisualScripting;
using UnityEngine.UI;

public class BossMovement : MonoBehaviour
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
    public Transform player_transform;
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
    int punch_index = 1;
    Vector3 position_target;
    int timer = 0;
    public GameObject train_1;
    public GameObject train_2;
    public GameObject train_3;
    public Slider health_bar;
    public GameObject credits;

    public int ItemID = 0;
    // Item ID 0 = Hands
    // Item ID 1 = Baseball bat
    
    //States:
    //0: Idle
    //1: Fighting
    //2: Winding Attack
    //3: Attacking
    public int hp = 20;
    public int max_hp = 20;
    //bool hit = false;

    // Start is called before the first frame update
    void Start()
    {

        // Assign a random number between 0 and 1 to ItemID
        
        TheSR.color = EColors[ItemID];

    }

    // Update is called once per frame
    void Update()
    {
        //Animations

        AnimationCode();

        health_bar.value = hp;
        health_bar.maxValue = max_hp;

        if (hp <= 0)
        {
            credits.SetActive(true);

            Destroy(id);
        }
        if (state == 2)
        {
            TheSR.color = new Color(1, 1, 0);
        }
        else
        {
            TheSR.color = EColors[ItemID];
        }
    }

    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            Destroy(gameObject);
        }

        Flip();

        if (IsGrounded())
        {
            if (grounded == false)
            {
                screen_shake.GetComponent<CameraController>().shake = 25f;
                train_1.GetComponent<Flash>().flash = 15f;
                train_2.GetComponent<Flash>().flash = 15f;
                train_3.GetComponent<Flash>().flash = 15f;
                flash = 15f;

                if (player.GetComponent<PlayerCore>().IsGrounded())
                {
                    GameObject Attack = Instantiate(Punch, new Vector2(player_transform.position.x, player_transform.position.y + 1), Quaternion.identity);
                }

                sprite.GetComponent<Scale>().scale_x = 1.25f;
                sprite.GetComponent<Scale>().scale_y = 0.75f;

                timer = 40;
                state = 4;
            }

            grounded = true;

            HuntingPlayer();
        }
        else
        {
            if(state == 1)
            {
                if(transform.position.y >= 15f)
                {
                    transform.position = new Vector2(position_target.x, 12f);
                    rb.velocity = new Vector2(rb.velocity.x, 0f);

                    Debug.Log(transform.position);
                }
            }

            grounded = false;
        }

        Debug.Log(state);

        knockback += (0 - knockback) * 0.1f;

        rb.velocity = new Vector2((speed * EnemySpeed) - ((knockback * 0.05f) * flip), rb.velocity.y);

        if (flash > 0f)
        {
            flash_sprite.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, flash/10);
        }
        else
        {
            flash_sprite.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 0f);
        }

        flash--;
        flash = Mathf.Clamp(flash, 0f, 100f);

        PunchCode();

        if (timer > 0) timer--;
        if (timer == 0 && state == 4)
        {
            timer = 20;
            state = 1;
        }
    }

    void AnimationCode()
    {
        if (state == 0 || state == 1 || state == 4)
        {
            if (PunchTimer <= 0f)
            {
                if (!IsGrounded())
                {
                    sprite.GetComponent<EnemyAnimation>().animation_state = 1;
                    flash_sprite.GetComponent<EnemyAnimation>().animation_state = 1;
                }
                else
                {
                    sprite.GetComponent<EnemyAnimation>().animation_state = 0;
                    flash_sprite.GetComponent<EnemyAnimation>().animation_state = 0;
                }
            }
        }
        else
        {
            if (state == 3)
            {
                if (ItemID == 0)
                {
                    if (punch_index == 1)
                    {
                        sprite.GetComponent<EnemyAnimation>().animation_state = 2;
                        flash_sprite.GetComponent<EnemyAnimation>().animation_state = 2;
                    }
                    else
                    {
                        sprite.GetComponent<EnemyAnimation>().animation_state = 3;
                        flash_sprite.GetComponent<EnemyAnimation>().animation_state = 3;
                    }
                }

                if (ItemID == 1)
                {
                    sprite.GetComponent<EnemyAnimation>().animation_state = 4;
                    flash_sprite.GetComponent<EnemyAnimation>().animation_state = 4;
                }
            }
            else
            {
                if (ItemID == 0)
                {
                    sprite.GetComponent<EnemyAnimation>().animation_state = 5;
                    flash_sprite.GetComponent<EnemyAnimation>().animation_state = 5;
                }

                if (ItemID == 1)
                {
                    sprite.GetComponent<EnemyAnimation>().animation_state = 6;
                    flash_sprite.GetComponent<EnemyAnimation>().animation_state = 6;
                }
            }
        }

        if (state == 4)
        {
            sprite.GetComponent<EnemyAnimation>().animation_state = 0;
            flash_sprite.GetComponent<EnemyAnimation>().animation_state = 0;
        }

        if (state == 1 && IsGrounded())
        {
            sprite.GetComponent<EnemyAnimation>().animation_state = 0;
            flash_sprite.GetComponent<EnemyAnimation>().animation_state = 0;
        }

        if (state == 1 && !IsGrounded())
        {
            sprite.GetComponent<EnemyAnimation>().animation_state = 1;
            flash_sprite.GetComponent<EnemyAnimation>().animation_state = 1;
        }
    }

    void HuntingPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > 4.5f && state != 2 && state != 4)
        {
            if (state != 1)
            {
                timer = 20;
            }

            sprite.GetComponent<Scale>().scale_x += (1.5f - sprite.GetComponent<Scale>().scale_x) * 0.25f;
            sprite.GetComponent<Scale>().scale_y += (0.5f - sprite.GetComponent<Scale>().scale_y) * 0.25f;

            if (timer <= 0)
            {
                position_target = player_transform.position;
                sprite.GetComponent<Scale>().scale_x = 0.5f;
                sprite.GetComponent<Scale>().scale_y = 1.5f;
                rb.velocity = new Vector2(rb.velocity.x, 60f);
            }

            state = 1;
        }
        else
        {
            speed = 0;

            if (state == 0 || state == 1)
            {
                if (mouse_side == 1)
                {
                    PunchDirRight = true;
                    state = 2;
                    PunchTimer = 40;
                }
                else
                {
                    PunchDirRight = false;
                    state = 2;
                    PunchTimer = 40;
                }
            }
        }
    }

    void Flip()
    {
        if (PunchTimer <= 0f || state == 1)
        {
            mouse_side = (player_transform.position.x - transform.position.x);
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
    }

    void PunchCode()
    {
        if (state == 2)
        {
            PunchTimer--;
            if (PunchTimer == 0)
            {
                state = 3;

                punch_index = -punch_index;

                if (PunchDirRight)
                {
                    if (ItemID == 0)
                    {
                        if (Vector3.Distance(transform.position, player.transform.position) <= 4.5f && player.GetComponent<PlayerCore>().IsGrounded())
                        {
                            GameObject Attack = Instantiate(Punch, new Vector2(player_transform.position.x, player_transform.position.y + 1), Quaternion.identity);
                        }
                        PunchTimer = 30;
                    }
                    else if (ItemID == 1)
                    {
                        GameObject Attack = Instantiate(BatSwing, new Vector2(player_transform.position.x, player_transform.position.y + 1), Quaternion.identity);
                        PunchTimer = 50;
                    }
                }
                else
                {
                    if (ItemID == 0)
                    {
                        if (Vector3.Distance(transform.position, player.transform.position) <= 4.5f && player.GetComponent<PlayerCore>().IsGrounded())
                        {
                            GameObject Attack = Instantiate(Punch, new Vector2(player_transform.position.x, player_transform.position.y + 1), Quaternion.identity);
                        }
                        PunchTimer = 30;
                    }
                    else if (ItemID == 1)
                    {
                        GameObject Attack = Instantiate(BatSwing, new Vector2(transform.position.x, transform.position.y + 1), Quaternion.identity);
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

    void OnTriggerEnter2D(Collider2D collsiion)
    {
        if (collsiion.tag == "killbox")
        {
            position_target = player_transform.position;
            sprite.GetComponent<Scale>().scale_x = 0.5f;
            sprite.GetComponent<Scale>().scale_y = 1.5f;
            rb.velocity = new Vector2(rb.velocity.x, 60f);

            state = 1;
        }
    }
}