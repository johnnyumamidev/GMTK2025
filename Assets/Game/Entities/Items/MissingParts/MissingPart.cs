using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissingPart : MonoBehaviour
{
    public void Collect()
    {
        Events.Level.CollectedMissingPart?.Invoke(this);
    }
}
