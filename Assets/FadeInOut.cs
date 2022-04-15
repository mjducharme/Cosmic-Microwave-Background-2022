using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FadeInOut : MonoBehaviour
{
    public OSC osc;

    private VisualEffect _Vfx;

    public string visAlphaMultiplier = "AlphaMultiplier";

    public string alphaMultiplierAddress = "/FadeInOut";

    private int _visAlphaMultiplierId;
    // Start is called before the first frame update
    void Start()
    {
        _Vfx = GetComponent<VisualEffect>();
        _visAlphaMultiplierId = Shader.PropertyToID(visAlphaMultiplier);
        osc = GameObject.Find("OSC").GetComponent<OSC>();
        osc.SetAddressHandler( alphaMultiplierAddress, AlphaMultiplier );
    }

    public void DirectAlpha(float fadeAlpha = 1.0f, float fadeSpeed = 1.0f, float exp = 2.0f, bool markSceneReadyToUnloadWhenDone = false) {
        StartCoroutine(FadeChange(fadeAlpha, fadeSpeed, exp, markSceneReadyToUnloadWhenDone)); 
    }

    void AlphaMultiplier(OscMessage message) {
        StartCoroutine(FadeChange(message.GetFloat(0), message.GetFloat(1), message.GetFloat(2))); 
    }

    public IEnumerator FadeChange(float fadeAlpha = 1.0f, float fadeSpeed = 1.0f, float exp = 2.0f, bool markSceneReadyToUnloadWhenDone = false)
    {
        float fadeAmount = _Vfx.GetFloat(_visAlphaMultiplierId);

        if (fadeAmount < fadeAlpha) {
            while (fadeAmount < fadeAlpha)
            {
                
                //Debug.Log("Log fadeAmount " + Mathf.Pow(_Vfx.GetFloat(_visAlphaMultiplierId) + (fadeSpeed * Time.deltaTime),2));
                fadeAmount = fadeAmount + (fadeSpeed * Time.deltaTime);

                // don't go above the target
                if (fadeAmount > fadeAlpha) {
                    fadeAmount = fadeAlpha;
                }
                
                _Vfx.SetFloat(_visAlphaMultiplierId, Mathf.Pow(fadeAmount,exp));

                yield return null;
            }
        } else {
            while (fadeAmount > fadeAlpha)
            {
                //Debug.Log("Log fadeAmount " + Mathf.Pow(_Vfx.GetFloat(_visAlphaMultiplierId) - (fadeSpeed * Time.deltaTime),2));
                fadeAmount = fadeAmount - (fadeSpeed * Time.deltaTime);
                //Debug.Log("Lin fadeAmount " + fadeAmount);

                // don't drop below the target
                if (fadeAmount < fadeAlpha) {
                    fadeAmount = fadeAlpha;
                }

                _Vfx.SetFloat(_visAlphaMultiplierId, Mathf.Pow(fadeAmount,exp));

                yield return null;
            }
        }
        if (markSceneReadyToUnloadWhenDone) {
            EventsManager.instance.OnSceneReadyToUnload(gameObject.scene.buildIndex);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
