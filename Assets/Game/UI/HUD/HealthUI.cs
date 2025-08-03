using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthDisplay;
    [SerializeField] Image hpMeter;
    int maxHealth, currentHealth;
    void OnEnable()
    {
        Events.Health.IntializeHealth += IntializeHealth;

        Events.Health.HealthChanged += UpdateHealth;
    }
    void OnDisable()
    {
        Events.Health.IntializeHealth -= IntializeHealth;

        Events.Health.HealthChanged -= UpdateHealth;
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
        hpMeter.fillAmount = Mathf.Lerp(hpMeter.fillAmount, (float)currentHealth / (float)maxHealth, 3 * Time.deltaTime);
        // healthDisplay.text = "HP: " + currentHealth + "/" + maxHealth;
    }
}
