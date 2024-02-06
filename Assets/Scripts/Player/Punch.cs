using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Punch : MonoBehaviour
{
    float Timer = 15f;
    Transform TheT;
    RaycastHit2D[] HitEnemies;
    LayerMask EMask;

    [SerializeField] AudioSource hitSource;

    // Start is called before the first frame update
    void Start()
    {
        EMask = LayerMask.GetMask("Enemy");
        TheT = gameObject.GetComponent<Transform>();
        HitEnemies = Physics2D.BoxCastAll(TheT.position, new Vector2(1, 1), 0f, new Vector2(1, 0), 0f, EMask);
        for (int i = 0; i < HitEnemies.Length; i++)
        {
            HitEnemies[i].collider.gameObject.GetComponent<EnemyMovement>().hp--;
            
            HitEnemies[i].collider.gameObject.GetComponent<EnemyMovement>().flash = 10f;
            HitEnemies[i].collider.gameObject.GetComponent<EnemyMovement>().knockback = 10f;
            HitEnemies[i].collider.gameObject.GetComponent<EnemyMovement>().screen_shake.GetComponent<CameraController>().shake = 15f;
            Instantiate(HitEnemies[i].collider.gameObject.GetComponent<EnemyMovement>().blood, transform.position, Quaternion.identity);

            if (SceneManager.GetActiveScene().name == "Boss Level")
            {
                HitEnemies[i].collider.gameObject.GetComponent<BossMovement>().hp--;

                HitEnemies[i].collider.gameObject.GetComponent<BossMovement>().flash = 10f;
                HitEnemies[i].collider.gameObject.GetComponent<BossMovement>().knockback = 10f;
                HitEnemies[i].collider.gameObject.GetComponent<BossMovement>().screen_shake.GetComponent<CameraController>().shake = 15f;
                Instantiate(HitEnemies[i].collider.gameObject.GetComponent<BossMovement>().blood, transform.position, Quaternion.identity);
            }

            hitSource.Play();
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
