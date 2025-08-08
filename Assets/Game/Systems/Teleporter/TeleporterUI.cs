using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TeleporterUI : MonoBehaviour
{
    [SerializeField] Image teleporterButton;
    [SerializeField] Image highlight;
    Color highlightColor;
    [SerializeField] Color errorColor;
    [SerializeField] Image[] charges;
    [SerializeField] TextMeshProUGUI buttonText;

    private void OnEnable()
    {
        Events.Health.ChargesChanged += UpdateCharges;

        Events.Level.NotEnoughCharges += NotifyInsuffienctCharges;
    }
    private void OnDisable()
    {
        Events.Health.ChargesChanged -= UpdateCharges;

        Events.Level.NotEnoughCharges -= NotifyInsuffienctCharges;
    }

    void Start()
    {
        highlightColor = highlight.color;

        for (int i = 0; i < charges.Length; i++)
        {
            charges[i].color = Color.red;
        }
    }
    void UpdateCharges(int _chargeAmt)
    {
        if (_chargeAmt <= 0)
        {
            for (int i = 0; i < charges.Length; i++)
            {
                charges[i].color = Color.red;
            }
        }
        else
        {
            for (int i = 0; i < _chargeAmt; i++)
            {
                charges[i].color = Color.green;
            }
        }
    }

    void NotifyInsuffienctCharges()
    {
        buttonText.text = "Need More Charges";
        highlight.gameObject.SetActive(true);
        highlight.color = errorColor;

        Invoke("RevertButton", 1.25f);
    }

    void RevertButton()
    {
        buttonText.text = "Teleport";

        highlight.gameObject.SetActive(false);
        highlight.color = highlightColor;
    }
}
