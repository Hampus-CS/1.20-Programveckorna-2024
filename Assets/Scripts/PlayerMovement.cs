using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public LayerMask mask;
    public GameObject sprite;
    float speed = 0f;
    public ParticleSystem dust;
    public GameObject slash;
    [SerializeField] GameObject Punch;
    public Transform id;
    public GameObject screen_shake;
    float flip;
    public int Grounds = 0;
    public LayerMask GMask;
    float raycastLength = 0.5f;
    Transform TheT;
    [SerializeField] float PlayerSpeed;
    [SerializeField] float PlayerJumpHeight;
    bool MouseRightOfPlayer;
    int State;
    int PunchTimer;
    //States:
    //0: Nothing specific, can move around
    //1: Currently Punching

    // Start is called before the first frame update
    void Start()
    {
        //GMask = LayerMask.NameToLayer("Ground");
        TheT = gameObject.GetComponent<Transform>();
        State = 0;
    }

    // Update is called once per frame
    void Update()
    {
        speed = 0;
        if (Input.GetKey(KeyCode.D) && State == 0)
        {
            speed++;
        }
        if (Input.GetKey(KeyCode.A) && State == 0)
        {
            speed--;
        }
        rb.velocity = new Vector2(speed*PlayerSpeed, rb.velocity.y);

        //speed += ((Input.GetAxisRaw("Horizontal") * 4f) - speed) * 0.035f;

        float MouseWorldX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        if (MouseWorldX > TheT.position.x)
        {
            MouseRightOfPlayer = true;
        }
        else
        {
            MouseRightOfPlayer = false;
        }
        //mouse_side = Mathf.Clamp(mouse_side, -1, 1);
        //if (mouse_side != -1 && mouse_side != 1) mouse_side = 1;
        //if (mouse_side != 0) flip = mouse_side;

        if (Input.GetKeyDown(KeyCode.Space) && State == 0)
        {
            
            if (IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, PlayerJumpHeight);
            }

        }

        if (Input.GetMouseButtonDown(0) && State == 0 && IsGrounded())
        {
            
            if(MouseRightOfPlayer)
            {
                GameObject Attack = Instantiate(Punch, new Vector2(transform.position.x + 0.6f, transform.position.y), Quaternion.identity);
            }
            else
            {
                GameObject Attack = Instantiate(Punch, new Vector2(transform.position.x - 0.6f, transform.position.y), Quaternion.identity);
            }
            
            State = 1;
            PunchTimer = 15;
            //Attack.GetComponent<SlashCode>().creator = id;
            //Attack.GetComponent<SlashCode>().flip = flip;
        }
    }
    private void FixedUpdate()
    {
        if (PunchTimer != 0)
        {
            PunchTimer--;
        }
        if (PunchTimer == 0 && State == 1)
        {
            State = 0;
        }
    }
    bool IsGrounded()
    {
        RaycastHit2D hitL = Physics2D.Raycast(new Vector2(TheT.position.x - 0.6f, TheT.position.y - 1), Vector2.down, raycastLength, GMask);
        RaycastHit2D hitC = Physics2D.Raycast(new Vector2(TheT.position.x, TheT.position.y - 1), Vector2.down, raycastLength, GMask);
        RaycastHit2D hitR = Physics2D.Raycast(new Vector2(TheT.position.x + 0.6f, TheT.position.y - 1), Vector2.down, raycastLength, GMask);

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