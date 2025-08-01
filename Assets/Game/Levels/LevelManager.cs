using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    [SerializeField] TileBase baseTile;
    [SerializeField] TileBase selectedTile;
    [SerializeField] TileBase tentTile;
    [SerializeField] TileBase fogTile;
    [SerializeField] Tilemap worldTilemap;
    [SerializeField] Tilemap fogTilemap;
    [SerializeField] Tilemap pathTilemap;

    [SerializeField] int levelWidth, levelHeight;
    [SerializeField] Transform levelGenerationStartPoint;
    Vector3Int levelGenStartTilePos;
    Vector3Int playerStartTilePos;
    [SerializeField] List<Vector3Int> playableTiles = new();

    [SerializeField] Transform player;

    public static LevelManager instance;
    void Awake()
    {
        instance = this;
    }
    void OnEnable()
    {
        Events.Level.StartLoop += SetPathTilemapToGroundLayer;

        Events.Level.ReachedNextTile += RemoveTileFromPath;

        Events.Level.LoopComplete += SetPathTilemapToFogLayer;
        Events.Level.LoopComplete += ResetTiles;
    } 
    void OnDisable()
    {
        Events.Level.StartLoop -= SetPathTilemapToGroundLayer;

        Events.Level.ReachedNextTile -= RemoveTileFromPath;

        Events.Level.LoopComplete -= SetPathTilemapToFogLayer;
        Events.Level.LoopComplete -= ResetTiles;
    }
    void Start()
    {
        //loop through each tile at x, y until assigned height & width is reached
        GenerateLevel();
    }

    void GenerateLevel()
    {
        levelGenStartTilePos = worldTilemap.WorldToCell(levelGenerationStartPoint.position);
        for (int x = 0; x < levelWidth; x++)
        {
            for (int y = 0; y < levelHeight; y++)
            {
                Vector3Int currentTile = levelGenStartTilePos + new Vector3Int(x, y, 0);
                worldTilemap.SetTile(currentTile, baseTile);
                fogTilemap.SetTile(currentTile, fogTile);

                playableTiles.Add(currentTile);
            }
        }

        // get the middle tile of the entire map
        Vector3Int centerTile = new Vector3Int(Mathf.CeilToInt(levelWidth / 2 - 1), Mathf.CeilToInt(levelHeight / 2 - 1), 0);
        player.position = worldTilemap.CellToWorld(centerTile) + (Vector3.one * 0.5f);
        playerStartTilePos = centerTile;

        // set start pos tile to tent sprite
        worldTilemap.SetTile(playerStartTilePos, tentTile);

        // invoke event to start spawning other units
        Events.Level.GridGenerated?.Invoke();
    }

    #region Get Variable Functions
    public List<Vector3Int> GetPlayableTiles()
    {
        return playableTiles;
    }
    public Vector3Int GetStartingTilePos()
    {
        return playerStartTilePos;
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

    public void AddTileToPath(Vector3Int selectedTilePos)
    {
        if (worldTilemap.HasTile(selectedTilePos))
        {
            pathTilemap.SetTile(selectedTilePos, selectedTile);
        }
    }
    void RemoveTileFromPath(Vector3Int tileToRemove)
    {
        pathTilemap.SetTile(tileToRemove, null);
    }

    public void ResetTiles()
    {
        foreach (Vector3Int tilePos in playableTiles)
        {
            pathTilemap.SetTile(tilePos, null);
        }
    }

    void SetPathTilemapToGroundLayer()
    {
        // show path below fog
        pathTilemap.GetComponent<TilemapRenderer>().sortingLayerName = "Ground";
    }
    void SetPathTilemapToFogLayer()
    {
        // show path above fog
        pathTilemap.GetComponent<TilemapRenderer>().sortingLayerName = "Fog";
    }
}
