using UnityEngine;
using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

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
    public GameObject train_object_1;
    public GameObject train_object_2;
    public GameObject train_object_3;
    bool train_1_choosen = false;
    bool train_2_choosen = false;
    bool train_3_choosen = false;
    GameObject train_destroy;
    int train_destroy_index;
    public Slider health_bar;
    public GameObject credits;
    public GameObject lightning;
    public GameObject light_object;
    public GameObject lightning_screen;

    public int ItemID = 0;
    // Item ID 0 = Hands
    // Item ID 1 = Baseball bat
    
    //States:
    //0: Idle
    //1: Fighting
    //2: Winding Attack
    //3: Attacking
    //4: Waiting to jump
    //5: destroys train
    public int hp = 30;
    public int max_hp = 30;
    int phase = 0;
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

        if(hp <= 20 && phase == 0)
        {
            state = 5;
            timer = 240;
            float destroy = Random.Range(0, 100);
            if(destroy <= 50)
            {
                train_destroy_index = 1;
            }
            else
            {
                train_destroy_index = -1;
            }

            if(train_destroy_index == 1)
            {
                train_destroy = train_object_1;
                train_1_choosen = true;
            }
            else
            {
                train_destroy = train_object_3;
                train_3_choosen = true;
            }

            phase = 1;
        }

        if (hp <= 10 && phase == 1)
        {
            state = 5;
            timer = 240;
            train_destroy_index = -train_destroy_index;

            if (train_destroy_index == 1)
            {
                train_destroy = train_object_1;
                train_1_choosen = true;
            }
            else
            {
                train_destroy = train_object_3;
                train_3_choosen = true;
            }

            phase = 2;
        }

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
        if (state != 5)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                Destroy(gameObject);
            }

            Flip();

            if (IsGrounded())
            {
                if (grounded == false)
                {
                    screen_shake.GetComponent<CameraController>().shake = 25f;
                    if (train_1_choosen == false) train_1.GetComponent<Flash>().flash = 15f;
                    if (train_2_choosen == false) train_2.GetComponent<Flash>().flash = 15f;
                    if (train_3_choosen == false) train_3.GetComponent<Flash>().flash = 15f;
                    flash = 15f;

                    if (player.GetComponent<PlayerCore>().IsGrounded())
                    {
                        GameObject Attack = Instantiate(BatSwing, new Vector2(player_transform.position.x, player_transform.position.y + 1), Quaternion.identity);
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
                if (state == 1)
                {
                    if (transform.position.y >= 15f)
                    {
                        transform.position = new Vector2(position_target.x + Random.Range(-1, 1), 12f);
                        rb.velocity = new Vector2(rb.velocity.x, 0f);
                        PunchTimer = 0;

                        Debug.Log(transform.position);
                    }
                }

                grounded = false;
            }

            knockback += (0 - knockback) * 0.1f;

            rb.velocity = new Vector2((speed * EnemySpeed) - ((knockback * 0.05f) * flip), rb.velocity.y);

            Flash();

            PunchCode();

            if (timer > 0) timer--;
            if (timer == 0 && state == 4)
            {
                timer = 20;
                state = 1;
            }
        }
        else
        {
            Vector3 flying_position = new Vector3(35f, 2f , 0f);
            transform.position += (flying_position - transform.position) * 0.1f;
            rb.velocity = new Vector2(0f, 0f);

            PunchTimer = 0;

            Flash();

            if (timer <= 120)
            {
                if (train_destroy_index == 1)
                {
                    train_1.GetComponent<Flash>().flash += 5;
                }
                else
                {
                    train_3.GetComponent<Flash>().flash += 5;
                }
            }

            if (timer <= 0)
            {
                screen_shake.GetComponent<CameraController>().shake = 30f;
                screen_shake.GetComponent<Camera>().backgroundColor = new Color(0.157f, 0.2f, 0.37f, 1f);
                light_object.GetComponent<Light2D>().color = new Color(0.157f, 0.2f, 0.37f, 1f);
                lightning_screen.GetComponent<LightningScreen>().alpha = 3f;

                Instantiate(blood, train_destroy.transform.position+new Vector3(Random.Range(-2,2),0f,0f), Quaternion.identity);
                Instantiate(blood, train_destroy.transform.position + new Vector3(Random.Range(-2, 2), 0f, 0f), Quaternion.identity);
                Instantiate(blood, train_destroy.transform.position + new Vector3(Random.Range(-2, 2), 0f, 0f), Quaternion.identity);

                Instantiate(lightning, train_destroy.transform.position + new Vector3(0f, 7f, 0f), Quaternion.identity);

                Destroy(train_destroy);

                position_target = player_transform.position;
                sprite.GetComponent<Scale>().scale_x = 0.5f;
                sprite.GetComponent<Scale>().scale_y = 1.5f;
                rb.velocity = new Vector2(rb.velocity.x, 60f);

                state = 1;
            }

            if (timer > 0) timer--;
        }
    }

    void Flash()
    {
        if (flash > 0f)
        {
            flash_sprite.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, flash / 10);
        }
        else
        {
            flash_sprite.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 0f);
        }

        flash--;
        flash = Mathf.Clamp(flash, 0f, 100f);
    }

    void AnimationCode()
    {
        if (state != 5)
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
        else
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

            if (state == 0 || state == 1 && state != 2 && state != 4 && IsGrounded())
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

            if(PunchTimer == 20)
            {
                sprite.GetComponent<Scale>().scale_x = 0.65f;
                sprite.GetComponent<Scale>().scale_y = 1.35f;
            }

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