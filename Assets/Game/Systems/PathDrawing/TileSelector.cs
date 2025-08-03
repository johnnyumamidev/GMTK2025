using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSelector : MonoBehaviour
{
    [SerializeField] PathManager pathManager;
    [SerializeField] LevelManager levelManager;

    void Update()
    {
        Vector2 mousePosToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tilePos = levelManager.GetWorldTilemap().WorldToCell(mousePosToWorld);
        if (levelManager.GetWorldTilemap().HasTile(tilePos))
        {
            Vector2 tileToWorldPos = levelManager.GetWorldTilemap().CellToWorld(tilePos);
            transform.position = tileToWorldPos + Vector2.one * 0.5f;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3Int mouseToTilemapPos = levelManager.GetWorldTilemap().WorldToCell(mousePosToWorld);
            pathManager?.AddTile(mouseToTilemapPos);
        }
    }
}
