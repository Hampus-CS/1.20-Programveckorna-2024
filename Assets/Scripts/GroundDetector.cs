using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] PlayerMovement PlayerMovementScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered"+collision.gameObject.layer);
        if (collision.gameObject.layer == 5)
        {
            PlayerMovementScript.Grounds++;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Left" + collision.gameObject.layer);
        if (collision.gameObject.layer == 5)
        {
            PlayerMovementScript.Grounds--;
        }
        
    }
}
