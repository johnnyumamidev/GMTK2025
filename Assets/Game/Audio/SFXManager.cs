using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    public AudioSource sfxSource;
    public AudioClip enemyDieSFX;
    void Awake()
    {
        instance = this;
    }
    private void OnEnable() {
        Events.Combat.EnemyHit += PlayEnemyDieSFX;
    }
    private void OnDisable() {
        Events.Combat.EnemyHit -= PlayEnemyDieSFX;
    }

    public void PlayEnemyDieSFX()
    {
        sfxSource.PlayOneShot(enemyDieSFX);
    }
}
