using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GradientTexture : MonoBehaviour {

    public Gradient gradient;
    public string visGradient = "Gradient";
    public string visTexture = "GradientTexture";
    [Header("Type of Gradient")]
    [Tooltip("0 = downward to black")]
    public int type = 0;
 
    private VisualEffect _Vfx;

    void Start () {
        _Vfx = GetComponent<VisualEffect>();

        /*float width = 64;
        float height = 1;
 
        Texture2D texture = new Texture2D(Mathf.CeilToInt(width), Mathf.CeilToInt(height));
        texture.alphaIsTransparency = true;
 
        Color color;

        for(int x = 0; x < Mathf.CeilToInt(width); x++) {
            color = gradient.Evaluate(x / 64F);
            print("X: " + x + " | R: " + color.r + " G: " + color.g + " B: " + color.b + " A: " + color.a);
            texture.SetPixel(Mathf.CeilToInt(x), Mathf.CeilToInt(0), color);
        }

        _Vfx.SetTexture("GradientTexture", texture);*/
    }

    void Update () {
        gradient = GetComponent<FluvioControl>().currentGradient;

        int width = 64;
        int height = 1;
 
        Texture2D texture = new Texture2D(width, height);
        //texture.alphaIsTransparency = true;
 
        Color color;

        for(int x = 0; x < width; x++) {
            color = gradient.Evaluate(x / 64F);
            //print("X: " + x + " | R: " + color.r + " G: " + color.g + " B: " + color.b + " A: " + color.a);
            texture.SetPixel(x, 0, color);
        }

        texture.Apply();

        _Vfx.SetTexture("GradientTexture", texture);
    }

 
}
 
