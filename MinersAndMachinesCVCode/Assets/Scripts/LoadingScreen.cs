using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen i;

    [Header("References")]
    [SerializeField] private AnimatorHandler animatorHandler;

    public event EventHandler OnLoadingScreenShowed;

    private void Awake()
    {
        if (LoadingScreen.i == null)
            i = this;

        SetGameobjectState(false);
    }

    public void ShowLoadingScreen()
    {
        animatorHandler.TriggerAnimator("Show");
        PlayLoadingScreenSound();
    }

    public void HideLoadingScreen()
    {
        animatorHandler.TriggerAnimator("Hide");
    }

    public void TriggerEventOnLoadingScreenShowed()
    {
        OnLoadingScreenShowed?.Invoke(this, EventArgs.Empty);
    }

    public void SetGameobjectState(bool newState)
    {
        gameObject.SetActive(newState);
    }

    private void PlayLoadingScreenSound()
    {
        SoundManager.i.PlaySound(SoundManager.SoundType.LoadingScreen);
    }
}
