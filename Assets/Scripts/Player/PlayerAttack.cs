using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerCore thePlayerCore;
    Transform theTransform;
    Rigidbody2D theRigidbody;

    [SerializeField] GameObject punch;
    [SerializeField] GameObject batSwing;
    [SerializeField] GameObject batThrow;
    [SerializeField] GameObject knifeSwing;
    [SerializeField] GameObject knifeThrow;
    [SerializeField] AudioSource attackSource;

    public int attackTimer;

    void Start()
    {
        thePlayerCore = gameObject.GetComponent<PlayerCore>();
        theTransform = gameObject.GetComponent<Transform>();
        theRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && thePlayerCore.currentState == 0 && thePlayerCore.IsGrounded())
        {
            if (ItemTracker.CurrentItemID == 0)
            {
                thePlayerCore.punchIndex = -thePlayerCore.punchIndex;

                if (thePlayerCore.isMouseRightOfPlayer)
                {
                    GameObject Attack = Instantiate(punch, new Vector2(transform.position.x + 1.1f, transform.position.y + 1), Quaternion.identity);
                    attackTimer = 15;
                }
                else
                {
                    GameObject Attack = Instantiate(punch, new Vector2(transform.position.x - 1.1f, transform.position.y + 1), Quaternion.identity);
                    attackTimer = 15;
                }
            }
            if (ItemTracker.CurrentItemID == 1)
            {
                if (thePlayerCore.isMouseRightOfPlayer)
                {
                    GameObject Attack = Instantiate(batSwing, new Vector2(transform.position.x + 1.6f, transform.position.y + 1), Quaternion.identity);
                    attackTimer = 30;
                }
                else
                {
                    GameObject Attack = Instantiate(batSwing, new Vector2(transform.position.x - 1.6f, transform.position.y + 1), Quaternion.identity);
                    attackTimer = 30;
                }
            }
            if (ItemTracker.CurrentItemID == 2)
            {
                if (thePlayerCore.isMouseRightOfPlayer)
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
            thePlayerCore.currentState = 1;
            attackSource.Play();
        }




        if (Input.GetKeyDown(KeyCode.E) && thePlayerCore.currentState == 0 && thePlayerCore.IsGrounded() && ItemTracker.Delay == false)
        {
            if (ItemTracker.CurrentItemID == 1)
            {
                ItemTracker.CurrentItemID = 0;

                if (thePlayerCore.isMouseRightOfPlayer)
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

                if (thePlayerCore.isMouseRightOfPlayer)
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
}
