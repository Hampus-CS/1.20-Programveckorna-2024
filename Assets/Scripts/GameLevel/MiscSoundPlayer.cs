using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscSoundPlayer : MonoBehaviour
{
    [SerializeField] AudioSource wooshSource;
    [SerializeField] AudioSource batHitSource;
    [SerializeField] AudioSource batBreakSource;
    [SerializeField] AudioSource knifeHitSource;
    [SerializeField] AudioSource knifeBreakSource;

    int timer;

    void Start()
    {
        timer = 1000;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer--;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ThrowSound()
    {
        wooshSource.Play();
    }

    public void BatHitSound()
    {
        batHitSource.Play();
        batBreakSource.Play();
    }

    public void KnifeHitSound()
    {
        knifeHitSource.Play();
        knifeBreakSource.Play();
    }

    
}
