using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class SceneTransition : MonoBehaviour
{

    public OSC osc;

    public string bigBangAddress = "/BigBang";

    public string fadeAlphaAddress = "/FadeAlpha";

    public string constellationsAddress = "/ToConstellations";

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

    void ConstellationsOSC(OscMessage message) {
        if (!transitionInProgress) {
            StartCoroutine(Constellations());
        }
    }

    void Start()
    {
        osc.SetAddressHandler( bigBangAddress, BigBangOSC);
        osc.SetAddressHandler( fadeAlphaAddress, FadeAlphaOSC);
        osc.SetAddressHandler( constellationsAddress, ConstellationsOSC);
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

    IEnumerator Constellations() {
        if (!transitionInProgress) {
            yield return StartCoroutine(FadeToAlpha(0, 1.0f, 1.0f, 1.0f));
        }
        GameObject.Find("VOLUME").SetActive(false);
        GameObject.Find("ElectricOrb").GetComponent<VisualEffect>().SetFloat("AlphaMultiplier", 0.0f);
        GameObject.Find("Section 3 A material visual effect").GetComponent<VisualEffect>().SetFloat("AlphaMultiplier", 0.0f);
        GameObject.Find("Constellations").GetComponent<VisualEffect>().enabled = true;
        StartCoroutine(FadeToAlpha(0, 0.0f, 1.0f, 1.0f));

        yield return null;
    }

}
