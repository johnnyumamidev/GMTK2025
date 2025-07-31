using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;

    [SerializeField] Transform enemyPrefab;
    [SerializeField] int enemySpawnCount = 0;
     void OnEnable()
    {
        Events.Level.ItemsGenerated += SpawnEnemies;
    }
    void OnDisable()
    {
        Events.Level.ItemsGenerated -= SpawnEnemies;
    }

    void SpawnEnemies(List<Vector3Int> possibleSpawnPositions)
    {
        List<Vector3Int> validSpawnPoints = new(possibleSpawnPositions);

        for (int i = 0; i < enemySpawnCount; i++)
        {
            Vector3Int randomSpawnPos = validSpawnPoints[Random.Range(0, validSpawnPoints.Count - 1)];
            Vector3 worldPos = levelManager.GetWorldTilemap().CellToWorld(randomSpawnPos);
            worldPos += Vector3.one * 0.5f;

            Instantiate(enemyPrefab, worldPos, enemyPrefab.localRotation);

            validSpawnPoints.Remove(randomSpawnPos);
        }
    }
}
