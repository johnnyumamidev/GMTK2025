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
    Vector2 targetPosition;
    [SerializeField] Transform collisionDetector;

    bool isFacingRight = true;

    void OnEnable()
    {
        Events.Level.StartLoop += ReadyNextMove;
        Events.Level.StartLoop += StoreStartPosition;

        Events.Level.LoopComplete += Reset;

        Events.Health.AllHealthLost += StopMovement;
    }
    void OnDisable()
    {
        Events.Level.StartLoop -= ReadyNextMove;
        Events.Level.StartLoop -= StoreStartPosition;

        Events.Level.LoopComplete -= Reset;

        Events.Health.AllHealthLost -= StopMovement;
    }
    void Update()
    {
        collisionDetector.position = targetPosition;

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

        targetPosition = levelManager.GetWorldTilemap().CellToWorld(path[pathIndex]);
        Vector2 offset = Vector2.one * 0.5f;
        targetPosition += offset;

        if (isReady && Vector2.Distance(transform.position, targetPosition) < distanceToTargetThreshold)
        {
            transform.position = targetPosition;
            isReady = false;

            Events.Level.ReachedNextTile?.Invoke(path[pathIndex]);

            if (pathIndex < path.Count - 1)
            {
                pathIndex++;
                Debug.Log("next tile");
                Invoke("ReadyNextMove", delayBetweenMoves);
            }
            else
                Events.Level.LoopComplete?.Invoke();
        }

        Vector2 dir = targetPosition - (Vector2)transform.position;

        if (dir.x < 0 && isFacingRight || dir.x > 0 && !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
        transform.Translate(dir * speed * Time.deltaTime);
    }

    void ReadyNextMove()
    {
        isReady = true;
        Events.Level.StartMove?.Invoke();
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
    void StopMovement(int i)
    {
        isReady = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(collisionDetector.position, 0.25f);
    }
}
