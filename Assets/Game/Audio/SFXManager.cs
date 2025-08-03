using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    public AudioSource sfxSource;
    public AudioClip enemyDieSFX;
    public AudioClip menuSFX;
    public AudioClip drawPathSFX;
    public AudioClip undoSFX;
    public AudioClip cancelPathSFX;
    public AudioClip buttonEnterSFX;
    public AudioClip transitionSFX;
    public AudioClip nightfallSFX;
    void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        Events.Combat.EnemyHit += PlayEnemyDieSFX;
        Events.Level.StartNight += PlayNightFallSFX;
    }
    private void OnDisable()
    {
        Events.Combat.EnemyHit -= PlayEnemyDieSFX;
        Events.Level.StartNight -= PlayNightFallSFX;
    }

    public void PlayEnemyDieSFX()
    {
        sfxSource.PlayOneShot(enemyDieSFX);
    }
    public void PlayMenuSFX()
    {
        sfxSource.PlayOneShot(menuSFX);
    }
    public void PlayPathDrawSFX()
    {
        sfxSource.PlayOneShot(drawPathSFX);
    }
    public void CancelPathSFX()
    {
        sfxSource.PlayOneShot(cancelPathSFX);
    }

    public void PlayButtonEnterSFX()
    {
        sfxSource.PlayOneShot(buttonEnterSFX);
    }

    public void PlayTransitionSFX()
    {
        sfxSource.PlayOneShot(transitionSFX);
    }

    public void PlayUndoSFX()
    {
        sfxSource.PlayOneShot(undoSFX);
    }
    
    public void PlayNightFallSFX()
    {
        sfxSource.PlayOneShot(nightfallSFX);
    }
}
