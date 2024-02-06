using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class KnifeThrow : MonoBehaviour
{
    public int MovementDir;
    [SerializeField] float MoveSpeed;
    Rigidbody2D TheRB;
    LayerMask EMask;
    LayerMask GMask;
    int Timer;
    Transform TheT;
    RaycastHit2D[] HitEnemies;
    RaycastHit2D[] HitGrounds;
    SpriteRenderer TheSR;

    [SerializeField] GameObject soundPlayer;

    // Start is called before the first frame update

    void Start()
    {
        TheSR = gameObject.GetComponent<SpriteRenderer>();
        TheRB = gameObject.GetComponent<Rigidbody2D>();
        Instantiate(soundPlayer, new Vector3(0, 0, 0), quaternion.identity).GetComponent<MiscSoundPlayer>().ThrowSound();

        //soundPlayer.ThrowSound();
        Timer = 200;
        EMask = LayerMask.GetMask("Enemy");
        GMask = LayerMask.GetMask("Ground");
        TheT = gameObject.GetComponent<Transform>();
        //HitEnemies = Physics2D.BoxCastAll(TheT.position, new Vector2(1, 1), 0f, new Vector2(1, 0), 0f, EMask);
        if (MovementDir == -1)
        {
            TheSR.flipX = true;
        }
        
        /*
        for (int i = 0; i < HitEnemies.Length; i++)
        {
            HitEnemies[i].collider.gameObject.GetComponent<EnemyMovement>().hp -= 2;
            HitEnemies[i].collider.gameObject.GetComponent<EnemyMovement>().flash = 10f;
            HitEnemies[i].collider.gameObject.GetComponent<EnemyMovement>().knockback = 10f;
            HitEnemies[i].collider.gameObject.GetComponent<EnemyMovement>().screen_shake.GetComponent<CameraController>().shake = 15f;
            Instantiate(HitEnemies[i].collider.gameObject.GetComponent<EnemyMovement>().blood, transform.position, Quaternion.identity);
        }
        */
    }

    private void FixedUpdate()
    {
        TheRB.velocity = new Vector2(MovementDir * MoveSpeed, 0);
        Timer--;
        if (Timer <= 0f) Destroy(gameObject);
        HitEnemies = Physics2D.BoxCastAll(TheT.position, new Vector2(2, 1), 0f, new Vector2(1, 0), 0f, EMask);
        for (int i = 0; i < HitEnemies.Length; i++)
        {
            HitEnemies[i].collider.gameObject.GetComponent<EnemyMovement>().hp -= 2;
            HitEnemies[i].collider.gameObject.GetComponent<EnemyMovement>().flash = 10f;
            HitEnemies[i].collider.gameObject.GetComponent<EnemyMovement>().knockback = 10f;
            //HitEnemies[i].collider.gameObject.GetComponent<EnemyMovement>().screen_shake.GetComponent<CameraController>().shake = 15f;
            Instantiate(HitEnemies[i].collider.gameObject.GetComponent<EnemyMovement>().blood, transform.position, Quaternion.identity);
            Instantiate(soundPlayer, new Vector3(0, 0, 0), quaternion.identity).GetComponent<MiscSoundPlayer>().KnifeHitSound();
            Destroy(gameObject);
        }
        HitGrounds = Physics2D.BoxCastAll(TheT.position, new Vector2(2, 1), 0f, new Vector2(1, 0), 0f, GMask);
        for (int i = 0; i < HitGrounds.Length; i++)
        {
            Destroy(gameObject);
        }

    }
}
