using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class GoalUI : MonoBehaviour
{
    public UnityEvent ItemFound;
    [SerializeField] TextMeshProUGUI counter;

    public int foundPartsRemaining;
    void OnEnable()
    {
        Events.Level.CollectedMissingPart += FoundEvent;
    }
    private void OnDisable() {
        Events.Level.CollectedMissingPart -= FoundEvent;
    }
    void Update()
    {
        counter.text = "0 " + foundPartsRemaining.ToString();
    }

    void FoundEvent(MissingPart _m)
    {
        ItemFound?.Invoke();
    }
}
