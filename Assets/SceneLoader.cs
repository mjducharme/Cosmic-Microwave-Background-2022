using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Artngame.SKYMASTER;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    public Camera mainCamera;

    public GameObject nebula;

    public GameObject pointLight;

    public GameObject menuGameObject;

    public GameObject sceneTransitionImage;

    public int currentScene = 0;

    public int selectedScene = 1;

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();


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
    public void LoadScene(int sceneNum) {
        if (sceneNum != currentScene) {
            Debug.Log("LoadScene Current Scene is: " + currentScene);
            Debug.Log("LoadScene New Scene is: " + sceneNum);

            if (currentScene == 0) {
                HideMenu();
            }
            if (sceneNum != 0) {
                scenesToLoad.Add(SceneManager.LoadSceneAsync(sceneNum, LoadSceneMode.Additive));
                sceneTransitionImage.SetActive(true);
            } else {
                menuGameObject.SetActive(true);
                sceneTransitionImage.SetActive(false);
            }
            scenesToLoad.Remove(SceneManager.UnloadSceneAsync(currentScene));
            if (sceneNum == 3) {
                nebula.SetActive(true);
                pointLight.SetActive(true);
                mainCamera.GetComponent<connectSuntoNebulaCloudsHDRP>().enabled=true;
            } else {
                nebula.SetActive(false);
                pointLight.SetActive(false);
                mainCamera.GetComponent<connectSuntoNebulaCloudsHDRP>().enabled=false;
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
        //currentScene = SceneManager.GetActiveScene().buildIndex;
        //Debug.Log("Current scene is: " + currentScene);
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
