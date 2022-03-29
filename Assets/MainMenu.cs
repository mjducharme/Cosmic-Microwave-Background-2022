using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject menu;

    public int startScene = 2;
    public GameObject loadingInterface;
    public Image loadingProgressBar;
    //List of the scenes to load from Main Menu
    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
    public void StartGame()
    {
        //HideMenu();
        //ShowLoadingScreen();
        //Load the Scene asynchronously in the background
        //scenesToLoad.Add(SceneManager.LoadSceneAsync("GlobalScene"));
        SceneManager.UnloadSceneAsync("MainMenuScene");
        scenesToLoad.Add(SceneManager.LoadSceneAsync(startScene, LoadSceneMode.Additive));
        GameObject s = GameObject.Find("SceneManager");
        if (s) {
            s.GetComponent<SceneLoader>().currentScene = startScene;
            Debug.Log("Set current scene to " + startScene);
        } else {
            Debug.Log("Scene Manager Not Found");
        }
        //Additive mode adds the Scene to the current loaded Scenes, in this case Gameplay scene
        //scenesToLoad.Add(SceneManager.LoadSceneAsync("Level01Part01", LoadSceneMode.Additive));
        //StartCoroutine(LoadingScreen());
    }

    //dropdown
    public void ChangeStartScene(TMP_Dropdown dropDown)
    {
        Debug.Log("DROP DOWN CHANGED -> " + dropDown.value);
        startScene = dropDown.value + 1;
    }

    public void StartGameSO()
    {
        HideMenu();
        ShowLoadingScreen();
        //Load the Scene asynchronously in the background
        //scenesToLoad.Add(SceneManager.LoadSceneAsync("Gameplay"));
        //Additive mode adds the Scene to the current loaded Scenes, in this case Gameplay scene
        //scenesToLoad.Add(SceneManager.LoadSceneAsync("Level01Part01", LoadSceneMode.Additive));
        StartCoroutine(LoadingScreen());
    }

    public void HideMenu()
    {
        menu.SetActive(false);
    }

    public void ShowLoadingScreen()
    {
        loadingInterface.SetActive(true);
    }

    IEnumerator LoadingScreen()
    {
        float totalProgress=0;
        //Iterate through all the scenes to load
        for(int i=0; i<scenesToLoad.Count; ++i)
        {
            while (!scenesToLoad[i].isDone)
            {
                //Adding the scene progress to the total progress
                totalProgress += scenesToLoad[i].progress;
                //the fillAmount needs a value between 0 and 1, so we devide the progress by the number of scenes to load
                loadingProgressBar.fillAmount = totalProgress/scenesToLoad.Count;
                yield return null;
            }
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
