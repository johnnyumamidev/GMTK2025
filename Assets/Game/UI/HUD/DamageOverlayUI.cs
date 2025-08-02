using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverlayUI : MonoBehaviour
{
    public GameObject hurtScreenFX;
    public float fxDuration;

    private void OnEnable()
    {
        Events.Health.LoseHealth += EnableFX;
    }
    private void OnDisable()
    {
        Events.Health.LoseHealth += EnableFX;
    }

    void EnableFX()
    {
        hurtScreenFX.SetActive(true);
        Invoke("DisableFX", fxDuration);
    }
    void DisableFX()
    {
        hurtScreenFX.SetActive(false);
    }
}
