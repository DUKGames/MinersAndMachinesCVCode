using UnityEngine;

public class CameraScreenResolution : MonoBehaviour
{
    [SerializeField] private CameraMove cameraMove;
    [SerializeField] private ResolutionManager resolutionManager;

    private void Awake()
    {
        cameraMove.CalculateMoveRange(Screen.width, Screen.height);

        SetupCameraSize(Screen.width, Screen.height);

        if(resolutionManager != null) resolutionManager.OnResolutionChanged += ResolutionManager_OnResolutionChanged;
    }

    private void ResolutionManager_OnResolutionChanged(object sender, ResolutionManager.OnResolutionChangedArgs e)
    {
        SetupCameraSize(e.resolution.width, e.resolution.height);
        cameraMove.GetCameraHandler().SetPosition(new Vector3(0f, 0f, -100f));
        cameraMove.CalculateMoveRange(Screen.width, Screen.height);
        cameraMove.CalculateMaxDownPositionResized();
    }

    private void SetupCameraSize(float screenWidth, float screenHeight)
    {
        float ratio = ReturnScreenRatio(screenWidth, screenHeight);
        switch (ratio)
        {
            default:
            case 2f:
            case 1.78f:
            case 1.8f:
                cameraMove.GetCameraHandler().SetOrthographicSize(GetOrthographicSize(screenWidth, screenHeight));
                break;
            case 1.6f:
            case 1.33f:
            case 1.25f:
            case 1.5f:
                cameraMove.GetCameraHandler().SetOrthographicSize(5f);
                break;
        }
    }

    private float GetOrthographicSize(float screenWidth, float screenHeight)
    {
        float calculated = 1920f / screenWidth * screenHeight / 2f;
        calculated = calculated / 100.1f;
        calculated = calculated * 0.94f;
        calculated -= 0.1f;
        return calculated;
    }

    private float ReturnScreenRatio(float wi, float hei)
    {
        float rat = wi / hei;
        return (Mathf.Round(rat * 100f) / 100f);
    }
}
