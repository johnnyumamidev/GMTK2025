using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ClearFogComponent : MonoBehaviour
{
    List<Vector3Int> neighboringTileDirections = new List<Vector3Int> {
        new Vector3Int(0, 1, 0),
        new Vector3Int(0, -1, 0),
        new Vector3Int(1, 0, 0),
        new Vector3Int(-1, 0, 0),
        new Vector3Int(1, 1, 0),
        new Vector3Int(1, -1, 0),
        new Vector3Int(-1, -1, 0),
        new Vector3Int(-1, 1, 0),
    };

    Vector3Int pos;
    [SerializeField] GameObject fogFadeParticle;

    // Update is called once per frame
    void Update()
    {
        pos = LevelManager.instance.GetWorldTilemap().WorldToCell(transform.position);
        ClearFog(pos);

        foreach (var neighbor in neighboringTileDirections)
        {
            ClearFog(pos + neighbor);
        }
    }

    void ClearFog(Vector3Int _pos)
    {
        Tilemap fog = LevelManager.instance.GetFogTilemap();
        Vector2 worldPos = fog.CellToWorld(_pos) + Vector3.one * 0.5f;

        if (fog.HasTile(_pos))
        {
            fog.SetTile(_pos, null);
            Instantiate(fogFadeParticle, worldPos, Quaternion.identity);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        foreach (var neighbor in neighboringTileDirections)
        {
            Vector3Int pos = LevelManager.instance.GetWorldTilemap().WorldToCell(transform.position) + new Vector3Int(1,1,0);
            Gizmos.DrawWireSphere(pos + neighbor, 0.25f);
        }
    }
}
