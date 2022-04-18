using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TriggerParticleSystem : MonoBehaviour
{
    private VisualEffect _Vfx;
    public string triggerName = "OnTrigger";
    public OSC osc;

    public string oscAddress = "/OnTrigger";

    private int _visTriggerId;

    // Start is called before the first frame update
    void Start()
    {
        _Vfx = GetComponent<VisualEffect>();
        _visTriggerId = Shader.PropertyToID(triggerName);
        osc = GameObject.Find("OSC").GetComponent<OSC>();
        osc.SetAddressHandler( oscAddress, TriggerVFX );
    }

    void TriggerVFX(OscMessage oscMessage) {
        _Vfx.SendEvent(_visTriggerId);
    }

    void OnDestroy() {
        osc.RemoveAddressHandler( oscAddress );
    }

}
