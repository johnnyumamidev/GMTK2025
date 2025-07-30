using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    [SerializeField] TileBase baseTile;
    [SerializeField] TileBase selectedTile;
    [SerializeField] Tilemap worldTilemap;

    [SerializeField] int levelWidth, levelHeight;
    [SerializeField] Transform startingPoint;
    [SerializeField] List<Vector3Int> playableTiles = new();
   

    void Start()
    {
        //loop through each tile at x, y until assigned height & width is reached
        Vector3Int startPosition = worldTilemap.WorldToCell(startingPoint.position);
        for (int x = 0; x < levelWidth; x++)
        {
            for (int y = 0; y < levelHeight; y++)
            {
                Vector3Int currentTile = startPosition + new Vector3Int(x, y, 0);
                worldTilemap.SetTile(currentTile, baseTile);

                playableTiles.Add(currentTile);
            }
        }
    }

    public List<Vector3Int> GetPlayableTiles()
    {
        return playableTiles;
    }

    public Tilemap GetWorldTilemap()
    {
        return worldTilemap;
    }

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
