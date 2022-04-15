using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public transitionType sceneStartTransitionMethod = transitionType.Crossfade;

    public int sceneStartFadeAlpha = 1;
    public float sceneStartFadeSpeed = 1f;
    public float sceneStartFadeExponent = 4;

    public transitionType sceneEndTransitionMethod = transitionType.Crossfade;

    public int sceneEndFadeAlpha = 0;
    public float sceneEndFadeSpeed = 1f;
    public float sceneEndFadeExponent = 4;

    // Start is called before the first frame update
    void Start()
    {
        _sceneId = gameObject.scene.buildIndex;
        Debug.Log("Start called for scene " + _sceneId);
        EventsManager.instance.PrepareSceneUnload += PrepareSceneUnload;
        if (sceneStartTransitionMethod == transitionType.Crossfade) {
            Debug.Log("About to start crossfade " + _sceneId);
            FadeInOut _sceneFade = GetComponent<FadeInOut>();
            _sceneFade.DirectAlpha(sceneStartFadeAlpha, sceneStartFadeSpeed, sceneStartFadeExponent);
        }
    }

    void PrepareSceneUnload(int id) {
        if (id == _sceneId) {
            Debug.Log("called preparesceneunload for scene " + _sceneId);
            if (sceneEndTransitionMethod == transitionType.Crossfade) {
                FadeInOut _sceneFade = GetComponent<FadeInOut>();
                _sceneFade.DirectAlpha(sceneEndFadeAlpha, sceneEndFadeSpeed, sceneEndFadeExponent, true);
            }
        }
        Debug.Log("New Test Message");
    }
}
