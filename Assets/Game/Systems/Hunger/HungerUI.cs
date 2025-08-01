 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HungerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hungerDisplay;
    int hungerValue = 0;
    private void OnEnable() {
        Events.Health.HungerChanged += UpdateHunger;
    }
    private void OnDisable() {
        Events.Health.HungerChanged -= UpdateHunger;
    }
    void Update()
    {
        hungerDisplay.text = "Hunger: " + hungerValue;
    }

    void UpdateHunger(int value)
    {
        hungerValue = value;
    }
}
