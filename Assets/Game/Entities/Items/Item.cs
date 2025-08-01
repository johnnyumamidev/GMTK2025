using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public UnityEvent Collected;
    public virtual void Collect()
    {
        Collected?.Invoke();
        gameObject.SetActive(false);
    }
}
