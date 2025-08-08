using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField] int chargeAmt;
    public void GainCharge()
    {
        Events.Health.GainCharge?.Invoke(chargeAmt);
    }
}
