using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 moveDirection;
    public float speed;
    public Transform sprite;
    public Transform splatterFX;
    void Update()
    {
        transform.Translate(moveDirection.normalized * speed * Time.deltaTime);

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, moveDirection);

        sprite.rotation = targetRotation;

        Collider2D hit = Physics2D.OverlapCircle(transform.position, 1f);
        if (hit && hit.TryGetComponent(out Enemy enemy))
        {
            Events.Combat.EnemyHit?.Invoke();
            Destroy(enemy.gameObject);

            //spawn splatter visual
            Transform splatter = Instantiate(splatterFX, enemy.transform.position, Quaternion.identity);
            splatter.gameObject.SetActive(true);

            Destroy(gameObject);
        }
    }
}
