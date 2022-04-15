using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{

    public OSC osc;

    public string transitionAddress = "/Transition";

    public string fadeAlphaAddress = "/FadeAlpha";
    private bool transitionInProgress = false;
    public void Update()
    {
        if (!transitionInProgress) {
            if(Input.GetKeyDown(KeyCode.W))
            {
                FadeWhite();
            }

            if(Input.GetKeyDown(KeyCode.B))
            {
                FadeBlack();
            }
        }
    }

    void TransitionOSC(OscMessage message) {
        if (!transitionInProgress) {
            switch (message.GetInt(0))
                {
                    case 0:
                        FadeWhite();
                        break;

                    case 1:
                        FadeBlack();
                        break;
                }
        }
    }

    void FadeAlphaOSC(OscMessage message) {
        if (!transitionInProgress) {
            StartCoroutine(FadeToAlpha(message.GetFloat(0), message.GetFloat(1), message.GetFloat(2)));
        }
    }
    void Start()
    {
        osc.SetAddressHandler( transitionAddress, TransitionOSC);
         osc.SetAddressHandler( fadeAlphaAddress, FadeAlphaOSC);
    }

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
    }

    public void FadeToColor(Color fadeColor, float fadeAlpha = 1.0f, float fadeSpeed = 1.0f, float exp = 2.0f, bool markSceneReadyToUnloadWhenDone = false) {
        transitionInProgress = true;
        GetComponent<Image>().color = fadeColor;
        StartCoroutine(FadeToAlpha(fadeAlpha, fadeSpeed, exp, markSceneReadyToUnloadWhenDone));

    }

    public IEnumerator FadeToAlpha(float fadeAlpha = 1.0f, float fadeSpeed = 1.0f, float exp = 2.0f, bool markSceneReadyToUnloadWhenDone = false) {
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
            EventsManager.instance.OnSceneReadyToUnload(gameObject.scene.buildIndex);
        }
    }

    public IEnumerator FadeOutIn(int fadeSpeed = 1)
    {
        Color objectColor = GetComponent<Image>().color;
        float fadeAmount;

        while (GetComponent<Image>().color.a < 1)
        {
            fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            GetComponent<Image>().color = objectColor;
            yield return null;
        }

        while (GetComponent<Image>().color.a > 0)
        {
            fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            GetComponent<Image>().color = objectColor;
            yield return null;
        }

        transitionInProgress = false;
    }
}
