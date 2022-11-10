using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager i;
    public Scene ActualScene { get; private set; }

    private Action actionAfterShowLoadingScreen;
    private Scene nextScene;

    private void Awake()
    {
        if (GameSceneManager.i == null)
        {
            i = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public enum Scene
    {
        MainMenu,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        Level7,
        Level8,
        Level9,
        Level10,
        Level11,
        Level12,
        Level13,
        Level14,
        Level15,
        Level16,
        TutorialLevel,
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene((int)scene);
        SetActualScene(scene);
    }

    public void LoadScene(int sceneIndex)
    {
        LoadScene((Scene)sceneIndex);
    }

    public void LoadSceneWithLoadingScreen(Scene scene)
    {
        SetActualScene(scene);
        ShowLoadingScreen(null);
    }

    public void LoadSceneWithLoadingScreen(int sceneIndex)
    {
        LoadSceneWithLoadingScreen((Scene)sceneIndex);
    }

    public void ShowLoadingScreen(Action actionAfterShowed)
    {
        actionAfterShowLoadingScreen = actionAfterShowed;
        LoadingScreen.i.OnLoadingScreenShowed += LoadingScreenController_OnLoadingScreenShowed;
        LoadingScreen.i.SetGameobjectState(true);
        LoadingScreen.i.ShowLoadingScreen();
    }

    private void LoadingScreenController_OnLoadingScreenShowed(object sender, EventArgs e)
    {
        LoadingScreen.i.OnLoadingScreenShowed -= LoadingScreenController_OnLoadingScreenShowed;
        if (actionAfterShowLoadingScreen == null)
        {
            LoadScene(nextScene);
        }
        else
        {
            actionAfterShowLoadingScreen();
            actionAfterShowLoadingScreen = null;
        }
        LoadingScreen.i.HideLoadingScreen();
    }

    private void SetActualScene(Scene scene)
    {
        ActualScene = scene;
    }

}
