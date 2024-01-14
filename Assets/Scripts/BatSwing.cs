using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSwing : MonoBehaviour
{
    float Timer = 30f;
    Transform TheT;
    RaycastHit2D[] HitEnemies;
    LayerMask EMask;
    // Start is called before the first frame update
    void Start()
    {
        EMask = LayerMask.GetMask("Enemy");
        TheT = gameObject.GetComponent<Transform>();
        HitEnemies = Physics2D.BoxCastAll(TheT.position, new Vector2(2, 1), 0f, new Vector2(1, 0), 0f, EMask);
        for (int i = 0; i < HitEnemies.Length; i++)
        {
            HitEnemies[i].collider.gameObject.GetComponent<EnemyMovement>().hp -= 2;
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
}
