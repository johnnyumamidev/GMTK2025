using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField] AudioSource audio;

    [SerializeField] AudioClip stepSFX;
    [SerializeField] AudioClip eatSFX;
    [SerializeField] AudioClip partFoundSFX;

    void OnEnable()
    {
        Events.Level.StartMove += PlayStepSFX;

        Events.Health.EatFood += PlayEatSFX;
        Events.Level.CollectedMissingPart += PlayPartFoundSFX;
    }
    private void OnDisable()
    {
        Events.Level.StartMove -= PlayStepSFX;

        Events.Health.EatFood -= PlayEatSFX;
        Events.Level.CollectedMissingPart -= PlayPartFoundSFX;
    }

    void PlayStepSFX()
    {
        audio.PlayOneShot(stepSFX);
    }
    void PlayEatSFX(int i)
    {
        audio.PlayOneShot(eatSFX);
    }

    void PlayPartFoundSFX(MissingPart _m)
    {
        audio.PlayOneShot(partFoundSFX);
    }
}
