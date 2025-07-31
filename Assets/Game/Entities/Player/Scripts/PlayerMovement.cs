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
    void OnEnable()
    {
        Events.Level.Reset += Reset;
        Events.Level.StartLoop += ReadyNextMove;
    }
    void OnDisable()
    {
        Events.Level.Reset -= Reset;
        Events.Level.StartLoop -= ReadyNextMove;
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
        List<Vector3Int> path = pathManager.GetPath();
        if (path.Count <= 0)
            return;

        Vector3 targetPosition = levelManager.GetWorldTilemap().CellToWorld(path[pathIndex]);
        Vector3 offset = Vector3.one * 0.5f;
        targetPosition += offset;

        if (isReady && Vector3.Distance(transform.position, targetPosition) < distanceToTargetThreshold)
        {
            transform.position = targetPosition;
            isReady = false;

            if (pathIndex < path.Count - 1)
                pathIndex++;
            else
                Events.Level.LoopComplete?.Invoke();

            Invoke("ReadyNextMove", delayBetweenMoves);
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }

    void ReadyNextMove()
    {
        isReady = true;
    }

    void Reset()
    {
        pathIndex = 0;
    }
}
