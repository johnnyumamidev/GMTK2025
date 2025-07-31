using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage = 1;
    List<Vector2> directions = new List<Vector2>
    {
        new Vector2(-1, 0),
        new Vector2(0, 1),
        new Vector2(1, 0),
        new Vector2(0, -1),
    };
    Vector2 attackPoint;
    bool hasAttacked = false;
    void Start()
    {
        attackPoint = directions[Random.Range(0, 3)];

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, attackPoint);
        transform.rotation = targetRotation;

        attackPoint += (Vector2)transform.position;
    }
    void Update()
    {
        //check for player
        Collider2D checkForPlayer = Physics2D.OverlapCircle(attackPoint, 0.25f);

        if (checkForPlayer && checkForPlayer.gameObject.CompareTag("Player"))
        {
            if (!hasAttacked)
            {
                Events.Health.UpdateHealth?.Invoke(-damage);
                hasAttacked = true;   
            }
        }
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint, 0.25f);
    }
}
