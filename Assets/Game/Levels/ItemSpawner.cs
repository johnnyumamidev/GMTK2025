using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;

    [SerializeField] Transform goalItem;
    [SerializeField] Transform foodPrefab;
    [SerializeField] Transform crystalPrefab;
    [SerializeField] int goalItemSpawnCount = 0;
    [SerializeField] int foodSpawnCount = 0;
    [SerializeField] int crystalSpawnCount = 0;
    [SerializeField] int minDistanceFromStartTile = 4;
    List<Vector3Int> possibleSpawnPositions;
    void OnEnable()
    {
        Events.Level.GridGenerated += SpawnGoalItems;

        Events.Level.LoopComplete += SpawnFoodItems;

        Events.Level.CollectedItem += UpdateSpawnPoints;
    }
    void OnDisable()
    {
        Events.Level.GridGenerated -= SpawnGoalItems;

        Events.Level.LoopComplete -= SpawnFoodItems;

        Events.Level.CollectedItem -= UpdateSpawnPoints;
    }
    void Start()
    {
        Events.Level.MissingPartsGenerated?.Invoke(goalItemSpawnCount);
    }

    void SpawnGoalItems()
    {
        // make goal collectibles spawn reasonably far away from start position 
        possibleSpawnPositions = new(levelManager.GetPlayableTiles());

        List<Vector3Int> tooCloseToStartSpawns = new();

        foreach (Vector3Int spawnPoint in possibleSpawnPositions)
        {
            if (Vector3Int.Distance(spawnPoint, levelManager.GetStartingTilePos()) < minDistanceFromStartTile)
            {
                tooCloseToStartSpawns.Add(spawnPoint);
            }
        }

        foreach (Vector3Int spawnPoint in tooCloseToStartSpawns)
        {
            possibleSpawnPositions.Remove(spawnPoint);
        }

        // spawn goal items
        for (int i = 0; i < goalItemSpawnCount; i++)
        {
            Vector3Int randomSpawnPos = possibleSpawnPositions[Random.Range(0, possibleSpawnPositions.Count - 1)];
            Vector3 worldPos = levelManager.GetWorldTilemap().CellToWorld(randomSpawnPos);
            worldPos += Vector3.one * 0.5f;

            Instantiate(goalItem, worldPos, goalItem.localRotation);

            possibleSpawnPositions.Remove(randomSpawnPos);
        }

        foreach (Vector3Int spawnPoint in tooCloseToStartSpawns)
        {
            possibleSpawnPositions.Add(spawnPoint);
        }

        SpawnFoodItems();
    }

    void SpawnFoodItems()
    {
        // spawn other items (food, herbs)
        for (int i = 0; i < foodSpawnCount; i++)
        {
            Vector3Int randomSpawnPos = possibleSpawnPositions[Random.Range(0, possibleSpawnPositions.Count - 1)];
            Vector3 worldPos = levelManager.GetWorldTilemap().CellToWorld(randomSpawnPos);
            worldPos += Vector3.one * 0.5f;

            Instantiate(foodPrefab, worldPos, Quaternion.identity);

            possibleSpawnPositions.Remove(randomSpawnPos);
        }

        //spawn crystals closer to player
        List<Vector3Int> closeToBaseSpawns = new();
        float offset = 2;
        float maxDistanceFromBase = LevelManager.instance.GetDistanceToEdgeOfLevel().y;
        maxDistanceFromBase -= offset;

        foreach (Vector3Int spawnPoint in possibleSpawnPositions)
        {
            if (Vector3Int.Distance(spawnPoint, levelManager.GetStartingTilePos()) < maxDistanceFromBase)
            {
                closeToBaseSpawns.Add(spawnPoint);
            }
        }

        for (int i = 0; i < crystalSpawnCount; i++)
        {
            Vector3Int randomSpawnPos = closeToBaseSpawns[Random.Range(0, closeToBaseSpawns.Count - 1)];
            Vector3 worldPos = levelManager.GetWorldTilemap().CellToWorld(randomSpawnPos);
            worldPos += Vector3.one * 0.5f;

            Instantiate(crystalPrefab, worldPos, Quaternion.identity);

            closeToBaseSpawns.Remove(randomSpawnPos);
            possibleSpawnPositions.Remove(randomSpawnPos);
        }

        Events.Level.ItemsGenerated(possibleSpawnPositions);
    }

    void UpdateSpawnPoints(Vector2 collectionPos)
    {
        Vector3Int tilePos = LevelManager.instance.GetWorldTilemap().WorldToCell(collectionPos);

        possibleSpawnPositions.Add(tilePos);
    }
}
