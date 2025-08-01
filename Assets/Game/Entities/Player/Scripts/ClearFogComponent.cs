using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ClearFogComponent : MonoBehaviour
{
    [SerializeField] GameObject fogFadeParticle;

    void OnEnable()
    {
        Events.Level.NeighborDetected += ClearFog;
    }
    void OnDisable()
    {
        Events.Level.NeighborDetected -= ClearFog;
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
}
