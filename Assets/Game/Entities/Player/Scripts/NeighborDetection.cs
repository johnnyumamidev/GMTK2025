using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighborDetection : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        Vector3Int pos = LevelManager.instance.GetWorldTilemap().WorldToCell(transform.position);
       
        //trigger on own tile
        Events.Level.NeighborDetected?.Invoke(pos);

        foreach (var neighbor in neighboringTileDirections)
        {
            Events.Level.NeighborDetected?.Invoke(pos + neighbor);
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
