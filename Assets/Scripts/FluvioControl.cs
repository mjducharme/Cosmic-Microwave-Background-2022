using UnityEngine;
using UnityEngine.VFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class FluvioControl : MonoBehaviour {
    
   	public OSC osc;

    public VisualEffect visualEffect;

    public string amplitudeAddress = "/Amplitude";
    public string visAmplitude = "amplitude";
    //public string partialAddress = "/AmplitudeList";
    //public string visPartial1 = "partial1Amp";
    //public string visPartial2 = "partial2Amp";
    //public string visPartial3 = "partial3Amp";
    //public string visPartial4 = "partial4Amp";

    public string visGradient = "fluvioGradient";

    public string visPosition = "fluvioPosition";
    public string positionAddress = "/Position";

    public Vector3 currentPosition;

    public float speed = 3;

    public int gradientNum = 0;

    private int _curGradientNum = 0;

    public float fadeTime = 1.0f;

    [GradientUsage(true)] public List<Gradient> GradientsQuiet = new List<Gradient>();

    [GradientUsage(true)] public List<Gradient> GradientsLoud = new List<Gradient>();

    public Gradient gradient1;
    
    public Gradient gradient2;

    public Gradient currentGradient;

    private VisualEffect _testVfx;

    private int _amplitudeId;
    //private int _partial1Amp;
    //private int _partial2Amp;
    //private int _partial3Amp;
    //private int _partial4Amp;

    private int _visGradientId;

    private int _visPositionId;

    private GradientControl gc;

    // Use this for initialization
	void Start () {
        _testVfx = GetComponent<VisualEffect>();
        _amplitudeId = Shader.PropertyToID(visAmplitude);
        //_partial2Amp = Shader.PropertyToID(visPartial2);
        //_partial3Amp = Shader.PropertyToID(visPartial3);
        //_partial4Amp = Shader.PropertyToID(visPartial4);
        _visGradientId = Shader.PropertyToID(visGradient);
        _visPositionId = Shader.PropertyToID(visPosition);
        gc = new GradientControl();
        currentPosition = new Vector3(0,0,0);
        osc = GameObject.Find("OSC").GetComponent<OSC>();
        osc.SetAddressHandler( amplitudeAddress , Amplitude );
        osc.SetAddressHandler( positionAddress, SpherePosition);
    }
	
    void OnDestroy() {
        osc.RemoveAddressHandler( amplitudeAddress );
        osc.RemoveAddressHandler( positionAddress );
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L)) {
            gradientNum = (gradientNum + 1) % GradientsQuiet.Count;
        }
        if (Input.GetKeyDown(KeyCode.K)) {
            if (gradientNum == 0) {
                gradientNum = GradientsQuiet.Count - 1;
            } else {
                gradientNum = (gradientNum - 1) % GradientsQuiet.Count;
            }
        }
        if (gradientNum != _curGradientNum) {
            StartCoroutine(colorLerp(_curGradientNum, gradientNum));
        }
	}

    void SpherePosition (OscMessage message) {
        float x = message.GetFloat(0);
        float y = message.GetFloat(1);
        Vector3 v = new Vector3(x,y,0);
        Vector3 pos = Vector3.Lerp(currentPosition, v, 1.0f - Mathf.Exp(-speed * Time.deltaTime));
        currentPosition = pos;
        _testVfx.SetVector3(_visPositionId, pos);
    }

/*
	void PartialAmps(OscMessage message) {
		float x = message.GetFloat(0);
        _testVfx.SetFloat(_partial1Amp, x);
        float y = message.GetFloat(1);
        _testVfx.SetFloat(_partial2Amp, y);
		float z = message.GetFloat(2);
        _testVfx.SetFloat(_partial3Amp, z);
        float a = message.GetFloat(3);
        _testVfx.SetFloat(_partial4Amp, a);
        currentGradient = gc.SetGradient(gradient1, gradient2, (x+y+z+a)*10);
        _testVfx.SetGradient(_visGradientId, currentGradient);
	}
*/
    void Amplitude(OscMessage message) {
		float x = message.GetFloat(0);
        _testVfx.SetFloat(_amplitudeId, x);
        currentGradient = gc.SetGradient(gradient1, gradient2, x*10);
        _testVfx.SetGradient(_visGradientId, currentGradient);
	}

    IEnumerator colorLerp(int currentColor, int newColor) {
        for (float t=0.0f; t<fadeTime; t+=Time.deltaTime) {
            gradient1 = Util.Gradient.Lerp(GradientsQuiet[currentColor], GradientsQuiet[newColor], t/fadeTime);
            gradient2 = Util.Gradient.Lerp(GradientsLoud[currentColor], GradientsLoud[newColor], t/fadeTime);
            yield return null;
        }
        _curGradientNum = newColor;
        yield return null;
    }


}