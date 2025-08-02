using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DaylightSystem : MonoBehaviour
{
    public int startingTimePoints;
    public int timePoints;
    [SerializeField] Light2D worldLight;

    [SerializeField] Color dayColor, nightColor;

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

        daylightUI.UpdateTime(startingTimePoints, timePoints);
    }
    void DecreaseTime(Vector3Int n)
    {
        timePoints--;
        if (timePoints <= 0)
        {
            Events.Level.StartNight?.Invoke();

            worldLight.color = nightColor;
        }
        daylightUI.UpdateTime(startingTimePoints, timePoints);
    }

    void ResetTime()
    {
        worldLight.color = dayColor;
        timePoints = startingTimePoints;
        daylightUI.UpdateTime(startingTimePoints, timePoints);
    }
}
