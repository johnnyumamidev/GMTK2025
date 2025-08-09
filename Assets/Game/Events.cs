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
    public UnityAction<int> AllHealthLost;

    // Hunger
    public UnityAction<int> EatFood;
    public UnityAction<float, float> HungerChanged;

    // Teleporter Charge
    public UnityAction<int> GainCharge;
    public UnityAction<int> ChargesChanged;
}

public class LevelEvents
{
    public UnityAction GridGenerated;
    public UnityAction<int> MissingPartsGenerated;
    public UnityAction<List<Vector3Int>> ItemsGenerated;

    public UnityAction Undo;
    public UnityAction Reset;
    public UnityAction NotEnoughCharges;
    public UnityAction TeleportMode;
    public UnityAction EndTeleportMode;
    public UnityAction InitiateTeleport;

    public UnityAction PathDrawnIsClosedLoop;
    public UnityAction PathDrawnIsOpen;
    public UnityAction StartLoop;
    public UnityAction StartMove;
    public UnityAction<Vector3Int> ReachedNextTile;
    public UnityAction<Vector2> CollectedItem;
    public UnityAction<MissingPart> CollectedMissingPart;
    public UnityAction StartNight;
    public UnityAction LoopComplete;

    public UnityAction<Vector3Int> NeighborDetected;
}

public class CombatEvents
{
    public UnityAction EnemyHit;
}
