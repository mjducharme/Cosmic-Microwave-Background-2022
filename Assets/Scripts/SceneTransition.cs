using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{

    public OSC osc;

    public string bigBangAddress = "/BigBang";

    public string fadeAlphaAddress = "/FadeAlpha";
    private bool transitionInProgress = false;
    public void Update()
    {

    }

    void BigBangOSC(OscMessage message) {
        if (!transitionInProgress) {
            Debug.Log("Fading");
            FadeToColor(1, Color.white, 0, 0.1f, 4);
        }
    }

    void FadeAlphaOSC(OscMessage message) {
        if (!transitionInProgress) {
            StartCoroutine(FadeToAlpha(0, message.GetFloat(0), message.GetFloat(1), message.GetFloat(2)));
        }
    }
    void Start()
    {
        osc.SetAddressHandler( bigBangAddress, BigBangOSC);
        osc.SetAddressHandler( fadeAlphaAddress, FadeAlphaOSC);
    }

    /*
    public void FadeWhite () {
        transitionInProgress = true;
        Color whiteColor = Color.white;
        whiteColor.a = 1;
        GetComponent<Image>().color = whiteColor;
        StartCoroutine(FadeOutIn());
    }

    private void FadeBlack () {
        transitionInProgress = true;
        Color blackColor = Color.black;
        blackColor.a = 0;
        GetComponent<Image>().color = blackColor;
        StartCoroutine(FadeOutIn());  
    } */

    public void FadeToColor(int sceneNum, Color fadeColor, float fadeAlpha = 1.0f, float fadeSpeed = 1.0f, float exp = 2.0f, bool markSceneReadyToUnloadWhenDone = false) {
        transitionInProgress = true;
        GetComponent<Image>().color = fadeColor;
        StartCoroutine(FadeToAlpha(sceneNum, fadeAlpha, fadeSpeed, exp, markSceneReadyToUnloadWhenDone));

    }

    public IEnumerator FadeToAlpha(int sceneNum, float fadeAlpha = 1.0f, float fadeSpeed = 1.0f, float exp = 2.0f, bool markSceneReadyToUnloadWhenDone = false) {
        Color objectColor = GetComponent<Image>().color;
        float fadeAmount = GetComponent<Image>().color.a;

        if (fadeAmount < fadeAlpha) {
            while (fadeAmount < fadeAlpha)
            {
                
                //Debug.Log("Log fadeAmount " + Mathf.Pow(_Vfx.GetFloat(_visAlphaMultiplierId) + (fadeSpeed * Time.deltaTime),2));
                fadeAmount = fadeAmount + (fadeSpeed * Time.deltaTime);

                // don't go above the target
                if (fadeAmount > fadeAlpha) {
                    fadeAmount = fadeAlpha;
                }
                
                GetComponent<Image>().color = new Color(objectColor.r, objectColor.g, objectColor.b, Mathf.Pow(fadeAmount,exp));

                yield return null;
            }
        } else {
            while (fadeAmount > fadeAlpha)
            {
                fadeAmount = fadeAmount - (fadeSpeed * Time.deltaTime);

                // don't drop below the target
                if (fadeAmount < fadeAlpha) {
                    fadeAmount = fadeAlpha;
                }

                GetComponent<Image>().color = new Color(objectColor.r, objectColor.g, objectColor.b, Mathf.Pow(fadeAmount,exp));

                yield return null;
            }
        }
        if (markSceneReadyToUnloadWhenDone) {
            EventsManager.instance.OnSceneReadyToUnload(sceneNum);
        }
        transitionInProgress = false;
    }
}
