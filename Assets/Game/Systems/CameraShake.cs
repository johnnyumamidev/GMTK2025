using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    CinemachineVirtualCamera cmCam;
    CinemachineBasicMultiChannelPerlin cmPerlin;
    public float shakeIntensity;
    public float shakeLength;
    void Awake()
    {
        cmCam = GetComponent<CinemachineVirtualCamera>();
        cmPerlin = cmCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    void OnEnable()
    {
        Events.Health.LoseHealth += ShakeCamera;
    }
    void OnDisable()
    {
        Events.Health.LoseHealth -= ShakeCamera;
    }

    void ShakeCamera()
    {
        cmPerlin.m_AmplitudeGain = shakeIntensity;
        Invoke("StopShake", shakeLength);
    }

    void StopShake()
    {
        cmPerlin.m_AmplitudeGain = 0f;
    }
}
