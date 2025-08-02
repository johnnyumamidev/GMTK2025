using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;

public class ButtonInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public float scaler;

    public UnityEvent PointerEnter;
    public UnityEvent PointerExit;
    public UnityEvent PointerDown;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        PointerEnter?.Invoke();
        transform.localScale = transform.localScale * scaler;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        PointerExit?.Invoke();
        transform.localScale = Vector3.one;
    }

    // on mouse click
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        transform.localScale = transform.localScale / scaler;

        PointerDown?.Invoke();
    }
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        transform.localScale = transform.localScale * scaler;
    }
}
