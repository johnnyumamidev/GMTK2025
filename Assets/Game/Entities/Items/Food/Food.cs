using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int hungerValue;
    int daysUntilSpoiled = 3;
    int daysSinceSpawn = 0;
    void OnEnable()
    {
        Events.Level.LoopComplete += AddToSpoilCounter;
    }
    private void OnDisable() {
        Events.Level.LoopComplete -= AddToSpoilCounter;
    }
    public void Eat()
    {
        Events.Health.EatFood?.Invoke(hungerValue);
    }


    void AddToSpoilCounter()
    {
        daysSinceSpawn++;

        if (daysSinceSpawn >= daysUntilSpoiled)
        {
            Destroy(gameObject);
        }
    }
}
