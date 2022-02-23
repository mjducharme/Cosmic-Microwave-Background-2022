using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(FadeWhiteOutSquare());
        }
    }

    public IEnumerator FadeWhiteOutSquare(bool fadeToWhite = true, int fadeSpeed = 1)
    {
        Color objectColor = GetComponent<Image>().color;
        float fadeAmount;

        if (fadeToWhite)
        {
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
}
