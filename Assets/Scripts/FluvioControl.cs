using UnityEngine;
using UnityEngine.VFX;
using System.Collections;

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
        osc.SetAddressHandler( amplitudeAddress , Amplitude );
        osc.SetAddressHandler( positionAddress, SpherePosition);
    }
	
	// Update is called once per frame
	void Update () {
        
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


}