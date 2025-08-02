using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoalUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI counter;

    public int foundPartsRemaining;
    void Update()
    {
        counter.text = "0 " + foundPartsRemaining.ToString();
    }
}
