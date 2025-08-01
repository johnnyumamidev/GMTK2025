using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] int maxHealth, currentHealth;
    [SerializeField] int repairAmt = 5;
    void OnEnable()
    {
        Events.Health.UpdateHealth += UpdateHealth;

        Events.Level.LoopComplete += Repair;
    }
    void OnDisable()
    {
        Events.Health.UpdateHealth -= UpdateHealth;

        Events.Level.LoopComplete -= Repair;
    }
    void Start()
    {
        currentHealth = maxHealth;

        Events.Health.IntializeHealth?.Invoke(maxHealth);
    }

    void Repair()
    {
        UpdateHealth(repairAmt);
    }
    void UpdateHealth(int amount)
    {
        currentHealth += amount;

        if (amount < 0)
        {
            DecreaseHealth();
        }
        else
        {
            IncreaseHealth();
        }

        Events.Health.HealthChanged?.Invoke(currentHealth);
    }
    void IncreaseHealth()
    {
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        Events.Health.GainHealth?.Invoke();
    }
    void DecreaseHealth()
    {
        if (currentHealth <= 0)
        {
            Events.Health.AllHealthLost?.Invoke();
        }
        else
        {
            Events.Health.LoseHealth?.Invoke();
        }
    }
}
