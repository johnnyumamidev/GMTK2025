using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField] int chargeAmt;
    [SerializeField] GameObject shatterSpawn;
    int daysUntilSpoiled = 3;
    int daysSinceSpawn = 0;
    void OnEnable()
    {
        Events.Level.LoopComplete += AddToSpoilCounter;
    }
    private void OnDisable() {
        Events.Level.LoopComplete -= AddToSpoilCounter;
    }

    public void GainCharge()
    {
        Events.Health.GainCharge?.Invoke(chargeAmt);
    }
    
    void AddToSpoilCounter()
    {
        daysSinceSpawn++;

        if (daysSinceSpawn >= daysUntilSpoiled)
        {
            shatterSpawn.SetActive(true);
            Instantiate(shatterSpawn, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
