using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PathManager : MonoBehaviour
{
    LevelManager levelManager;
    [SerializeField] List<Vector3Int> selectedTiles = new();
    List<Vector3Int> neighboringTileDirections = new List<Vector3Int> {
        new Vector3Int(0, 1, 0),
        new Vector3Int(0, -1, 0),
        new Vector3Int(1, 0, 0),
        new Vector3Int(-1, 0, 0),
    };
    [SerializeField] Transform counterPrefab;
    List<Transform> counters = new();

    void OnEnable()
    {
        levelManager = FindObjectOfType<LevelManager>();

        Events.Level.LoopComplete += ClearCounters;
    }
    void OnDisable()
    {
        Events.Level.LoopComplete -= ClearCounters;
    }
    void Update()
    {
        //TESTING
        if (Input.GetKeyDown(KeyCode.Space))
        {
            selectedTiles.Clear();
            levelManager.ResetTiles();

            ClearCounters();

            Events.Level.Reset?.Invoke();
        }
    }

    public void AddTile(Vector3Int _tilePos)
    {
        if (selectedTiles.Contains(_tilePos) || !levelManager.GetPlayableTiles().Contains(_tilePos))
            return;

        if (selectedTiles.Count == 0)
        {
            CheckForNeighboringTiles(_tilePos, levelManager.GetStartingTilePos());
        }
        else
        {
            CheckForNeighboringTiles(_tilePos, selectedTiles[^1]);
        }
    }

    void CheckForNeighboringTiles(Vector3Int _tilePos, Vector3Int _point)
    {
        foreach (Vector3Int direction in neighboringTileDirections)
        {
            Vector3Int neighborTile = _point + direction;
            if (_tilePos == neighborTile)
            {
                selectedTiles.Add(_tilePos);
                levelManager.ChangeTileToSelected(_tilePos);
                SpawnCounter(_tilePos);
                break;
            }
        }
    }
    void SpawnCounter(Vector3Int tilePos)
    {
        Vector3 worldPos = levelManager.GetWorldTilemap().CellToWorld(tilePos);
        worldPos += Vector3.one * 0.5f;
        worldPos.z *= -1;
        Transform counterInstance = Instantiate(counterPrefab, worldPos, Quaternion.identity, levelManager.transform);
        counters.Add(counterInstance);

        PathCounter pathCounter = counterInstance.GetComponentInChildren<PathCounter>();
        pathCounter.UpdateText(selectedTiles.Count.ToString());
    }

    public List<Vector3Int> GetPath()
    {
        return selectedTiles;
    }

    void ClearCounters()
    {
        foreach (Transform counter in counters)
        {
            Destroy(counter.gameObject);
        }
        counters.Clear();
    }
}
