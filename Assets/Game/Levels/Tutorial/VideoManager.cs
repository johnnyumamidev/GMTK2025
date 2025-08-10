using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class VideoManager : MonoBehaviour
{
    public VideoPlayer[] videos;

    void Awake()
    {
        videos = GetComponentsInChildren<VideoPlayer>(true);
    }
    void Start()
    {
        foreach (VideoPlayer video in videos)
        {
            video.url = System.IO.Path.Combine (Application.streamingAssetsPath, video.gameObject.name + ".mp4");
            video.gameObject.SetActive(true);
            video.Play();
            Debug.Log(video + "/" + video.isPlaying);
        }
    }
}
