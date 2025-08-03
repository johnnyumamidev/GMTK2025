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
    }
    private void OnDisable()
    {
        Events.Level.StartLoop -= PlayRun;
        Events.Level.LoopComplete -=PlayIdle;
    }

    void PlayIdle()
    {
        animator.CrossFade("Idle", 0, 0);
    }

    void PlayRun()
    {
        animator.CrossFade("Run", 0, 0);
    }
}
