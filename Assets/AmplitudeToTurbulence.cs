using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class AmplitudeToTurbulence : MonoBehaviour
{
    public OSC osc;

    private VisualEffect _Vfx;

    public string visTurbulenceIntensity = "SecondTurbulenceIntensity";

    public string turbulenceIntensityAddress = "/SectionD/SecondTurbulenceIntensity";

    private int _visTurbulenceIntensityId;
    // Start is called before the first frame update
    void Start()
    {
        _Vfx = GetComponent<VisualEffect>();
        _visTurbulenceIntensityId = Shader.PropertyToID(visTurbulenceIntensity);
        osc = GameObject.Find("OSC").GetComponent<OSC>();
        osc.SetAddressHandler( turbulenceIntensityAddress, TurbulenceIntensity );
    }

    void TurbulenceIntensity (OscMessage message) {
        _Vfx.SetFloat(_visTurbulenceIntensityId, message.GetFloat(0));
    }

    void OnDestroy() {
        osc.RemoveAddressHandler( turbulenceIntensityAddress );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
