using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int hungerValue;
    public Transform splatterFX;
    int daysUntilSpoiled = 2;
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
        Transform splat = Instantiate(splatterFX, transform.position, Quaternion.identity);
        splatterFX.gameObject.SetActive(true); 
        Events.Health.EatFood?.Invoke(hungerValue);
    }

    void AddToSpoilCounter()
    {
        daysSinceSpawn++;

        if (daysSinceSpawn >= daysUntilSpoiled)
        {
            Transform splat = Instantiate(splatterFX, transform.position, Quaternion.identity);
            splatterFX.gameObject.SetActive(true); 
            Destroy(gameObject);
        }
    }
}
