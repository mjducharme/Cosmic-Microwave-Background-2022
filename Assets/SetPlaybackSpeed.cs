using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SetPlaybackSpeed : MonoBehaviour
{
    [SerializeField] float timeScale = 2.0f;
    [SerializeField] VisualEffect VFX;
 
    void Start()
    {
        VFX = GetComponent<VisualEffect>();
    }
 
    void Update()
    {
        VFX.playRate = timeScale;
    }
}
