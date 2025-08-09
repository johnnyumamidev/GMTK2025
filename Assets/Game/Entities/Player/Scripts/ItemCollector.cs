using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    bool canCollectItems = false;
    private void OnEnable() {
        Events.Level.StartLoop += EnableCollection;
        Events.Level.LoopComplete += DisableCollection;
    }
    private void OnDisable() {
        Events.Level.StartLoop -= EnableCollection;
        Events.Level.LoopComplete -= DisableCollection;

    }
    void EnableCollection()
    {
        canCollectItems = true;
    }
    void DisableCollection()
    {
        canCollectItems = false;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (!canCollectItems)
            return;
            
        if (other.transform.TryGetComponent(out Item item))
        {
            item.Collect();
        }
    }
}
