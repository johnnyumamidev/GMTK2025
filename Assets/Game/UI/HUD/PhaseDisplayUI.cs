using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhaseDisplayUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI header;
    [SerializeField] TextMeshProUGUI phaseDisplay;
    [SerializeField] Image phaseContainer;
    [SerializeField] float errorDisplayDuration;
    Color startingColor;
    string currentPhase = "";
    void Start()
    {
        startingColor = phaseContainer.color;
        UpdateText("Planning");
    }
    void Update()
    {
        phaseDisplay.text = currentPhase;
    }

    public void UpdateText(string _phase)
    {
        currentPhase = _phase;
    }

    public void DisplayError()
    {
        SFXManager.instance.CancelPathSFX();

        header.text = "ERROR";
        header.color = Color.red;
        UpdateText("LOOP IS OPEN");
        phaseContainer.color = Color.red;

        StopAllCoroutines();
        StartCoroutine(ResetDisplay());
    }

    IEnumerator ResetDisplay()
    {
        yield return new WaitForSeconds(errorDisplayDuration);

        header.color = startingColor;
        header.text = "Phase: ";
        phaseContainer.color = startingColor;
        UpdateText("Planning");
    }
}
