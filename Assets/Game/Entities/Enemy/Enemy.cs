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
    bool isAgressive = false;

    GameObject player;
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject sleepyParticles;

    [SerializeField] Animator animator;
    [SerializeField] SFXEnemy enemySFX;
    public Transform splatterFX;
    Vector3 targetPosition;
    void OnEnable()
    {
        Events.Level.StartNight += EnableAggression;
        Events.Level.LoopComplete += DisableAggression;
        Events.Level.LoopComplete += MoveAtNight;

        Events.Level.StartMove += MoveTowardsPlayer;
    }
    void OnDisable()
    {
        Events.Level.StartNight -= EnableAggression;
        Events.Level.LoopComplete -= DisableAggression;
        Events.Level.LoopComplete -= MoveAtNight;

        Events.Level.StartMove -= MoveTowardsPlayer;
    }
    void Start()
    {
        targetPosition = transform.position;
        // store player variable for when ready to chase
        player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        // consume any food enemy overlaps
        Collider2D obj = Physics2D.OverlapCircle(transform.position, 0.25f);
        if (obj && obj.TryGetComponent(out Food food))
        {
            Destroy(food.gameObject);
        }

        sleepyParticles.SetActive(!isAgressive);

        if (!isAgressive)
        {
            animator.CrossFade("Sleep", 0, 0);
            return;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }


    public void Die()
    {
        //spawn splatter visual
        Transform splatter = Instantiate(splatterFX, transform.position, Quaternion.identity);
        splatter.gameObject.SetActive(true);

        Destroy(gameObject);
    }
    void EnableAggression()
    {
        isAgressive = true;
    }
    void DisableAggression()
    {
        isAgressive = false;
    }
    void MoveTowardsPlayer()
    {
        if (!isAgressive)
            return;

        hasAttacked = false;

        // calculate direction to player
        Vector2 dirToPlayer = player.transform.position - transform.position;
        Vector2 assignedDir;
        // loop through each direction vector
        for (int i = 0; i < directions.Count; i++)
        {
            Vector2 dir = directions[i];
            float distance = Vector2.Distance(dir, dirToPlayer.normalized);
            if (distance < 1)
            {
                // pick direction that is closest to direction to player
                assignedDir = dir;

                // move in that direction
                targetPosition += (Vector3)assignedDir;

                enemySFX.PlayStepSFX();

                animator.CrossFade("Run", 0, 0);
                break;
            }
        }
        
        //check for player
        foreach (Vector2 dir in directions)
        {
            Collider2D checkForPlayer = Physics2D.OverlapCircle((Vector2)transform.position + dir, 0.25f);

            if (checkForPlayer && checkForPlayer.gameObject.CompareTag("Player"))
            {
                if (!hasAttacked)
                {
                    Debug.Log("hit target");
                    animator.CrossFade("Attack", 0, 0);
                    Events.Health.UpdateHealth?.Invoke(-damage);
                    hasAttacked = true;
                }
            }
        }
    }

    void MoveAtNight()
    {
        Vector2 dir = directions[Random.Range(0, 3)];
        Vector3Int targetTile = LevelManager.instance.GetWorldTilemap().WorldToCell((Vector2)transform.position + dir);

        if (LevelManager.instance.GetWorldTilemap().HasTile(targetTile))
            targetPosition += (Vector3)dir;

        enemySFX.PlayStepSFX();
        animator.CrossFade("Run", 0, 0);
    }
    public bool IsAgressive()
    {
        return isAgressive;
    }
    void OnDrawGizmos()
    {
        if (isAgressive && !hasAttacked)
        {
            foreach (Vector2 dir in directions)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere((Vector2)transform.position + dir, 0.4f);
            }
        }
    }
}
