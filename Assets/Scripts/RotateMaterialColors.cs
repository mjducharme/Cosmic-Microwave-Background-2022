using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMaterialColors : MonoBehaviour
{
    // Start is called before the first frame update

    public Color color1;
    public Color color2;
    public Color color3;
    public Color mix;
    private int counter = 0;
    private int maxCounter = 5000;
    private int increment = 1;
    private Material mat;

    void Start()
    {
        counter = 0;
        mat = gameObject.GetComponent<Renderer>().material;

    }

    private void FixedUpdate()
    {
        float percent; 

        if (counter > maxCounter / 2)
        {
            percent = (float)(counter - (maxCounter / 2)) / (float)(maxCounter / 2);
            mix = new Color(calcMid(color2.r, color3.r, percent),
                calcMid(color2.g, color3.g, percent),
                calcMid(color2.b, color3.b, percent),
                calcMid(color2.a, color3.a, percent));
            mat.SetColor("_BaseColor", mix);
        }
        else
        {
            percent = (float)counter / (float)(maxCounter / 2);
            mix = new Color(calcMid(color1.r, color2.r, percent),
                calcMid(color1.g, color2.g, percent),
                calcMid(color1.b, color2.b, percent),
                calcMid(color1.a, color2.a, percent));
            mat.SetColor("_BaseColor", mix);
        }
     
        counter += increment;
        if (counter >= maxCounter) {
            increment = -1;
        }
        else if (counter < 0)
        {
            increment = 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }


    float calcMid(float min, float max, float percentage)
    {
        return (((max - min) * percentage) + min);
    }

}
