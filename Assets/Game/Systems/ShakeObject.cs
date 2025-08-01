using UnityEngine;
using System.Collections;

public class ShakeObject : MonoBehaviour
{
    Vector3 startPosition;
    [SerializeField] float shakeIntensity = 0.5f;
    [SerializeField] int shakeFrequency = 5;
    [SerializeField] float delayBetweenShake = 0.5f;
    public bool shakeOnEnable = false;

    void OnEnable()
    {
        if (shakeOnEnable)
            CallShake();
    }
    
    public void CallShake()
    {
        startPosition = transform.position;
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        for(int i = 0; i < shakeFrequency; i++)
        {
            transform.position = startPosition + ((Vector3)Random.insideUnitCircle * shakeIntensity);
            yield return new WaitForSeconds(delayBetweenShake);
        }
        transform.position = startPosition;
    }
}
