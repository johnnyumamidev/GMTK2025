using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public UnityEvent Collected;
    public virtual void Collect()
    {
        Events.Level.CollectedItem?.Invoke(transform.position);
        Collected?.Invoke();
        gameObject.SetActive(false);
    }
}
