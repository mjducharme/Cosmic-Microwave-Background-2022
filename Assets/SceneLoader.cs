using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Artngame.SKYMASTER;
using TMPro;
using UnityEngine.VFX;

public class SceneLoader : MonoBehaviour
{
    public OSC osc;

    public Camera mainCamera;

    public GameObject nebula;

    public GameObject pointLight;

    public GameObject menuGameObject;

    public GameObject sceneTransitionImage;

    public string changeSceneAddress = "/ChangeScene";

    public int currentScene = 0;

    public int selectedScene = 1;

    List<int> sceneReadyToUnload = new List<int>();

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    public SceneLoadTransition.transitionType currentSceneEndTransitionMethod = SceneLoadTransition.transitionType.None;


    // called first
    void OnEnable()
    {
        Debug.Log("OnEnable called");
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }


    public void StartGame()
    {
        //HideMenu();
        //ShowLoadingScreen();
        //Load the Scene asynchronously in the background
        //scenesToLoad.Add(SceneManager.LoadSceneAsync("GlobalScene"));
        /*SceneManager.UnloadSceneAsync("MainMenuScene");
        scenesToLoad.Add(SceneManager.LoadSceneAsync(startScene, LoadSceneMode.Additive));
        GameObject s = GameObject.Find("SceneManager");
        if (s) {
            s.GetComponent<SceneLoader>().currentScene = startScene;
            Debug.Log("Set current scene to " + startScene);
        } else {
            Debug.Log("Scene Manager Not Found");
        }*/
        
        LoadScene(selectedScene);
        //Additive mode adds the Scene to the current loaded Scenes, in this case Gameplay scene
        //scenesToLoad.Add(SceneManager.LoadSceneAsync("Level01Part01", LoadSceneMode.Additive));
        //StartCoroutine(LoadingScreen());
    }

/*    IEnumerator LoadYourAsyncScene(int sceneNum)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneNum);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }*/


    IEnumerator UnloadSceneWhenReady(int id, int sceneNum, bool isCrossfade) {
        while (!sceneReadyToUnload.Contains(id)) {
            Debug.Log("scene is not ready to unload, scene id " + id);
            yield return null;
        }
        scenesToLoad.Remove(SceneManager.UnloadSceneAsync(id));
        sceneReadyToUnload.Remove(id);
        if (!isCrossfade && sceneNum != 0)
            scenesToLoad.Add(SceneManager.LoadSceneAsync(sceneNum, LoadSceneMode.Additive));
        if (sceneNum != 3) {
            nebula.SetActive(false);
            pointLight.SetActive(false);
            mainCamera.GetComponent<connectSuntoNebulaCloudsHDRP>().enabled=false;
        } 
    }

    void SceneReadyToUnload (int sceneNum) {
        Debug.Log("ready to unload scene " + sceneNum);
        sceneReadyToUnload.Add(sceneNum);
    }

    public void LoadScene(int sceneNum) {
        if (sceneNum != currentScene) {
            Debug.Log("LoadScene Current Scene is: " + currentScene);
            Debug.Log("LoadScene New Scene is: " + sceneNum);

            if (currentScene == 0) {
                HideMenu();
            }
            if (sceneNum != 0) {
                // if we need to crossfade, we need to load the new scene early, before the old scene is unloaded
                if ((currentSceneEndTransitionMethod == SceneLoadTransition.transitionType.Crossfade) || (currentScene == 0))
                    scenesToLoad.Add(SceneManager.LoadSceneAsync(sceneNum, LoadSceneMode.Additive));
                sceneTransitionImage.SetActive(true);
            } else {
                menuGameObject.SetActive(true);
                sceneTransitionImage.SetActive(false);
            }
            
            if (currentScene != 0) {
                EventsManager.instance.OnPrepareSceneUnload(currentScene);
                /*if (currentScene == 1 && sceneNum == 2) {
                    // Going from scene 1 to 2 we want to crossfade
                    FadeInOut _scene1Fade = GameObject.Find("FluvioFX Fluid").GetComponent<FadeInOut>();
                    _scene1Fade.FadeChange(0, 0.1f, 4);
                }*/

                bool isCrossfade = false;

                if (currentSceneEndTransitionMethod == SceneLoadTransition.transitionType.Crossfade)
                    isCrossfade = true;

                StartCoroutine(UnloadSceneWhenReady(currentScene, sceneNum, isCrossfade));
                //Debug.Log("After scene is ready to be unloaded, scene " + currentScene);

                
            }

            if (sceneNum == 3) {
                nebula.SetActive(true);
                pointLight.SetActive(true);
                mainCamera.GetComponent<connectSuntoNebulaCloudsHDRP>().enabled=true;
            }

            currentScene = sceneNum;
        }
    }


    public void HideMenu()
    {
        menuGameObject.SetActive(false);
    }

    public void ShowMenu()
    {
        menuGameObject.SetActive(true);
    }
    public void ChangeStartScene(TMP_Dropdown dropDown)
    {
        Debug.Log("DROP DOWN CHANGED -> " + dropDown.value);
        selectedScene = dropDown.value + 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        osc.SetAddressHandler( changeSceneAddress, ChangeSceneOSC);
        EventsManager.instance.SceneReadyToUnload += SceneReadyToUnload;
        //currentScene = SceneManager.GetActiveScene().buildIndex;
        //Debug.Log("Current scene is: " + currentScene);
    }

    void ChangeSceneOSC(OscMessage message) {
        Debug.Log ("Received OSC Change Scene command to go to scene " + message.GetInt(0));
        LoadScene(message.GetInt(0));
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.C)) {
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                LoadScene(1);
            } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
                LoadScene(2);
            } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
                LoadScene(3);
            } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
                LoadScene(4);
            } else if (Input.GetKeyDown(KeyCode.Alpha5)) {
                LoadScene(5);
            } else if (Input.GetKeyDown(KeyCode.Q)) {
                LoadScene(0);
            } 
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
