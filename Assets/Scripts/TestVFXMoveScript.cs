using UnityEngine;
using UnityEngine.VFX;
using System.Collections;

public class TestVFXMoveScript: MonoBehaviour {
    
   	public OSC osc;

    private VisualEffect _testVfx;

    private int _sineMax;
	// Use this for initialization
	private void Start () {
        _testVfx = GetComponent<VisualEffect>();
       _sineMax = Shader.PropertyToID("sineMax");
       osc.SetAddressHandler( "/SineMax" , OnReceiveSineMax );
    }
	
	// Update is called once per frame
	private void Update () {
	
	}

	private void OnReceiveSineMax(OscMessage message){
		float x = message.GetFloat(0);
        _testVfx.SetFloat(_sineMax, x);
        
	}

}
