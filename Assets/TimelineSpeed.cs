using UnityEngine;
using UnityEngine.Playables;
public class TimelineSpeed : MonoBehaviour
{
    public float newSpeed = 1.0f;
    public PlayableDirector pd;
    public OSC osc;

    public string oscAddress = "/TimelineSpeed";

    void Start()
    {
        pd = GetComponent<PlayableDirector>();
        osc = GameObject.Find("OSC").GetComponent<OSC>();
        osc.SetAddressHandler( oscAddress, TimelineSpeedOSC );
    }

    void TimelineSpeedOSC (OscMessage message) {
        newSpeed = message.GetFloat(0); 
    }

    void OnDestroy() {
        osc.RemoveAddressHandler( oscAddress );
    }

    void Update() {
        pd.playableGraph.GetRootPlayable(0).SetSpeed(newSpeed);
    }
}
