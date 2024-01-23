using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBatSwing : MonoBehaviour
{
    Transform TheT;
    float Timer = 50f;
    RaycastHit2D[] HitPlayer;
    LayerMask PMask;
    // Start is called before the first frame update
    void Start()
    {
        PMask = LayerMask.GetMask("Player");
        TheT = gameObject.GetComponent<Transform>();
        HitPlayer = Physics2D.BoxCastAll(TheT.position, new Vector2(2, 1), 0f, new Vector2(1, 0), 0f, PMask);
        for (int i = 0; i < HitPlayer.Length; i++)
        {
            PlayerCore.playerHealth -= ModDamage(2);
            HitPlayer[0].collider.gameObject.GetComponent<PlayerCore>().screenShake.GetComponent<CameraController>().shake = 50f;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        Timer--;
        if (Timer <= 0f) Destroy(gameObject);
    }
    private int ModDamage(int Dmg)
    {
        int Damage = Dmg;
        Damage = HitPlayer[0].collider.gameObject.GetComponent<PlayerMovement>().BlockDamage(Damage);
        HitPlayer[0].collider.gameObject.GetComponent<PlayerMovement>().screenShake.GetComponent<CameraController>().shake = 50f;
        if (Damage < 0)
        {
            Damage = 0;
        }

        return Damage;
    }
}
