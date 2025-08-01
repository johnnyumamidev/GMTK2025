using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaylightSystem : MonoBehaviour
{
    public int startingTimePoints;
    public int timePoints;
    public DaylightUI daylightUI;
    void OnEnable()
    {
        Events.Level.ReachedNextTile += DecreaseTime;
        Events.Level.LoopComplete += ResetTime;
    }
    void OnDisable()
    {
        Events.Level.ReachedNextTile -= DecreaseTime;
     
        Events.Level.LoopComplete -= ResetTime;
    }
    void Start()
    {
        ResetTime();

        daylightUI.UpdateTime(timePoints);
    }
    void DecreaseTime(Vector3Int n)
    {
        timePoints--;
        if (timePoints <= 0)
        {
            Events.Level.StartNight?.Invoke();
        }
        daylightUI.UpdateTime(timePoints);
    }

    void ResetTime()
    {
        timePoints = startingTimePoints;
        daylightUI.UpdateTime(timePoints);
    }
}
