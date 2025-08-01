using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Events
{
    public static HealthEvents Health = new();
    public static LevelEvents Level = new();
}

public class HealthEvents
{
    public UnityAction<int> IntializeHealth;
    public UnityAction<int> UpdateHealth;
    public UnityAction<int> GainHealth;
    public UnityAction<int> LoseHealth;
    public UnityAction AllHealthLost;
}

public class LevelEvents
{
    public UnityAction PathDrawnIsClosedLoop;
    public UnityAction PathDrawnIsOpen;
    public UnityAction GridGenerated;
    public UnityAction<List<Vector3Int>> ItemsGenerated;
    public UnityAction Reset;
    public UnityAction StartLoop;
    public UnityAction<Vector3Int> ReachedNextTile;
    public UnityAction LoopComplete;
}
