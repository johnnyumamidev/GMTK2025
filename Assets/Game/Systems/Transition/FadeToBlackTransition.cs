using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FadeToBlackTransition : MonoBehaviour
{
    public UnityEvent FadeComplete;
    [SerializeField] SpriteRenderer blackBox;
    [SerializeField] Color targetColor;
    [SerializeField] float speed;
    [SerializeField] float fadeLength;
    [SerializeField] float delayFadeOut;
    void OnEnable()
    {
        blackBox.color = Color.clear;
        StartCoroutine(StartFade());
    }

    void Update()
    {
        blackBox.color = targetColor;
    }

    IEnumerator StartFade()
    {
        float startTime = Time.time;
        float elapsedTime = 0;
        while (Time.time < startTime + fadeLength)
        {
            elapsedTime += Time.deltaTime;
            targetColor = new Color(0, 0, 0, elapsedTime / fadeLength);
            yield return new WaitForEndOfFrame();
        }
        targetColor = Color.black;

        FadeComplete?.Invoke();
        yield return new WaitForSeconds(delayFadeOut);

        startTime = Time.time;
        elapsedTime = fadeLength;
        while (Time.time < startTime + fadeLength)
        {
            elapsedTime -= Time.deltaTime;
            targetColor = new Color(0, 0, 0, elapsedTime / fadeLength);
            yield return new WaitForEndOfFrame();
        }
        targetColor = Color.clear;
    }
}
