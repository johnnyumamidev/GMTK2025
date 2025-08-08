using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSelector : MonoBehaviour
{
    [SerializeField] PathManager pathManager;
    [SerializeField] LevelManager levelManager;

    bool canDraw = true;

    private void OnEnable()
    {
        Events.Level.TeleportMode += DisableDraw;
        Events.Level.EndTeleportMode += EnableDraw;

        Events.Level.InitiateTeleport += EnableDraw;
    }
    private void OnDisable()
    {
        Events.Level.TeleportMode -= DisableDraw;
        Events.Level.EndTeleportMode -= EnableDraw;

        Events.Level.InitiateTeleport -= EnableDraw;
    }

    void Update()
    {
        Vector2 mousePosToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tilePos = levelManager.GetWorldTilemap().WorldToCell(mousePosToWorld);
        if (levelManager.GetWorldTilemap().HasTile(tilePos))
        {
            Vector2 tileToWorldPos = levelManager.GetWorldTilemap().CellToWorld(tilePos);
            transform.position = tileToWorldPos + Vector2.one * 0.5f;
        }
        
        if (!canDraw)
            return;

        if (Input.GetMouseButton(0))
        {
            Vector3Int mouseToTilemapPos = levelManager.GetWorldTilemap().WorldToCell(mousePosToWorld);
            pathManager?.AddTile(mouseToTilemapPos);
        }
    }

    void DisableDraw()
    {
        canDraw = false;
    }
    void EnableDraw()
    {
        canDraw = true;
    }
}
