using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthDisplay;
    int maxHealth, currentHealth;
    void OnEnable()
    {
        Events.Health.IntializeHealth += IntializeHealth;

        Events.Health.GainHealth += UpdateHealth;
        Events.Health.LoseHealth += UpdateHealth;
    }
    void OnDisable()
    {
        Events.Health.IntializeHealth -= IntializeHealth;

        Events.Health.GainHealth -= UpdateHealth;
        Events.Health.LoseHealth -= UpdateHealth;
    }

    void IntializeHealth(int _health)
    {
        maxHealth = _health;
        currentHealth = _health;
    }

    void UpdateHealth(int _updatedHealth)
    {
        currentHealth = _updatedHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthDisplay.text = "HP: " + currentHealth + "/" + maxHealth;
    }
}
