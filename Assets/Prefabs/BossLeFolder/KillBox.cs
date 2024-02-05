using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject screenShake;

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collsiion)
    {
        if (collsiion.tag == "Player")
        {
            PlayerCore.playerHealth -= 2;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 35f);
            screenShake.GetComponent<CameraController>().shake = 50f;
        }
    }

    private void Update()
    {
        if (screenShake.GetComponent<CameraController>().shake < 1f) screenShake.GetComponent<CameraController>().shake = 1f;
    }
}
