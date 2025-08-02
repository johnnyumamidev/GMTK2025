using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerManager : MonoBehaviour
{
    public int startingHunger;
    public int currentHunger;

    // Start is called before the first frame update
    void OnEnable()
    {
        currentHunger = startingHunger;


        Events.Level.StartMove += DepleteHunger;

        Events.Health.EatFood += RefillHunger;
    }
    private void OnDisable()
    {
        Events.Level.StartMove -= DepleteHunger;

        Events.Health.EatFood -= RefillHunger;
    }
    void Start()
    {
        Events.Health.HungerChanged?.Invoke((float)currentHunger, (float)startingHunger);
    }
    void DepleteHunger()
    {
        currentHunger--;

        if (currentHunger <= 0)
        {
            Events.Health.AllHealthLost?.Invoke();
        }
        else
            Events.Health.HungerChanged?.Invoke(currentHunger, startingHunger);
    }
    void RefillHunger(int amt)
    {
        currentHunger += amt;

        if (currentHunger > startingHunger)
            currentHunger = startingHunger;

        Events.Health.HungerChanged?.Invoke(currentHunger, startingHunger);
    }
}
