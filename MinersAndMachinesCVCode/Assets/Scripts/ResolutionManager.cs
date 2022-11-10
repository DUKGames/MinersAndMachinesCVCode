using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class ResolutionManager : MonoBehaviour
{
    public float ScreenRatio { get; private set; }
    public Resolution[] AvailableResolutionsList { get; private set; }

    public event EventHandler<OnResolutionChangedArgs> OnResolutionChanged;

    public class OnResolutionChangedArgs : EventArgs
    {
        public Resolution resolution;
    }

    private void Awake()
    {
        ScreenRatio = GetScreenRatio(Screen.width, Screen.height);
        AvailableResolutionsList = GetResolutions().Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
    }

    public float GetScreenRatio(float width, float height)
    {
        float ratio = width / height;
        return (Mathf.Round(ratio * 100f) / 100f);
    }

    public void SetResolution(Resolution resolution, bool fullscreen)
    {
        Screen.SetResolution(resolution.width, resolution.height, fullscreen);
        OnResolutionChanged?.Invoke(this, new OnResolutionChangedArgs { resolution = resolution });
    }

    public void SetFullscreenState(bool newState)
    {
        Screen.fullScreen = newState;
    }

    public bool GetFullscreenState()
    {
        return Screen.fullScreen;
    }

    private Resolution[] GetResolutions()
    {
        return Screen.resolutions;
    }
}
