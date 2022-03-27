using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLight : MonoBehaviour
{

    public int maxCounter = 1000;

    private Vector3 angle;
    private float randomX = 0f;
    private float randomY = 0f;
    private float incX = 0;
    private float incY = 0;
    private int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        CalcNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        randomX = randomX + incX;
        randomY = randomY + incY;
        counter++;

        transform.localEulerAngles = new Vector3(randomX, randomY, 0.0f);
        if (counter >= maxCounter) CalcNewDestination();
  

    }
    void CalcNewDestination()
    {
        float oldX = randomX;
        float oldY = randomY;
        float destX = Random.Range(-15, 15);
        float destY = Random.Range(-15, 15);
        incX = (destX - oldX) / maxCounter;
        incY = (destY - oldY) / maxCounter;
        counter = 0;
    }
}
