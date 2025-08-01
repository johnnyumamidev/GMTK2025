using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HomeBase : MonoBehaviour
{
    [SerializeField] Transform projectilePrefab;
    [SerializeField] Transform cannon;
    Vector2 cannonDir;
    [SerializeField] float cannonRotSpeed = 5f;
    [SerializeField] Transform projectileSpawnPoint;
    bool projectileReady = true;
    [SerializeField] SpriteRenderer walls;
    [SerializeField] Sprite wallsUpSprite, wallsDownSprite;

    public UnityEvent ProjectileFired;
    void OnEnable()
    {
        Events.Combat.EnemyHit += ReadyProjectile;
        Events.Level.StartMove += ReadyProjectile;

        Events.Level.StartLoop += WallsDown;
        Events.Level.LoopComplete += WallsUp;
    }
    private void OnDisable()
    {
        Events.Combat.EnemyHit -= ReadyProjectile;
        Events.Level.StartMove -= ReadyProjectile;
        
        Events.Level.StartLoop -= WallsDown;
        Events.Level.LoopComplete -= WallsUp;
    }
    void Update()
    {
        //aim cannon 
        Quaternion targetRotation = Quaternion.LookRotation(cannon.forward, cannonDir);
        cannon.rotation = targetRotation;
    }

    // shoot a projectile towards the enemy
    public void FireProjectile(Vector2 target)
    {
        if (!projectileReady)
            return;

        Debug.Log("spawn projectile");
        Vector2 dirToTarget = target - (Vector2)transform.position;
        cannonDir = dirToTarget;

        Transform _projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        Projectile projectileComponent = _projectile.GetComponent<Projectile>();
        projectileComponent.moveDirection = dirToTarget;

        ProjectileFired?.Invoke();

        projectileReady = false;
    }

    void ReadyProjectile()
    {
        projectileReady = true;
    }

    void WallsDown()
    {
        walls.sprite = wallsDownSprite;
        cannon.gameObject.SetActive(true);
    }
    void WallsUp()
    {
        walls.sprite = wallsUpSprite;
        cannon.gameObject.SetActive(false);
    }
}
