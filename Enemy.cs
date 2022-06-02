using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int enemyHitPoints = 1;
    [SerializeField] ParticleSystem explosionFX;

    ParticleSystem particleSystem;

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if(enemyHitPoints < 1)
        {
            KillEnemy();
        }
    }

    void ProcessHit()
    {
        enemyHitPoints--;
    }

    void KillEnemy()
    {
        explosionFX.Play();
        GetComponent<MeshRenderer>().enabled = false;
    }
}
