using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    PlayerCore thePlayerCore;
    Transform theTransform;
    public Rigidbody2D theRigidbody;
    PlayerAttack thePlayerAttack;

    [SerializeField] AudioSource jumpSource;
    [SerializeField] AudioSource stepSource1;
    [SerializeField] AudioSource stepSource2;
    [SerializeField] AudioSource stepSource3;
    [SerializeField] AudioSource stepSource4;
    [SerializeField] AudioSource stepSource5;

    int stepSoundCooldown = 0;
    float speed = 0f;
    int selectedSound = 0;
    // Start is called before the first frame update
    void Start()
    {
        thePlayerCore = gameObject.GetComponent<PlayerCore>();
        theTransform = gameObject.GetComponent<Transform>();
        theRigidbody = gameObject.GetComponent<Rigidbody2D>();
        thePlayerAttack = gameObject.GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (thePlayerAttack.attackTimer <= 0f && thePlayerCore.currentState != 2)
        if (thePlayerCore.currentState == 0)
        {
            speed += ((Input.GetAxisRaw("Horizontal") * 1f) - speed) * 0.035f;
        }    
        else
        {
            speed += (0 - speed) * 0.1f;
        }
        theRigidbody.velocity = new Vector2(speed * thePlayerCore.playerMaxSpeed, theRigidbody.velocity.y);
        
        if (Input.GetKeyDown(KeyCode.Space) && thePlayerCore.currentState == 0)
        {

            if (thePlayerCore.IsGrounded())
            {
                theRigidbody.velocity = new Vector2(theRigidbody.velocity.x, thePlayerCore.playerMaxJumpHeight);

                thePlayerCore.spriteGameObject.GetComponent<Scale>().scale_x = 0.75f;
                thePlayerCore.spriteGameObject.GetComponent<Scale>().scale_y = 1.25f;

                jumpSource.Play();
            }

        }
    }
    private void FixedUpdate()
    {
        stepSoundCooldown--;
        if (Input.GetAxisRaw("Horizontal") != 0f && stepSoundCooldown <= 0 && thePlayerCore.IsGrounded())
        {
            stepSoundCooldown = 15;

            selectedSound = Random.Range(1,5);
            if(selectedSound == 1)
            {
                stepSource1.Play();
            }
            if (selectedSound == 2)
            {
                stepSource2.Play();
            }
            if (selectedSound == 3)
            {
                stepSource3.Play();
            }
            if (selectedSound == 4)
            {
                stepSource4.Play();
            }
            if (selectedSound == 5)
            {
                stepSource5.Play();
            }
        }
    }
}
