using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int hungerValue;
    public void Eat()
    {
        Events.Health.EatFood?.Invoke(hungerValue);
    }
}
