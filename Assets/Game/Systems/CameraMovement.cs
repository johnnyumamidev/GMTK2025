using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] LayerMask boundaryLayer;
    [SerializeField] float moveSpeed;
    void Update()
    {
        Vector2 boundsCheck = (Vector2)transform.position + playerInput.Movement();
        Collider2D bounds = Physics2D.OverlapCircle(boundsCheck, 0.25f, boundaryLayer);
        if (bounds == null)
            return;

        transform.Translate(playerInput.Movement() * moveSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + playerInput.Movement(), 0.25f);
    }
}
