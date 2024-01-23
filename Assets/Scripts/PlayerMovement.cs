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
    public ParticleSystem dust;
    [SerializeField] GameObject punchObject;
    [SerializeField] GameObject batSwingObject;
    [SerializeField] GameObject batThrowObject;
    [SerializeField] GameObject knifeSwingObject;
    [SerializeField] GameObject knifeThrowObject;
    public Transform theTransform;
    public LayerMask groundMask;
    float raycastLength = 0.5f;
    [SerializeField] float playerSpeed; //Edit to change how fast the player moves
    [SerializeField] float playerJumpHeight; //Edit to change how high the player jumps
    bool mouseRightOfPlayer;
    int currentState;
    public int attackTimer;
    public static int playerHealth;
    [SerializeField] SpriteRenderer TheSR;
    bool grounded = false;
    float playerFaceDirection = 1;
    public int punchIndex = 1;

    //States:
    //0: Nothing specific, can move around
    //1: Currently Attacking
    //2: Currently Blocking

    // Start is called before the first frame update
    void Start()
    {
        theTransform = gameObject.GetComponent<Transform>();
        currentState = 0;
        playerHealth = 3;
    }

    // Update is called once per frame

    void Update()
    {


        if (playerHealth <= 0)
        {
            SceneManager.LoadScene("DeathScreen");
        }
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
        theRigidbody.velocity = new Vector2(speed * playerSpeed, theRigidbody.velocity.y);

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

        float MouseWorldX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        if (MouseWorldX > theTransform.position.x)
        {
            mouseRightOfPlayer = true;
        }
        else
        {
            mouseRightOfPlayer = false;
        }

        if (attackTimer <= 0f)
        {
            playerFaceDirection = Input.GetAxisRaw("Horizontal");
        }
        else
        {
            playerFaceDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        }
        playerFaceDirection = Mathf.Clamp(playerFaceDirection, -1, 1);
        if (playerFaceDirection != -1 && playerFaceDirection != 1)
        {
            if (playerFaceDirection < 0) playerFaceDirection = -1;
            if (playerFaceDirection > 0) playerFaceDirection = 1;
        }
        if (playerFaceDirection != 0) spriteGameObject.GetComponent<Scale>().flip = playerFaceDirection;

        if (Input.GetKeyDown(KeyCode.Space) && currentState == 0)
        {

            if (IsGrounded())
            {
                theRigidbody.velocity = new Vector2(theRigidbody.velocity.x, playerJumpHeight);

                spriteGameObject.GetComponent<Scale>().scale_x = 0.75f;
                spriteGameObject.GetComponent<Scale>().scale_y = 1.25f;
            }

        }

        if (Input.GetMouseButtonDown(0) && currentState == 0 && IsGrounded())
        {
            if (ItemTracker.CurrentItemID == 0)
            {
                punchIndex = -punchIndex;

                if (mouseRightOfPlayer)
                {
                    GameObject Attack = Instantiate(punchObject, new Vector2(transform.position.x + 1.1f, transform.position.y + 1), Quaternion.identity);
                    attackTimer = 15;
                }
                else
                {
                    GameObject Attack = Instantiate(punchObject, new Vector2(transform.position.x - 1.1f, transform.position.y + 1), Quaternion.identity);
                    attackTimer = 15;
                }
            }
            if (ItemTracker.CurrentItemID == 1)
            {
                if (mouseRightOfPlayer)
                {
                    GameObject Attack = Instantiate(batSwingObject, new Vector2(transform.position.x + 1.6f, transform.position.y + 1), Quaternion.identity);
                    attackTimer = 30;
                }
                else
                {
                    GameObject Attack = Instantiate(batSwingObject, new Vector2(transform.position.x - 1.6f, transform.position.y + 1), Quaternion.identity);
                    attackTimer = 30;
                }
            }
            if (ItemTracker.CurrentItemID == 2)
            {
                if (mouseRightOfPlayer)
                {
                    GameObject Attack = Instantiate(knifeSwingObject, new Vector2(transform.position.x + 1.1f, transform.position.y + 1), Quaternion.identity);
                    attackTimer = 15;
                }
                else
                {
                    GameObject Attack = Instantiate(knifeSwingObject, new Vector2(transform.position.x - 1.1f, transform.position.y + 1), Quaternion.identity);
                    attackTimer = 15;
                }
            }
            currentState = 1;

            //Attack.GetComponent<SlashCode>().creator = id;
            //Attack.GetComponent<SlashCode>().flip = flip;
        }

        if (currentState == 0 || currentState == 2)
        {
            if (Input.GetMouseButton(1) && IsGrounded())
            {
                currentState = 2;
                TheSR.color = new Color(1, 0, 0);

            }
            else
            {
                currentState = 0;
                TheSR.color = new Color(1, 1, 1);
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && currentState == 0 && IsGrounded() && ItemTracker.Delay == false)
        {
            if (ItemTracker.CurrentItemID == 1)
            {
                ItemTracker.CurrentItemID = 0;

                if (mouseRightOfPlayer)
                {
                    GameObject ThrownItem = Instantiate(batThrowObject, new Vector2(transform.position.x + 1.6f, transform.position.y + 1), Quaternion.identity);
                    ThrownItem.GetComponent<BatThrow>().MovementDir = 1;
                }
                else
                {
                    GameObject ThrownItem = Instantiate(batThrowObject, new Vector2(transform.position.x - 1.6f, transform.position.y + 1), Quaternion.identity);
                    ThrownItem.GetComponent<BatThrow>().MovementDir = -1;
                }
            }
            if (ItemTracker.CurrentItemID == 2)
            {
                ItemTracker.CurrentItemID = 0;

                if (mouseRightOfPlayer)
                {
                    GameObject ThrownItem = Instantiate(knifeThrowObject, new Vector2(transform.position.x + 1.6f, transform.position.y + 1), Quaternion.identity);
                    ThrownItem.GetComponent<KnifeThrow>().MovementDir = 1;
                }
                else
                {
                    GameObject ThrownItem = Instantiate(knifeThrowObject, new Vector2(transform.position.x - 1.6f, transform.position.y + 1), Quaternion.identity);
                    ThrownItem.GetComponent<KnifeThrow>().MovementDir = -1;
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (IsGrounded())
        {
            if (grounded == false)
            {
                spriteGameObject.GetComponent<Scale>().scale_x = 1.25f;
                spriteGameObject.GetComponent<Scale>().scale_y = 0.75f;
            }

            grounded = true;
        }
        else
        {
            grounded = false;
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
            if (ItemTracker.CurrentItemID == 0)
            {
                return Dmg - 1;
            }
            else if (ItemTracker.CurrentItemID == 1)
            {
                ItemTracker.CurrentItemDurability--;
                if (ItemTracker.CurrentItemDurability <= 0)
                {
                    ItemTracker.CurrentItemID = 0;
                }
                return Dmg - 2;
            }
            else if (ItemTracker.CurrentItemID == 2)
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