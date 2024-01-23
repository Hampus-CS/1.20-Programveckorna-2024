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
        }
    }
}
