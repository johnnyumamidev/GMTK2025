using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackComponent : MonoBehaviour
{
    public UnityEvent<Vector2> AttackTarget;
    public float attackRange = 2;
    void OnEnable()
    {
        Events.Level.NeighborDetected += Attack;
    }
    void OnDisable()
    {
        Events.Level.NeighborDetected -= Attack;
    }

    void Attack(Vector3Int pos)
    {
        Vector2 worldPos = LevelManager.instance.GetWorldTilemap().CellToWorld(pos) + Vector3.one * 0.5f;
        if (Vector2.Distance(worldPos, transform.position) > attackRange)
            return;

        Collider2D damageable = Physics2D.OverlapCircle(worldPos, 0.25f);

        if (damageable && damageable.TryGetComponent(out Enemy enemy))
        {
            AttackTarget?.Invoke(enemy.transform.position);
        }
    }
}
