using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInAfterDelay : MonoBehaviour
{
    public float delaySeconds = 2.0f;
    public float fadeSpeed = 1f;
    public float fadeExponent = 4;

    private FadeInOut _fadeInOut;
    // Start is called before the first frame update
    void Start()
    {
        _fadeInOut = GetComponent<FadeInOut>();
        StartCoroutine(ExecuteAfterTime());
    }

    IEnumerator ExecuteAfterTime()
    {
        yield return new WaitForSeconds(delaySeconds);

        // Code to execute after the delay
        _fadeInOut.DirectAlpha(1.0f, fadeSpeed, fadeExponent);
    }
}
