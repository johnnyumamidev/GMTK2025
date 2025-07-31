using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] int maxHealth, currentHealth;

    void OnEnable()
    {
        Events.Health.UpdateHealth += UpdateHealth;
    }
    void OnDisable()
    {
        Events.Health.UpdateHealth -= UpdateHealth;
    }
    void Start()
    {
        currentHealth = maxHealth;

        Events.Health.IntializeHealth?.Invoke(maxHealth);
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
    }
    void IncreaseHealth()
    {
        Events.Health.GainHealth?.Invoke(currentHealth);
    }
    void DecreaseHealth()
    {
        Events.Health.LoseHealth?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            Events.Health.AllHealthLost?.Invoke();
        }
    }
}
