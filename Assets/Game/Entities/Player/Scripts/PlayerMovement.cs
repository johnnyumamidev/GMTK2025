using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PathManager pathManager;
    [SerializeField] LevelManager levelManager;
    [SerializeField] float delayBetweenMoves = 1f;
    [SerializeField] float speed;
    [SerializeField] float distanceToTargetThreshold = 0.15f;
    int pathIndex = 0;
    public bool isReady = false;
    Vector3Int positionAtStartOfLoop;

    void OnEnable()
    {
        Events.Level.Reset += Reset;
        Events.Level.StartLoop += ReadyNextMove;
        Events.Level.StartLoop += StoreStartPosition;
    }
    void OnDisable()
    {
        Events.Level.Reset -= Reset;
        Events.Level.StartLoop -= ReadyNextMove;
        Events.Level.StartLoop -= StoreStartPosition;
    }
    void Update()
    {
        if (isReady)
        {
            Move();
        }
    }

    void Move()
    {
        List<Vector3Int> path = new(pathManager.GetPath());
        path.Add(positionAtStartOfLoop);
        
        if (path.Count <= 0)
            return;

        Vector3 targetPosition = levelManager.GetWorldTilemap().CellToWorld(path[pathIndex]);
        Vector3 offset = Vector3.one * 0.5f;
        targetPosition += offset;

        if (isReady && Vector3.Distance(transform.position, targetPosition) < distanceToTargetThreshold)
        {
            transform.position = targetPosition;
            isReady = false;

            Events.Level.ReachedNextTile?.Invoke(path[pathIndex]);

            if (pathIndex < path.Count - 1)
            {
                pathIndex++;
                Invoke("ReadyNextMove", delayBetweenMoves);
            }
            else
                Events.Level.LoopComplete?.Invoke();

        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }

    void ReadyNextMove()
    {
        isReady = true;
    }
    void StoreStartPosition()
    {
        Vector3Int pos = levelManager.GetWorldTilemap().WorldToCell(transform.position);
        positionAtStartOfLoop = pos;
    }
    void Reset()
    {
        pathIndex = 0;
    }
}
