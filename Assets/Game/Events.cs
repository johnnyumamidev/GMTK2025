using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Events
{
    public static HealthEvents Health = new();
    public static LevelEvents Level = new();
    public static CombatEvents Combat = new();
}

public class HealthEvents
{
    // Hit points
    public UnityAction<int> IntializeHealth;
    public UnityAction<int> UpdateHealth;
    public UnityAction<int> HealthChanged;
    public UnityAction GainHealth;
    public UnityAction LoseHealth;
    public UnityAction AllHealthLost;

    // Hunger
    public UnityAction<int> EatFood;
    public UnityAction<float, float> HungerChanged;
}

public class LevelEvents
{
    public UnityAction PathDrawnIsClosedLoop;
    public UnityAction PathDrawnIsOpen;
    public UnityAction GridGenerated;
    public UnityAction<List<Vector3Int>> ItemsGenerated;
    public UnityAction Reset;
    public UnityAction StartLoop;
    public UnityAction StartMove;
    public UnityAction<Vector3Int> ReachedNextTile;
    public UnityAction LoopComplete;
    public UnityAction CollectedItem;

    public UnityAction<MissingPart> CollectedMissingPart;
    public UnityAction<int> MissingPartsGenerated;
    public UnityAction StartNight;

    public UnityAction<Vector3Int> NeighborDetected;
}

public class CombatEvents
{
    public UnityAction EnemyHit;
}
