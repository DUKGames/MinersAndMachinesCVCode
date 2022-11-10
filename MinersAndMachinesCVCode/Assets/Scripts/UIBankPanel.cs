using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class UIBankPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AnimatorHandler animatorHandler;
    [SerializeField] private TextMeshProUGUI firstDescLine;
    [SerializeField] private TextMeshProUGUI secondDescLine;

    public event EventHandler OnPanelHided;

    private void Start()
    {
        SetTexts();
        LanguangeManager.i.OnLanguageChanged += I_OnLanguageChanged;
    }
    public void ShowPanel()
    {
        gameObject.gameObject.SetActive(true);
        animatorHandler.TriggerAnimator("ShowBankPanel");
        MainMenuManager.i.ActualPanel = MainMenuManager.PanelType.MapMenuBankInfo;
        PlayPanelSound();
    }

    public void HidePanel()
    {
        MainMenuManager.i.ActualPanel = MainMenuManager.PanelType.MapMenu;
        animatorHandler.TriggerAnimator("HideBankPanel");
    }

    public void TriggerOnHidedEvent()
    {
        OnPanelHided?.Invoke(this, EventArgs.Empty);
    }

    private void I_OnLanguageChanged(object sender, EventArgs e)
    {
        SetTexts();
    }

    private void SetTexts()
    {
        firstDescLine.text = LanguangeManager.i.GetWords().bankCreditWordsOne;
        firstDescLine.font = LanguangeManager.i.GetWords().fontsList[0];

        secondDescLine.text = LanguangeManager.i.GetWords().bankCreditWordsTwo;
        secondDescLine.font = LanguangeManager.i.GetWords().fontsList[0];
    }

    private void PlayPanelSound()
    {
        SoundManager.i.PlaySound(SoundManager.SoundType.PanelSound);
    }
}
