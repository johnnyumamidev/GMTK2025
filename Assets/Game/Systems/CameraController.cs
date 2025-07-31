using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject worldCam;
    [SerializeField] GameObject playerFollowCam;

    void OnEnable()
    {
        Events.Level.StartLoop += ShiftToPlayer;
        Events.Level.LoopComplete += ShiftToWorld;
    }
    void OnDisable()
    {
        Events.Level.StartLoop -= ShiftToPlayer;
        Events.Level.LoopComplete -= ShiftToWorld;
    }

    void ShiftToPlayer()
    {
        playerFollowCam.SetActive(true);
    }
    void ShiftToWorld()
    {
        playerFollowCam.SetActive(false);
    }
}
