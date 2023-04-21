using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class OSCVFXSetFloatLerp : MonoBehaviour
{
    public OSC osc;

    private VisualEffect _Vfx;

    public string visFloat = "Bleh";

    public string floatAddress = "/SectionD/Bleh";

    public float lerpTime = 0.1f;

    private int _visFloatId;

    private bool lerpingNow = false;
    // Start is called before the first frame update
    void Start()
    {
        _Vfx = GetComponent<VisualEffect>();
        _visFloatId = Shader.PropertyToID(visFloat);
        osc = GameObject.Find("OSC").GetComponent<OSC>();
        osc.SetAddressHandler( floatAddress, VisFloat );
    }

    void VisFloat (OscMessage message) {
        //_Vfx.SetFloat(_visFloatId, message.GetFloat(0));
        if (!lerpingNow) {
            StartCoroutine(MoveOverSecondsFloat(_Vfx.GetFloat(_visFloatId), message.GetFloat(0), lerpTime));
        }
    }

    void OnDestroy() {
        osc.RemoveAddressHandler( floatAddress );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator MoveOverSpeed (GameObject objectToMove, Vector3 end, float speed)
    {
        // speed should be 1 unit per second
        while (objectToMove.transform.position != end)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame ();
        }
    }
    
    public IEnumerator MoveOverSeconds (GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.position = end;
    }

    public IEnumerator MoveOverSecondsFloat (float start, float end, float seconds)
    {
        lerpingNow = true;
        float elapsedTime = 0;
        while (elapsedTime < seconds)
        {
            _Vfx.SetFloat(_visFloatId, Mathf.Lerp(start, end, (elapsedTime / seconds)));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _Vfx.SetFloat(_visFloatId, end);
        lerpingNow = false;
    }
}

