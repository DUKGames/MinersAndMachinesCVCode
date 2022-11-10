using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager i;
    public PanelType ActualPanel { get; set; }

    [Header("References")]
    [SerializeField] private AnimatorHandler animatorHandler;
    [SerializeField] private CameraMove cameraMove;
    [SerializeField] private UICredits uiCredits;
    [SerializeField] private UINewsletter uiNewsletter;
    [SerializeField] private OptionsHandler optionsHandler;

    private void Awake()
    {
        i = this;
    }

    private void Start()
    {
        CheckStartScene();
        SetupMusic();
    }

    // Change to new Input System
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
            CheckEscapeAction();
    }

    public enum PanelType
    {
        MainMenu,
        Credits,
        Newsletter,
        Options,
        OptionsConfirmRest,
        OptionsPrivacyPolicy,
        MapMenu,
        MapMenuBankInfo,
        Ranking,
        Upgrade,
    }

    public void LoadScene(int sceneIndex)
    {
        GameSceneManager.i.LoadSceneWithLoadingScreen(sceneIndex);
    }

    public void ShowMapFromMainMenu()
    {
        ActualPanel = PanelType.MapMenu;
        animatorHandler.TriggerAnimator("ShowMapFromMainMenu");
        cameraMove.SetIsCameraMovable(true);
    }

    public void ShowUpgradeMenu()
    {
        ActualPanel = PanelType.Upgrade;
        animatorHandler.TriggerAnimator("ShowUpgradeMenu");
        cameraMove.SetIsCameraMovable(false);
    }

    public void HideUpgradeMenu()
    {
        ActualPanel = PanelType.MapMenu;
        animatorHandler.TriggerAnimator("HideUpgrade");
        cameraMove.SetIsCameraMovable(true);
    }

    public void ShowRanking()
    {
        ActualPanel = PanelType.Ranking;
        animatorHandler.TriggerAnimator("ShowRanking");
        cameraMove.SetIsCameraMovable(false);
    }

    public void HideRanking()
    {
        ActualPanel = PanelType.MapMenu;
        animatorHandler.TriggerAnimator("HideRanking");
        cameraMove.SetIsCameraMovable(true);
    }

    public void HideMap()
    {
        ActualPanel = PanelType.MainMenu;
        animatorHandler.TriggerAnimator("HideMap");
        cameraMove.OnCameraWentToDestination += CameraMove_OnCameraWentToDestination;
        cameraMove.SetNewXCameraPosition(0f);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void CheckEscapeAction()
    {
        switch (ActualPanel)
        {
            case PanelType.MainMenu: 
                break;
            case PanelType.Credits: 
                uiCredits.HideCreditMenu(); 
                break;
            case PanelType.Newsletter: 
                uiNewsletter.HideNewsletterPanel(); 
                break;
            case PanelType.Options: 
                optionsHandler.HideOptions(); 
                break;
            case PanelType.OptionsConfirmRest: 
                optionsHandler.HideConfirmResetProgressPanel(); 
                break;
            case PanelType.OptionsPrivacyPolicy: 
                optionsHandler.HidePrivacyPolicy(); 
                break;
            case PanelType.MapMenu: 
                HideMap(); 
                break;
            case PanelType.MapMenuBankInfo: 
                break;
            case PanelType.Ranking: 
                HideRanking();
                break;
            case PanelType.Upgrade: 
                HideUpgradeMenu();
                break;
            default: break;
        }
    }

    private void CheckStartScene()
    {
        if(CodeTools.i.GetPlayerPrefsIntValue("backFromGame") != -1)
        {
            ShowMapFromMainMenu();
            CodeTools.i.DeletePlayerPrefs("backFromGame");
        }
        else
        {
            ActualPanel = PanelType.MainMenu;
        }
    }

    private void SetupMusic()
    {
        SoundManager.i.StopMusic(SoundManager.MusicType.GameMusic);
        SoundManager.i.StopMusic(SoundManager.MusicType.GameMusic2);

        SoundManager.i.PlayMusic(SoundManager.MusicType.MenuMusic);
    }
   

    private void CameraMove_OnCameraWentToDestination(object sender, System.EventArgs e)
    {
        cameraMove.OnCameraWentToDestination -= CameraMove_OnCameraWentToDestination;
        cameraMove.SetIsCameraMovable(false);
    }
}
