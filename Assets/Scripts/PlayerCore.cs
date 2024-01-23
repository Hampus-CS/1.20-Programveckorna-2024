using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    public Rigidbody2D theRigidbody;
    public GameObject spriteGameObject;
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
    public int currentState;
    public int attackTimer;
    public static int playerHealth;
    [SerializeField] SpriteRenderer TheSR;
    bool grounded = false;
    float playerFaceDirection = 1;
    public int punchIndex = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
