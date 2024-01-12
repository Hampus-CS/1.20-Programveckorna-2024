using System;
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
    LayerMask GMask;

    // Start is called before the first frame update
    void Start()
    {
        GMask = LayerMask.NameToLayer("Ground");
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
            Debug.Log(Physics.Raycast(new Vector3(id.position.x, id.position.y - 1, id.position.z), Vector3.down, 5f));
            Debug.Log(Physics.Raycast(new Vector3(id.position.x + 0.6f, id.position.y - 1, id.position.z), Vector3.down, 5f, GMask));
            Debug.Log(Physics.Raycast(new Vector3(id.position.x - 0.6f, id.position.y - 1, id.position.z), Vector3.down, 5f, GMask));
            
            if (Physics.Raycast(new Vector3(id.position.x, id.position.y - 1, id.position.z), Vector3.down, 0.2f, GMask) || Physics.Raycast(new Vector3(id.position.x + 0.6f, id.position.y - 1, id.position.z), Vector3.down, 0.2f, GMask) || Physics.Raycast(new Vector3(id.position.x - 0.6f, id.position.y - 1, id.position.z), Vector3.down, 0.2f, GMask))
            {
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