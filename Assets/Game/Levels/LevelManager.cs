using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    [SerializeField] TileBase baseTile;
    [SerializeField] TileBase selectedTile;
    [SerializeField] TileBase tentTile;
    [SerializeField] Tilemap worldTilemap;
    [SerializeField] Tilemap fogTilemap;

    [SerializeField] int levelWidth, levelHeight;
    [SerializeField] Transform levelGenerationStartPoint;
    [SerializeField] List<Vector3Int> playableTiles = new();

    [SerializeField] Transform player;
    public Vector3Int playerStartTilePos;

    public static LevelManager instance;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //loop through each tile at x, y until assigned height & width is reached
        GenerateLevel();
    }

    void GenerateLevel()
    {
        Vector3Int startPosition = worldTilemap.WorldToCell(levelGenerationStartPoint.position);
        for (int x = 0; x < levelWidth; x++)
        {
            for (int y = 0; y < levelHeight; y++)
            {
                Vector3Int currentTile = startPosition + new Vector3Int(x, y, 0);
                worldTilemap.SetTile(currentTile, baseTile);
                fogTilemap.SetTile(currentTile, baseTile);

                playableTiles.Add(currentTile);
            }
        }

        // get random tile from grid
        int random = Random.Range(0, playableTiles.Count - 1);
        playerStartTilePos = playableTiles[random];

        // place player at that randomly selected tile
        Vector3 playerStartPos = worldTilemap.CellToWorld(playerStartTilePos);
        playerStartPos += Vector3.one * 0.5f;
        player.position = playerStartPos;

        // set start pos tile to tent sprite
        worldTilemap.SetTile(playerStartTilePos, tentTile);

        // remove starting position from list of available spawn points
        playableTiles.RemoveAt(random);

        // invoke event to start spawning other units
        Events.Level.GridGenerated?.Invoke();
    }

    #region Get Variable Functions
    public List<Vector3Int> GetPlayableTiles()
    {
        return playableTiles;
    }

    public Tilemap GetWorldTilemap()
    {
        return worldTilemap;
    }
    public Tilemap GetFogTilemap()
    {
        return fogTilemap;
    }
    #endregion
    
    public void ChangeTileToSelected(Vector3Int selectedTilePos)
    {
        if (worldTilemap.HasTile(selectedTilePos))
        {
            worldTilemap.SetTile(selectedTilePos, selectedTile);
        }
    }

    public void ResetTiles()
    {
        foreach (Vector3Int tilePos in playableTiles)
        {
            worldTilemap.SetTile(tilePos, baseTile);
        }
    }
}
