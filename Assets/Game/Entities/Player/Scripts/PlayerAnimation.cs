using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    private void OnEnable()
    {
        Events.Level.StartLoop += PlayRun;
        Events.Level.LoopComplete += PlayIdle;

        Events.Level.InitiateTeleport += PlayTeleport;
    }
    private void OnDisable()
    {
        Events.Level.StartLoop -= PlayRun;
        Events.Level.LoopComplete -= PlayIdle;

        Events.Level.InitiateTeleport -= PlayTeleport;
    }

    void PlayIdle()
    {
        animator.CrossFade("Idle", 0, 0);
    }

    void PlayRun()
    {
        animator.CrossFade("Run", 0, 0);
    }

    void PlayTeleport()
    {
        animator.CrossFade("Teleport", 0, 0);
    }
}
