using System;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public static EventsManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this);
        }
    }

    public event Action<int> PrepareSceneUnload;
    public event Action<int> SceneReadyToUnload;

    public void OnPrepareSceneUnload(int sceneNum)
    {
        PrepareSceneUnload?.Invoke(sceneNum);
    }

    public void OnSceneReadyToUnload(int sceneNum)
    {
        SceneReadyToUnload?.Invoke(sceneNum);
    }
}
