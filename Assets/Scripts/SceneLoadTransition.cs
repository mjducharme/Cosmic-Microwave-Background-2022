using UnityEngine;
using UnityEngine.UI;
using MyBox;

public class SceneLoadTransition : MonoBehaviour
{
    // instanceid used for the event manager
    private int _sceneId;
    // default transition is a particle system fade
    public enum transitionType // your custom enumeration
    {
        None,
        Crossfade, 
        FadeAlpha
    };

    private SceneLoader _sceneLoader;

    public transitionType sceneStartTransitionMethod = transitionType.Crossfade;

    [ConditionalField(nameof(sceneStartTransitionMethod), false, transitionType.FadeAlpha)] public Color sceneStartFadeColor = Color.black;
    [ConditionalField(nameof(sceneStartTransitionMethod), true, transitionType.None)] public int sceneStartFadeAlpha = 1;
    [ConditionalField(nameof(sceneStartTransitionMethod), true, transitionType.None)] public float sceneStartFadeSpeed = 1f;
    [ConditionalField(nameof(sceneStartTransitionMethod), true, transitionType.None)] public float sceneStartFadeExponent = 4;

    public transitionType sceneEndTransitionMethod = transitionType.Crossfade;

    [ConditionalField(nameof(sceneEndTransitionMethod), false, transitionType.FadeAlpha)] public Color sceneEndFadeColor = Color.black;

    [ConditionalField(nameof(sceneEndTransitionMethod), true, transitionType.None)] public int sceneEndFadeAlpha = 0;
    [ConditionalField(nameof(sceneEndTransitionMethod), true, transitionType.None)] public float sceneEndFadeSpeed = 1f;
    [ConditionalField(nameof(sceneEndTransitionMethod), true, transitionType.None)] public float sceneEndFadeExponent = 4;

    // Start is called before the first frame update
    void Start()
    {
        _sceneLoader = GameObject.Find("SceneManager").GetComponent<SceneLoader>();
        _sceneId = gameObject.scene.buildIndex;
        Debug.Log("Start called for scene " + _sceneId);
        EventsManager.instance.PrepareSceneUnload += PrepareSceneUnload;
        if (sceneStartTransitionMethod == transitionType.Crossfade) {
            _sceneLoader.currentSceneEndTransitionMethod = transitionType.Crossfade;
            Debug.Log("About to start scene start crossfade " + _sceneId);
            FadeInOut _sceneFade = GetComponent<FadeInOut>();
            _sceneFade.DirectAlpha(sceneStartFadeAlpha, sceneStartFadeSpeed, sceneStartFadeExponent);

            // in case loading some unexpected scene (out of order), reset the transition image to a reasonable default
            Image _sceneFadeAlpha = GameObject.Find("SceneTransitionImage").GetComponent<Image>();
            Color myColor = Color.black;
            myColor.a = 0;
            _sceneFadeAlpha.color = myColor;
        } else if (sceneStartTransitionMethod == transitionType.FadeAlpha) {
            _sceneLoader.currentSceneEndTransitionMethod = transitionType.FadeAlpha;
            Debug.Log("About to start scene end fadealpha " + _sceneId);
            SceneTransition _sceneFadeAlpha = GameObject.Find("SceneTransitionImage").GetComponent<SceneTransition>();
            _sceneFadeAlpha.FadeToColor(_sceneId, sceneStartFadeColor, sceneStartFadeAlpha, sceneStartFadeSpeed, sceneStartFadeExponent);
        } else if (sceneStartTransitionMethod == transitionType.None) {
            // in case loading some unexpected scene (out of order), reset the transition image to a reasonable default
            Image _sceneFadeAlpha = GameObject.Find("SceneTransitionImage").GetComponent<Image>();
            Color myColor = Color.black;
            myColor.a = 0;
            _sceneFadeAlpha.color = myColor;
        }
        _sceneLoader.currentSceneEndTransitionMethod = sceneEndTransitionMethod;
    }

    void OnDestroy() {
        EventsManager.instance.PrepareSceneUnload -= PrepareSceneUnload;
    }

    void PrepareSceneUnload(int id) {
        if (id == _sceneId) {
            Debug.Log("called preparesceneunload for scene " + _sceneId);
            if (this) {
                Debug.Log("This is good for preparesceneunload for scene " + _sceneId);
                if (sceneEndTransitionMethod == transitionType.Crossfade) {
                    Debug.Log("About to start scene end crossfade " + _sceneId);
                    FadeInOut _sceneFade = GetComponent<FadeInOut>();
                    _sceneFade.DirectAlpha(sceneEndFadeAlpha, sceneEndFadeSpeed, sceneEndFadeExponent, true);
                } else if (sceneEndTransitionMethod == transitionType.FadeAlpha) {
                    Debug.Log("About to start scene end fadealpha " + _sceneId);
                    SceneTransition _sceneFadeAlpha = GameObject.Find("SceneTransitionImage").GetComponent<SceneTransition>();
                    _sceneFadeAlpha.FadeToColor(id, sceneEndFadeColor, sceneEndFadeAlpha, sceneEndFadeSpeed, sceneEndFadeExponent, true);
                } else if (sceneEndTransitionMethod == transitionType.None) {
                    // If there is no unload transition, scene should be ready to unload immediately
                    EventsManager.instance.OnSceneReadyToUnload(_sceneId);
                }
            } else {
                Debug.LogWarning("Forcing Unload - Something went wrong with preparesceneunload for scene " + _sceneId);
                EventsManager.instance.OnSceneReadyToUnload(_sceneId);
            }
        }
        //Debug.Log("New Test Message");
    }
}
