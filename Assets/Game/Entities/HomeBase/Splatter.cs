using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splatter : MonoBehaviour
{
    int daysUntilFade = 2;
    int daysSinceSpawn = 0;
    void OnEnable()
    {
        Events.Level.LoopComplete += AddFadeCounter;
    }
    private void OnDisable()
    {
        Events.Level.LoopComplete -= AddFadeCounter;

    }

    void AddFadeCounter()
    {
        daysSinceSpawn++;

        if (daysSinceSpawn >= daysUntilFade)
        {
            Destroy(gameObject);
        }
    }
}
