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

        Events.Level.LoopComplete += Reset;
        Events.Level.Reset += Reset;

        Events.Level.Undo += RemoveLastPointOnPath;
    }
    void OnDisable()
    {
        Events.Level.LoopComplete -= Reset;
        Events.Level.Reset -= Reset;

        Events.Level.Undo -= RemoveLastPointOnPath;
    }

    public void AddTile(Vector3Int _tilePos)
    {
        if (selectedTiles.Contains(_tilePos) || !levelManager.GetPlayableTiles().Contains(_tilePos) || _tilePos == levelManager.GetStartingTilePos())
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
                // levelManager.AddTileToPath(_tilePos);
                SpawnCounter(_tilePos, _point);

                foreach (Vector3Int dir in neighboringTileDirections)
                {
                    Vector3Int neighbor = levelManager.GetStartingTilePos() + dir;
                    if (neighbor == _tilePos)
                    {
                        // loop is closed!
                        Events.Level.PathDrawnIsClosedLoop?.Invoke();
                        break;
                    }
                    else
                    {
                        Events.Level.PathDrawnIsOpen?.Invoke();
                    }
                }

                SFXManager.instance.PlayPathDrawSFX();

                break;
            }
        }
    }

    void SpawnCounter(Vector3Int tilePos, Vector3Int start)
    {
        Vector3 worldPos = levelManager.GetWorldTilemap().CellToWorld(tilePos);
        worldPos += Vector3.one * 0.5f;
        worldPos.z *= -1;

        //spawn path visual
        Transform counterInstance = Instantiate(counterPrefab, worldPos, Quaternion.identity, levelManager.transform);

        //get direction from prev point to next point
        Vector2 direction = levelManager.GetWorldTilemap().CellToWorld(tilePos) - levelManager.GetWorldTilemap().CellToWorld(start);
        Quaternion targetRotation = Quaternion.LookRotation(counterInstance.forward, direction);
        counterInstance.rotation = targetRotation;

        counters.Add(counterInstance);

        PathCounter pathCounter = counterInstance.GetComponentInChildren<PathCounter>();
        pathCounter.UpdateText(selectedTiles.Count.ToString());
    }

    void RemoveLastPointOnPath()
    {
        // destroy arrow spawn counter & remove from counters list
        Destroy(counters[^1].gameObject);
        counters.Remove(counters[^1]);

        // remove last point on path
        selectedTiles.Remove(selectedTiles[^1]);

        // play undo sfx
    }

    public List<Vector3Int> GetPath()
    {
        return selectedTiles;
    }

    void Reset()
    {
        foreach (Transform counter in counters)
        {
            Destroy(counter.gameObject);
        }
        counters.Clear();

        selectedTiles.Clear();
        levelManager.ResetTiles();
    }
}
