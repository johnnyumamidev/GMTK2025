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
    }
    void Update()
    {
        //TESTING
        if (Input.GetKeyDown(KeyCode.Space))
        {
            selectedTiles.Clear();
            levelManager.ResetTiles();

            foreach (Transform arrow in counters)
            {
                Destroy(arrow.gameObject);
            }
            counters.Clear();
        }
    }

    public void AddTile(Vector3Int _tilePos)
    {
        if (selectedTiles.Contains(_tilePos) || !levelManager.GetPlayableTiles().Contains(_tilePos))
            return;

        if (selectedTiles.Count == 0)
        {
            selectedTiles.Add(_tilePos);
            levelManager.ChangeTileToSelected(_tilePos);
            SpawnCounter(_tilePos);
        }
        else
        {
            //check if selected tile is neighboring to last tile in the list
            foreach (Vector3Int direction in neighboringTileDirections)
            {
                Vector3Int neighborTile = selectedTiles[^1] + direction;

                if (_tilePos == neighborTile)
                {
                    selectedTiles.Add(neighborTile);
                    levelManager.ChangeTileToSelected(_tilePos);

                    SpawnCounter(neighborTile);
                    break;
                }
            }
        }
    }

    void SpawnCounter(Vector3Int tilePos)
    {
        Vector3 worldPos = levelManager.GetWorldTilemap().CellToWorld(tilePos);
        worldPos += Vector3.one * 0.5f;
        worldPos.z *= -1;
        Transform counterInstance = Instantiate(counterPrefab, worldPos, Quaternion.identity);
        counters.Add(counterInstance);

        TMP_Text counterText = counterInstance.GetComponentInChildren<TMP_Text>();
        counterText.text = selectedTiles.Count.ToString();
    }
}
