using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField] AudioSource audio;

    [SerializeField] AudioClip stepSFX;
    [SerializeField] AudioClip eatSFX;

    void OnEnable()
    {
        Events.Level.StartMove += PlayStepSFX;

        Events.Health.EatFood += PlayEatSFX;
    }
    private void OnDisable()
    {
        Events.Level.StartMove -= PlayStepSFX;

        Events.Health.EatFood -= PlayEatSFX;
    }

    void PlayStepSFX()
    {
        audio.PlayOneShot(stepSFX);
    }
    void PlayEatSFX(int i)
    {
        audio.PlayOneShot(eatSFX);
    }
}
