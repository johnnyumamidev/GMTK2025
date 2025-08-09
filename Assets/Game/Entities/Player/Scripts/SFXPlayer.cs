using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField] AudioSource audio;

    [SerializeField] AudioClip stepSFX;
    [SerializeField] AudioClip eatSFX;
    [SerializeField] AudioClip partFoundSFX;
    [SerializeField] AudioClip crystalSFX;
    [SerializeField] AudioClip hurtSFX;

    void OnEnable()
    {
        Events.Level.StartMove += PlayStepSFX;

        Events.Health.LoseHealth += PlayHurtSFX;

        Events.Health.EatFood += PlayEatSFX;
        Events.Health.GainCharge += PlayCrystalSFX;
        Events.Level.CollectedMissingPart += PlayPartFoundSFX;
    }
    private void OnDisable()
    {
        Events.Level.StartMove -= PlayStepSFX;

        Events.Health.LoseHealth -= PlayHurtSFX;

        Events.Health.EatFood -= PlayEatSFX;
        Events.Health.GainCharge -= PlayCrystalSFX;
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
    void PlayCrystalSFX(int i)
    {
        audio.PlayOneShot(crystalSFX);
    }
    void PlayHurtSFX()
    {
        audio.PlayOneShot(hurtSFX);
    }

    void PlayPartFoundSFX(MissingPart _m)
    {
        audio.PlayOneShot(partFoundSFX);
    }
}
