 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HungerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hungerDisplay;
    [SerializeField] Image hungerBar;
    float startingHunger = 0;
    float hungerValue = 0;
    private void OnEnable() {
        Events.Health.HungerChanged += UpdateHunger;
    }
    private void OnDisable() {
        Events.Health.HungerChanged -= UpdateHunger;
    }
    void Update()
    {
        hungerBar.fillAmount = hungerValue / startingHunger;
    }

    void UpdateHunger(float curHunger, float maxHunger)
    {
        hungerValue = curHunger;
        startingHunger = maxHunger;
    }
}
