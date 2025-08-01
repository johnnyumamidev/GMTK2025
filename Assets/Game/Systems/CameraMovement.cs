using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] LayerMask boundaryLayer;
    [SerializeField] float moveSpeed;
    void OnEnable()
    {
        Events.Level.GridGenerated += SetPositionToStart;
        Events.Level.LoopComplete += SetPositionToStart;
    }
    void OnDisable()
    {
        Events.Level.GridGenerated -= SetPositionToStart;
        Events.Level.LoopComplete -= SetPositionToStart;
    }

    void Update()
    {
        Vector2 boundsCheck = (Vector2)transform.position + playerInput.Movement();
        Vector3Int boundCheckTile = LevelManager.instance.GetWorldTilemap().WorldToCell(boundsCheck);
        if (!LevelManager.instance.GetWorldTilemap().HasTile(boundCheckTile))
        {
            return;
        }

        transform.Translate(playerInput.Movement() * moveSpeed * Time.deltaTime);
    }

    void SetPositionToStart()
    {
        Vector2 startPos = LevelManager.instance.GetWorldTilemap().CellToWorld(LevelManager.instance.GetStartingTilePos());

        transform.position = startPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + playerInput.Movement(), 0.25f);
    }
}
