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
        
    }
}
