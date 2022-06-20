using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Artngame.SKYMASTER;
using UnityEngine.VFX;

public class ElectricOrbColorChange : MonoBehaviour
{
    public connectSuntoNebulaCloudsHDRP _nebulaClouds;
    //public GameObject gamePointLight;
    public Light _pointLight;
    private bool _pointLightFound = false;

    private VisualEffect _vfx;
    private int _vfxColor;
    private int _vfxElectGradient;

    private int _currentColor = 0;
    public int setNewColor = 0;

    [ColorUsage(true, true)] public List<Color> BallColors = new List<Color>();
    [GradientUsage(true)] public List<Gradient> ElectGradients = new List<Gradient>();
    public List<Color> CloudTopColors = new List<Color>();
    public List<Color> HighSunColors = new List<Color>();
    public List<Color> PointLightColors = new List<Color>();
    // Start is called before the first frame update
    void Start()
    {
        _vfx = GetComponent<VisualEffect>();
        _vfxColor = Shader.PropertyToID("Color");
        _vfxElectGradient = Shader.PropertyToID("ElectGradient");
        _nebulaClouds = GameObject.Find("Main Camera").GetComponent<connectSuntoNebulaCloudsHDRP>();
        //gamePointLight = GameObject.FindWithTag("PointLightTag");
        //_vfxElectGradient = ElectGradients[currentColor];
        _vfx.SetVector4(_vfxColor, BallColors[setNewColor]);
        _vfx.SetGradient(_vfxElectGradient, ElectGradients[setNewColor]);
        _nebulaClouds.cloudTopColor = CloudTopColors[setNewColor];
        _nebulaClouds.highSunColor = HighSunColors[setNewColor];
        //_pointLight.Color = PointLightColors[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (!_pointLightFound) {
            _pointLight = GameObject.Find("PointLight").GetComponent<Light>();
            if (_pointLight != null) {
                _pointLightFound = true;
                _pointLight.color = PointLightColors[setNewColor];
            }
        }
        if (Input.GetKeyDown(KeyCode.L)) {
            setNewColor++;
        }
        if (Input.GetKeyDown(KeyCode.K)) {
            setNewColor--;
        }
        if (setNewColor != _currentColor) {
            if (setNewColor < 0) {
                setNewColor = 0;
            }
            setNewColor = setNewColor % BallColors.Count;
            //setNewColor = Mathf.Clamp(setNewColor, 0, BallColors.Count - 1);
            ChangeColorTo(setNewColor);
        }
        //if (Input.GetKeyDown(KeyCode.L)) {
        //    setNewColor++;
        //    ChangeColorTo(setNewColor);
        //}
        //if (Input.GetKeyDown(KeyCode.K)) {
        //    setNewColor--;
        //    ChangeColorTo(setNewColor);
        //}
    }

    void ChangeColorTo(int newColor) {
        Debug.Log("Changing color");
        if (newColor != _currentColor) {
            _vfx.SetVector4(_vfxColor, BallColors[newColor]);
            _vfx.SetGradient(_vfxElectGradient, ElectGradients[newColor]);
            _nebulaClouds.cloudTopColor = CloudTopColors[newColor];
            _nebulaClouds.highSunColor = HighSunColors[newColor];
            _pointLight.color = PointLightColors[newColor];
            _currentColor = newColor;
        }
    }
}
