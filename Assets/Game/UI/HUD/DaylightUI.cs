using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DaylightUI : MonoBehaviour
{
    public TextMeshProUGUI timeDisplay;
    public void UpdateTime(int time)
    {
        if (time > 0)
            timeDisplay.text = "Time Until Dusk: " + time;
        else
        {
            timeDisplay.text = "NIGHT HAS FALLEN";
        }
    }
}
