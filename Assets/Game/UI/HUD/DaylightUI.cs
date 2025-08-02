using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DaylightUI : MonoBehaviour
{
    public TextMeshProUGUI timeDisplay;
    public Image timeMeter;
    int timeRemaining, totalTime;
    public void UpdateTime(int start, int time)
    {
        totalTime = start;
        timeRemaining = time;

        if (time > 0)
            timeDisplay.text = "TIME TIL DUSK: " + time + ":00";
        else
        {
            timeDisplay.text = "NIGHT HAS FALLEN";
        }
    }

    void Update()
    {
        timeMeter.fillAmount = (float)timeRemaining / (float)totalTime;
    }
}
