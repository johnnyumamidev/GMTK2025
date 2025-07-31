using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;

    [SerializeField] Transform itemPrefab;
    [SerializeField] int itemSpawnCount = 0;
     void OnEnable()
    {
        Events.Level.GridGenerated += SpawnItems;
    }
    void OnDisable()
    {
        Events.Level.GridGenerated -= SpawnItems;
    }

    void SpawnItems()
    {
        List<Vector3Int> possibleSpawnPositions = new(levelManager.GetPlayableTiles());

        for (int i = 0; i < itemSpawnCount; i++)
        {
            Vector3Int randomSpawnPos = possibleSpawnPositions[Random.Range(0, possibleSpawnPositions.Count - 1)];
            Vector3 worldPos = levelManager.GetWorldTilemap().CellToWorld(randomSpawnPos);
            worldPos += Vector3.one * 0.5f;

            Instantiate(itemPrefab, worldPos, itemPrefab.localRotation);
            
            possibleSpawnPositions.Remove(randomSpawnPos);
        }

        Events.Level.ItemsGenerated(possibleSpawnPositions);
    }
}
