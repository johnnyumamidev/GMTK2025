using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXEnemy : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip stepSFX;
    [SerializeField] AudioClip dieSFX;

    public void PlayStepSFX()
    {
        audio.PlayOneShot(stepSFX);
    }
    public void PlayDieSFX()
    {
        audio.PlayOneShot(dieSFX);
    }
}
