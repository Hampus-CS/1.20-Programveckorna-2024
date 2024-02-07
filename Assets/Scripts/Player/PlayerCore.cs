using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCore : MonoBehaviour
{
    Rigidbody2D theRigidbody;
    Transform theTransform;
    PlayerAttack thePlayerAttack;

    [SerializeField] public GameObject spriteGameObject;
    [SerializeField] public GameObject screenShake;
    [SerializeField] LayerMask groundMask;

    [SerializeField] public float playerMaxSpeed;
    [SerializeField] public float playerMaxJumpHeight;

    [SerializeField] AudioSource landSource;

    public int currentState;
    public static int playerHealth;
    bool isGrounded = false;
    public int punchIndex = 1;
    float MouseWorldX;
    public bool isMouseRightOfPlayer;
    float playerDirection;
    int landCooldown = 0; //Lazy band aid bug fix variable

    //States
    //0: Nothing specific, can move around
    //1: Currently Attacking
    //2: Currently Blocking

    // Start is called before the first frame update
    void Start()
    {
        theRigidbody = gameObject.GetComponent<Rigidbody2D>();
        theTransform = gameObject.GetComponent<Transform>();
        thePlayerAttack = gameObject.GetComponent<PlayerAttack>();

        currentState = 0;
        playerHealth = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0)
        {
            SceneManager.LoadScene("DeathScreen");
        }

        MouseWorldX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        if (MouseWorldX > theTransform.position.x)
        {
            isMouseRightOfPlayer = true;
        }
        else
        {
            isMouseRightOfPlayer = false;
        }

        if (thePlayerAttack.attackTimer <= 0f)
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

    }

    private void FixedUpdate()
    {
        landCooldown--;
        if (IsGrounded())
        {
            if (isGrounded == false)
            {
                if(landCooldown <= 0)
                {
                    spriteGameObject.GetComponent<Scale>().scale_x = 1.25f;
                    spriteGameObject.GetComponent<Scale>().scale_y = 0.75f;
                    landSource.Play();
                    landCooldown = 10;
                }
            }

            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (thePlayerAttack.attackTimer != 0)
        {
            thePlayerAttack.attackTimer--;
        }
        if (thePlayerAttack.attackTimer == 0 && currentState == 1)
        {
            currentState = 0;
        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D hitL = Physics2D.Raycast(new Vector2(theTransform.position.x - 0.6f, theTransform.position.y), Vector2.down, 0.5f, groundMask);
        RaycastHit2D hitC = Physics2D.Raycast(new Vector2(theTransform.position.x, theTransform.position.y), Vector2.down, 0.5f, groundMask);
        RaycastHit2D hitR = Physics2D.Raycast(new Vector2(theTransform.position.x + 0.6f, theTransform.position.y), Vector2.down, 0.5f, groundMask);

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
