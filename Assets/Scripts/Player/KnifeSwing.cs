using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSwing : MonoBehaviour
{
    float Timer = 15f;
    Transform TheT;
    RaycastHit2D[] HitEnemies;
    LayerMask EMask;

    [SerializeField] AudioSource hitSource;
    [SerializeField] AudioSource breakSource;
    // Start is called before the first frame update
    void Start()
    {
        EMask = LayerMask.GetMask("Enemy");
        TheT = gameObject.GetComponent<Transform>();
        HitEnemies = Physics2D.BoxCastAll(TheT.position, new Vector2(1, 1), 0f, new Vector2(1, 0), 0f, EMask);
        for (int i = 0; i < HitEnemies.Length; i++)
        {
            hitSource.Play();
            breakSource.Play();

            HitEnemies[i].collider.gameObject.GetComponent<EnemyMovement>().hp-=3;

            HitEnemies[i].collider.gameObject.GetComponent<EnemyMovement>().flash = 10f;
            HitEnemies[i].collider.gameObject.GetComponent<EnemyMovement>().knockback = 10f;
            HitEnemies[i].collider.gameObject.GetComponent<EnemyMovement>().screen_shake.GetComponent<CameraController>().shake = 15f;
            Instantiate(HitEnemies[i].collider.gameObject.GetComponent<EnemyMovement>().blood, transform.position, Quaternion.identity);

            ItemTracker.CurrentItemID = 0;
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
