using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        // Debug.Log(other.transform.name);
        // if (other.transform.TryGetComponent(out Item item))
        // {
        //     item.Collect();
        // }

        // if (other.transform.TryGetComponent(out Enemy enemy))
        // {
        //     Destroy(other.gameObject);
        // }
    }
}
