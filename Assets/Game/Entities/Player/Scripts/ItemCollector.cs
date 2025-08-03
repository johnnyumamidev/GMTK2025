using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    bool canCollectItems = false;
    private void OnEnable() {
        Events.Level.MissingPartsGenerated += EnableCollection;
    }
    private void OnDisable() {
        Events.Level.MissingPartsGenerated -= EnableCollection;
    }
    void EnableCollection(int i)
    {
        canCollectItems = true;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (!canCollectItems)
            return;
            
        if (other.transform.TryGetComponent(out Item item))
        {
            item.Collect();
            Events.Level.CollectedItem?.Invoke();
        }
    }
}
