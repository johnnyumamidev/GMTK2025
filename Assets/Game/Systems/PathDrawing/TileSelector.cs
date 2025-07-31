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
        transform.position = mousePosToWorld;

        if (Input.GetMouseButton(0))
        {
            Vector3Int mouseToTilemapPos = levelManager.GetWorldTilemap().WorldToCell(mousePosToWorld);
            pathManager?.AddTile(mouseToTilemapPos);
        }
    }
}
