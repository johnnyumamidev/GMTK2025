using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZoomInTransition : MonoBehaviour
{
    public UnityEvent TransitionEvent;
    [SerializeField] Transform circleMask;
    [SerializeField] float startScale;
    [SerializeField] float threshold;
    [SerializeField] float speedIn, speedOut;
    [SerializeField] float delay;
    void OnEnable()
    {
        StartCoroutine(StartTransition());
    }
    void OnDisable()
    {
        circleMask.localScale = Vector3.one * startScale;
    }

    IEnumerator StartTransition()
    {
        while (circleMask.localScale.x >= threshold)
        {
            circleMask.localScale = Vector3.Lerp(circleMask.localScale, Vector3.zero, speedIn * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        circleMask.localScale = Vector3.zero;
        TransitionEvent?.Invoke();
        yield return new WaitForSeconds(delay);


        while (circleMask.localScale.x <= startScale - threshold)
        {
            circleMask.localScale = Vector3.Lerp(circleMask.localScale, Vector3.one * startScale, speedOut * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        gameObject.SetActive(false);
    }
}
