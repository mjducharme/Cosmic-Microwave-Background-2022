using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Artngame.SKYMASTER;
using UnityEngine.VFX;

public class SectionCColorController : MonoBehaviour
{
    public connectSuntoNebulaCloudsHDRP _nebulaClouds;
    //public GameObject gamePointLight;
    public Light _pointLight;
    private bool _pointLightFound = false;

    public VisualEffect VisualEffect1;
    public VisualEffect VisualEffect2;
    public VisualEffect VisualEffect3;

    private VisualEffect _vfx1;
    private VisualEffect _vfx2;
    private VisualEffect _vfx3;

    private int _vfx1Gradient;
    private int _vfx2Color;
    private int _vfx2ElectGradient;

    private int _currentColor = 0;
    public int setNewColor = 0;

    [GradientUsage(true)] public List<Gradient> Vfx1Gradients = new List<Gradient>();
    [ColorUsage(true, true)] public List<Color> BallColors = new List<Color>();
    [GradientUsage(true)] public List<Gradient> ElectGradients = new List<Gradient>();

    public List<Color> CloudTopColors = new List<Color>();
    public List<Color> HighSunColors = new List<Color>();
    public List<Color> PointLightColors = new List<Color>();
    // Start is called before the first frame update
    void Start()
    {
        _vfx1 = VisualEffect1.GetComponent<VisualEffect>();
        _vfx2 = VisualEffect2.GetComponent<VisualEffect>();
        _vfx1Gradient = Shader.PropertyToID("Gradient");
        _vfx2Color = Shader.PropertyToID("Color");
        _vfx2ElectGradient = Shader.PropertyToID("ElectGradient");
        _nebulaClouds = GameObject.Find("Main Camera").GetComponent<connectSuntoNebulaCloudsHDRP>();
        //gamePointLight = GameObject.FindWithTag("PointLightTag");
        //_vfxElectGradient = ElectGradients[currentColor];
        _vfx1.SetGradient(_vfx1Gradient, Vfx1Gradients[setNewColor]);
        _vfx2.SetVector4(_vfx2Color, BallColors[setNewColor]);
        _vfx2.SetGradient(_vfx2ElectGradient, ElectGradients[setNewColor]);
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
            _vfx1.SetGradient(_vfx1Gradient, Vfx1Gradients[newColor]);
            _vfx2.SetVector4(_vfx2Color, BallColors[newColor]);
            _vfx2.SetGradient(_vfx2ElectGradient, ElectGradients[newColor]);
            _nebulaClouds.cloudTopColor = CloudTopColors[newColor];
            _nebulaClouds.highSunColor = HighSunColors[newColor];
            _pointLight.color = PointLightColors[newColor];
            _currentColor = newColor;
        }
    }
}