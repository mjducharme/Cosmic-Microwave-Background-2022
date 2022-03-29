using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            Color whiteColor = Color.white;
            whiteColor.a = 0;
            GetComponent<Image>().color = whiteColor;
            StartCoroutine(FadeOutIn());
        }

        if(Input.GetKeyDown(KeyCode.B))
        {
            Color blackColor = Color.black;
            blackColor.a = 0;
            GetComponent<Image>().color = blackColor;
            StartCoroutine(FadeOutIn());
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
    }
}
