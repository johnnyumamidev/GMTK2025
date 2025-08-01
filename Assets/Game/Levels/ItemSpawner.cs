using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;

    [SerializeField] Transform goalItem;
    [SerializeField] int itemSpawnCount = 0;
    [SerializeField] int minDistanceFromStartTile = 4;

     void OnEnable()
    {
        Events.Level.GridGenerated += SpawnItems;
    }
    void OnDisable()
    {
        Events.Level.GridGenerated -= SpawnItems;
    }
    void Start()
    {
        Events.Level.MissingPartsGenerated?.Invoke(itemSpawnCount);
    }

    void SpawnItems()
    {
        // make goal collectibles spawn reasonably far away from start position 
        List<Vector3Int> possibleSpawnPositions = new(levelManager.GetPlayableTiles());
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
        for (int i = 0; i < itemSpawnCount; i++)
        {
            Vector3Int randomSpawnPos = possibleSpawnPositions[Random.Range(0, possibleSpawnPositions.Count - 1)];
            Vector3 worldPos = levelManager.GetWorldTilemap().CellToWorld(randomSpawnPos);
            worldPos += Vector3.one * 0.5f;

            Instantiate(goalItem, worldPos, goalItem.localRotation);

            possibleSpawnPositions.Remove(randomSpawnPos);
        }

        // re-add close spawn points
        foreach (Vector3Int spawnPoint in tooCloseToStartSpawns)
        {
            possibleSpawnPositions.Add(spawnPoint);
        }

        // spawn other items (food, herbs)

        Events.Level.ItemsGenerated(possibleSpawnPositions);
    }
}
