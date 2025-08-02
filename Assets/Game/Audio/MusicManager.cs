using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource musicPlayer;

    public void RestartMusic()
    {
        musicPlayer.Stop();
        musicPlayer.Play();
    }
}
