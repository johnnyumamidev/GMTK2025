using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public int causeOfDeath = 0;
    public TextMeshProUGUI causeOfDeathDisplay;
    void Update()
    {
        if (causeOfDeath == 0)
        {
            //died from hunger
            causeOfDeathDisplay.text = "You were overcome by hunger...";
        }
        else
        {
            //died from damage
            causeOfDeathDisplay.text = "You were eaten by Aliens...";
        }
    }
}
