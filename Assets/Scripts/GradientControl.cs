using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientControl
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Gradient SetGradient(Gradient minColor, Gradient maxColor, float value)
    {
        Gradient g = new Gradient();
        GradientColorKey[] minColorKey = minColor.colorKeys;
        GradientAlphaKey[] minColorAlpha = minColor.alphaKeys;
        GradientColorKey[] maxColorKey = maxColor.colorKeys;
        GradientAlphaKey[] maxColorAlpha = maxColor.alphaKeys;
        GradientColorKey[] colorKey;
        GradientAlphaKey[] alphaKey;

        int minColors = minColorKey.Length;
        if (maxColorKey.Length < minColors) minColors = maxColorKey.Length;
        if (minColors > 6) minColors = 6;
        int minAlpha = minColorAlpha.Length;
        if (maxColorAlpha.Length < minAlpha) minAlpha = maxColorAlpha.Length;
        if (minAlpha > 6)minAlpha = 6;


        colorKey = new GradientColorKey[minColors];
        alphaKey = new GradientAlphaKey[minAlpha];

        switch (minColors)
        {
            case 1:
                colorKey[0].color = calcMidColor(minColorKey[0].color, maxColorKey[0].color, value);
                colorKey[0].time = 0.0f;
                break;
            case 2:
                colorKey[0].color = calcMidColor(minColorKey[0].color, maxColorKey[0].color, value);
                colorKey[0].time = 0.0f;
                colorKey[1].color = calcMidColor(minColorKey[1].color, maxColorKey[1].color, value);
                colorKey[1].time = 1.0f;
                break;
            case 3:
                colorKey[0].color = calcMidColor(minColorKey[0].color, maxColorKey[0].color, value);
                colorKey[0].time = 0.0f;
                colorKey[1].color = calcMidColor(minColorKey[1].color, maxColorKey[1].color, value);
                colorKey[1].time = 0.5f;
                colorKey[2].color = calcMidColor(minColorKey[2].color, maxColorKey[2].color, value);
                colorKey[2].time = 1.0f;
                break;
            case 4:
                colorKey[0].color = calcMidColor(minColorKey[0].color, maxColorKey[0].color, value);
                colorKey[0].time = 0.0f;
                colorKey[1].color = calcMidColor(minColorKey[1].color, maxColorKey[1].color, value);
                colorKey[1].time = 0.33f;
                colorKey[2].color = calcMidColor(minColorKey[2].color, maxColorKey[2].color, value);
                colorKey[2].time = 0.67f;
                colorKey[3].color = calcMidColor(minColorKey[3].color, maxColorKey[3].color, value);
                colorKey[3].time = 1.0f;
                break;
            case 5:
                colorKey[0].color = calcMidColor(minColorKey[0].color, maxColorKey[0].color, value);
                colorKey[0].time = 0.0f;
                colorKey[1].color = calcMidColor(minColorKey[1].color, maxColorKey[1].color, value);
                colorKey[1].time = 0.25f;
                colorKey[2].color = calcMidColor(minColorKey[2].color, maxColorKey[2].color, value);
                colorKey[2].time = 0.5f;
                colorKey[3].color = calcMidColor(minColorKey[3].color, maxColorKey[3].color, value);
                colorKey[3].time = 0.75f;
                colorKey[4].color = calcMidColor(minColorKey[4].color, maxColorKey[4].color, value);
                colorKey[4].time = 1.0f;
                break;
            case 6:
                colorKey[0].color = calcMidColor(minColorKey[0].color, maxColorKey[0].color, value);
                colorKey[0].time = 0.0f;
                colorKey[1].color = calcMidColor(minColorKey[1].color, maxColorKey[1].color, value);
                colorKey[1].time = 0.2f;
                colorKey[2].color = calcMidColor(minColorKey[2].color, maxColorKey[2].color, value);
                colorKey[2].time = 0.4f;
                colorKey[3].color = calcMidColor(minColorKey[3].color, maxColorKey[3].color, value);
                colorKey[3].time = 0.6f;
                colorKey[4].color = calcMidColor(minColorKey[4].color, maxColorKey[4].color, value);
                colorKey[4].time = 0.8f;
                colorKey[5].color = calcMidColor(minColorKey[5].color, maxColorKey[5].color, value);
                colorKey[5].time = 1.0f;
                break;
        }
        switch (minAlpha)
        {
            case 1:
                alphaKey[0].alpha = calcMid(minColorAlpha[0].alpha, minColorAlpha[0].alpha, value);
                alphaKey[0].time = 0.0f;
                break;
            case 2:
                alphaKey[0].alpha = calcMid(minColorAlpha[0].alpha, minColorAlpha[0].alpha, value);
                alphaKey[0].time = 0.0f;
                alphaKey[1].alpha = calcMid(minColorAlpha[1].alpha, minColorAlpha[1].alpha, value);
                alphaKey[1].time = 1.0f;
                break;
            case 3:
                alphaKey[0].alpha = calcMid(minColorAlpha[0].alpha, minColorAlpha[0].alpha, value);
                alphaKey[0].time = 0.0f;
                alphaKey[1].alpha = calcMid(minColorAlpha[1].alpha, minColorAlpha[1].alpha, value);
                alphaKey[1].time = 0.5f;
                alphaKey[2].alpha = calcMid(minColorAlpha[2].alpha, minColorAlpha[2].alpha, value);
                alphaKey[2].time = 1.0f;
                break;
            case 4:
                alphaKey[0].alpha = calcMid(minColorAlpha[0].alpha, minColorAlpha[0].alpha, value);
                alphaKey[0].time = 0.0f;
                alphaKey[1].alpha = calcMid(minColorAlpha[1].alpha, minColorAlpha[1].alpha, value);
                alphaKey[1].time = 0.33f;
                alphaKey[2].alpha = calcMid(minColorAlpha[2].alpha, maxColorAlpha[3].alpha, value);
                alphaKey[3].time = 1.0f;
                break;
            case 5:
                alphaKey[0].alpha = calcMid(minColorAlpha[0].alpha, maxColorAlpha[0].alpha, value);
                alphaKey[0].time = 0.0f;
                alphaKey[1].alpha = calcMid(minColorAlpha[1].alpha, maxColorAlpha[1].alpha, value);
                alphaKey[1].time = 0.25f;
                alphaKey[2].alpha = calcMid(minColorAlpha[2].alpha, maxColorAlpha[2].alpha, value);
                alphaKey[2].time = 0.5f;
                alphaKey[3].alpha = calcMid(minColorAlpha[3].alpha, maxColorAlpha[3].alpha, value);
                alphaKey[3].time = 0.75f;
                alphaKey[4].alpha = calcMid(minColorAlpha[4].alpha, maxColorAlpha[4].alpha, value);
                alphaKey[4].time = 1.0f;
                break;
            case 6:
                alphaKey[0].alpha = calcMid(minColorAlpha[0].alpha, maxColorAlpha[0].alpha, value);
                alphaKey[0].time = 0.0f;
                alphaKey[1].alpha = calcMid(minColorAlpha[1].alpha, maxColorAlpha[1].alpha, value);
                alphaKey[1].time = 0.2f;
                alphaKey[2].alpha = calcMid(minColorAlpha[2].alpha, maxColorAlpha[2].alpha, value);
                alphaKey[2].time = 0.4f;
                alphaKey[3].alpha = calcMid(minColorAlpha[3].alpha, maxColorAlpha[3].alpha, value);
                alphaKey[3].time = 0.6f;
                alphaKey[4].alpha = calcMid(minColorAlpha[4].alpha, maxColorAlpha[4].alpha, value);
                alphaKey[4].time = 0.8f;
                alphaKey[5].alpha = calcMid(minColorAlpha[5].alpha, maxColorAlpha[5].alpha, value);
                alphaKey[5].time = 1.0f;
                break;
            }
        g.colorKeys = colorKey;
        g.alphaKeys = alphaKey;
        return g;
    }
    /********************************/
    private Color calcMidColor(Color min, Color max, float percentage)
    {
        return new Color(calcMid(min.r, max.r, percentage),
            calcMid(min.g, max.g, percentage),
            calcMid(min.b, max.b, percentage),
            calcMid(min.a, max.a, percentage));
    }
    /********************************/
    private float calcMid(float min, float max, float percentage)
    {
        return (((max - min) * percentage) + min);
    }
    /********************************/
}
