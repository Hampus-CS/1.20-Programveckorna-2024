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
    public Transform id;
    public GameObject screen_shake;
    float flip;
    public int Grounds = 0;
    public LayerMask GMask;
    [SerializeField,Range(0, 5)]
    float raycastLength = 0.5f;
    Transform TheT;

    // Start is called before the first frame update
    void Start()
    {
        //GMask = LayerMask.NameToLayer("Ground");
        TheT = gameObject.GetComponent<Transform>();
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

       


        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log(Physics.Raycast(new Vector2(TheT.position.x, TheT.position.y - 1), Vector2.down, 5f, GMask));
            //Debug.Log(Physics.Raycast(new Vector2(TheT.position.x + 0.6f, TheT.position.y - 1), Vector2.down, 5f, GMask));
            //Debug.Log(Physics.Raycast(new Vector2(TheT.position.x - 0.6f, TheT.position.y - 1), Vector2.down, 5f, GMask));
         

            RaycastHit2D hitL = Physics2D.Raycast(new Vector2(TheT.position.x - 0.6f, TheT.position.y - 1), Vector2.down, raycastLength, GMask);
            RaycastHit2D hitC = Physics2D.Raycast(new Vector2(TheT.position.x, TheT.position.y - 1), Vector2.down, raycastLength, GMask);
            RaycastHit2D hitR = Physics2D.Raycast(new Vector2(TheT.position.x + 0.6f, TheT.position.y - 1), Vector2.down, raycastLength, GMask);

            
            if (hitL.collider != null || hitC.collider != null || hitR.collider != null)
            {
                Debug.Log(hitL.collider.gameObject.name);
                //Debug.Log(hitL.collider.gameObject.name);
                print("hopp");
                rb.velocity = new Vector2(rb.velocity.x, 15f);
            }

        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject Attack = Instantiate(slash, transform.position, Quaternion.identity);
            Attack.GetComponent<SlashCode>().creator = id;
            Attack.GetComponent<SlashCode>().flip = flip;
        }
    }
}