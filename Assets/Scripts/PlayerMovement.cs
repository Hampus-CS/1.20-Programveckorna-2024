using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D theRigidbody;
    public GameObject spriteGameObject;
    float speed = 0f;
    [SerializeField] GameObject punch;
    [SerializeField] GameObject batSwing;
    [SerializeField] GameObject batThrow;
    [SerializeField] GameObject knifeSwing;
    [SerializeField] GameObject knifeThrow;
    public Transform theTransform;
    public GameObject screenShake;
    public LayerMask groundMask;
    float raycastLength = 0.5f;
    [SerializeField] float playerMaxSpeed;
    [SerializeField] float playerMaxJumpHeight;
    bool isMouseRightOfPlayer;
    public int currentState;
    public int attackTimer;
    public static int playerHealth;
    [SerializeField] SpriteRenderer theSpriteRenderer;
    bool isGrounded = false;
    public int punchIndex = 1;
    float MouseWorldX;
    float playerDirection;

    //States:
    //0: Nothing specific, can move around
    //1: Currently Attacking
    //2: Currently Blocking

    // Start is called before the first frame update
    void Start()
    {
        //GMask = LayerMask.NameToLayer("Ground");
        theTransform = gameObject.GetComponent<Transform>();
        currentState = 0;
        playerHealth = 3;
    }

    // Update is called once per frame
    
    void Update()
    {
        //if(attackTimer <= 0f && currentState != 2) speed += ((Input.GetAxisRaw("Horizontal") * 1f) - speed) * 0.035f;
        //else speed += (0 - speed) * 0.1f;


        //if (playerHealth <= 0)
        //{
        //    SceneManager.LoadScene("DeathScreen");
        //}
        /*
            speed = 0;
        if (Input.GetKey(KeyCode.D) && State == 0)
        {
            speed++;
        }
        if (Input.GetKey(KeyCode.A) && State == 0)
        {
            speed--;
        }
        */
        //theRigidbody.velocity = new Vector2(speed*playerMaxSpeed, theRigidbody.velocity.y);

        // Animations
        if (currentState != 2)
        {
            if (attackTimer <= 0f)
            {
                if (Input.GetAxisRaw("Horizontal") != 0f)
                {
                    spriteGameObject.GetComponent<PlayerAnimation>().animation_state = 1;
                }
                else
                {
                    spriteGameObject.GetComponent<PlayerAnimation>().animation_state = 0;
                }
            }
            else
            {
                if (ItemTracker.CurrentItemID == 0)
                {
                    if (punchIndex == 1) spriteGameObject.GetComponent<PlayerAnimation>().animation_state = 2;
                    else spriteGameObject.GetComponent<PlayerAnimation>().animation_state = 3;
                }

                if (ItemTracker.CurrentItemID == 1)
                {
                    spriteGameObject.GetComponent<PlayerAnimation>().animation_state = 4;
                }
            }
        }
        else
        {
            if (ItemTracker.CurrentItemID == 0)
            {
                spriteGameObject.GetComponent<PlayerAnimation>().animation_state = 5;
            }

            if (ItemTracker.CurrentItemID == 1)
            {
                spriteGameObject.GetComponent<PlayerAnimation>().animation_state = 6;
            }
        }

        //

        MouseWorldX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        if (MouseWorldX > theTransform.position.x)
        {
            isMouseRightOfPlayer = true;
        }
        else
        {
            isMouseRightOfPlayer = false;
        }
        
        if (attackTimer <= 0f)
        {
            playerDirection = Input.GetAxisRaw("Horizontal");
        }
        else
        {
            playerDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        }

        playerDirection = Mathf.Clamp(playerDirection, -1, 1);
        if (playerDirection != -1 && playerDirection != 1)
        {
            if (playerDirection < 0) playerDirection = -1;
            if (playerDirection > 0) playerDirection = 1;
        }
        if (playerDirection != 0) spriteGameObject.GetComponent<Scale>().flip = playerDirection;

        /*
        if (Input.GetKeyDown(KeyCode.Space) && currentState == 0)
        {
            
            if (IsGrounded())
            {
                theRigidbody.velocity = new Vector2(theRigidbody.velocity.x, playerMaxJumpHeight);

                spriteGameObject.GetComponent<Scale>().scale_x = 0.75f;
                spriteGameObject.GetComponent<Scale>().scale_y = 1.25f;
            }

        }
        */
        if (Input.GetMouseButtonDown(0) && currentState == 0 && IsGrounded())
        {
            if (ItemTracker.CurrentItemID == 0)
            {
                punchIndex = -punchIndex;

                if (isMouseRightOfPlayer)
                {
                    GameObject Attack = Instantiate(punch, new Vector2(transform.position.x + 1.1f, transform.position.y +1), Quaternion.identity);
                    attackTimer = 15;
                }
                else
                {
                    GameObject Attack = Instantiate(punch, new Vector2(transform.position.x - 1.1f, transform.position.y +1), Quaternion.identity);
                    attackTimer = 15;
                }
            }
            if (ItemTracker.CurrentItemID == 1)
            {
                if (isMouseRightOfPlayer)
                {
                    GameObject Attack = Instantiate(batSwing, new Vector2(transform.position.x + 1.6f, transform.position.y+1), Quaternion.identity);
                    attackTimer = 30;
                }
                else
                {
                    GameObject Attack = Instantiate(batSwing, new Vector2(transform.position.x - 1.6f, transform.position.y+1), Quaternion.identity);
                    attackTimer = 30;
                }
            }
            if (ItemTracker.CurrentItemID == 2)
            {
                if (isMouseRightOfPlayer)
                {
                    GameObject Attack = Instantiate(knifeSwing, new Vector2(transform.position.x + 1.1f, transform.position.y + 1), Quaternion.identity);
                    attackTimer = 15;
                }
                else
                {
                    GameObject Attack = Instantiate(knifeSwing, new Vector2(transform.position.x - 1.1f, transform.position.y + 1), Quaternion.identity);
                    attackTimer = 15;
                }
            }
            currentState = 1;
            
            //Attack.GetComponent<SlashCode>().creator = id;
            //Attack.GetComponent<SlashCode>().flip = flip;
        }
        /*
        if (currentState == 0 || currentState == 2)
        {
            if (Input.GetMouseButton(1) && IsGrounded())
            {
                currentState = 2;
                theSpriteRenderer.color = new Color(1, 0, 0);

            }
            else
            {
                currentState = 0;
                theSpriteRenderer.color = new Color(1, 1, 1);
            }
        }
        */
        if (Input.GetKeyDown(KeyCode.E) && currentState == 0 && IsGrounded() && ItemTracker.Delay == false)
        {
            if (ItemTracker.CurrentItemID == 1)
            {
                ItemTracker.CurrentItemID = 0;

                if (isMouseRightOfPlayer)
                {
                    GameObject ThrownItem = Instantiate(batThrow, new Vector2(transform.position.x + 1.6f, transform.position.y + 1), Quaternion.identity);
                    ThrownItem.GetComponent<BatThrow>().MovementDir = 1;
                }
                else
                {
                    GameObject ThrownItem = Instantiate(batThrow, new Vector2(transform.position.x - 1.6f, transform.position.y + 1), Quaternion.identity);
                    ThrownItem.GetComponent<BatThrow>().MovementDir = -1;
                }
            }
            if (ItemTracker.CurrentItemID == 2)
            {
                ItemTracker.CurrentItemID = 0;

                if (isMouseRightOfPlayer)
                {
                    GameObject ThrownItem = Instantiate(knifeThrow, new Vector2(transform.position.x + 1.6f, transform.position.y + 1), Quaternion.identity);
                    ThrownItem.GetComponent<KnifeThrow>().MovementDir = 1;
                }
                else
                {
                    GameObject ThrownItem = Instantiate(knifeThrow, new Vector2(transform.position.x - 1.6f, transform.position.y + 1), Quaternion.identity);
                    ThrownItem.GetComponent<KnifeThrow>().MovementDir = -1;
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if(IsGrounded())
        {
            if(isGrounded == false)
            {
                spriteGameObject.GetComponent<Scale>().scale_x = 1.25f;
                spriteGameObject.GetComponent<Scale>().scale_y = 0.75f;
            }

            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (attackTimer != 0)
        {
            attackTimer--;
        }
        if (attackTimer == 0 && currentState == 1)
        {
            currentState = 0;
        }
    }
    bool IsGrounded()
    {
        RaycastHit2D hitL = Physics2D.Raycast(new Vector2(theTransform.position.x - 0.6f, theTransform.position.y), Vector2.down, raycastLength, groundMask);
        RaycastHit2D hitC = Physics2D.Raycast(new Vector2(theTransform.position.x, theTransform.position.y), Vector2.down, raycastLength, groundMask);
        RaycastHit2D hitR = Physics2D.Raycast(new Vector2(theTransform.position.x + 0.6f, theTransform.position.y), Vector2.down, raycastLength, groundMask);

        if (hitL.collider != null || hitC.collider != null || hitR.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "EnemyAttack")
    //    {
    //        PlayerHealth--;
    //        if(PlayerHealth <= 0)
    //        {
    //            Destroy(gameObject);
    //        }
    //    }
    //
    public int BlockDamage(int Dmg)
    {
        if (currentState == 2)
        {
            if(ItemTracker.CurrentItemID == 0)
            {
                return Dmg-1;
            }
            else if(ItemTracker.CurrentItemID == 1)
            {
                ItemTracker.CurrentItemDurability --;
                if (ItemTracker.CurrentItemDurability <= 0)
                {
                    ItemTracker.CurrentItemID = 0;
                }
                return Dmg - 2;
            }
            else if(ItemTracker.CurrentItemID == 2)
            {
                ItemTracker.CurrentItemDurability--;
                if (ItemTracker.CurrentItemDurability <= 0)
                {
                    ItemTracker.CurrentItemID = 0;
                }
                return Dmg - 2;
            }
            else
            {
                return Dmg;
            }
        }
        else
        {
            return Dmg;
        }
        
    }
}