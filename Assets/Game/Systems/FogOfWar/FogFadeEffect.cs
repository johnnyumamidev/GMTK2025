using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogFadeEffect : MonoBehaviour
{
    public float fadeSpeed;
    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, fadeSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.localScale, Vector3.zero) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
