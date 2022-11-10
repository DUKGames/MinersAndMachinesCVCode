using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UIUpgrades : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI upgradeDescription;
    [SerializeField] private Button buyButton;

    [SerializeField] private List<UIUpgrade> uiUpgrades;

    [SerializeField] private TextMeshProUGUI backButtonText;
    [SerializeField] private TextMeshProUGUI buyButtonText;

    private void Start()
    {
        SetTexts();
        LanguangeManager.i.OnLanguageChanged += I_OnLanguageChanged;
    }

    private void OnEnable()
    {
        SetUpgradeDescription(LanguangeManager.i.GetWords().selectUpgradeWords);
        SetUpgradeFont(LanguangeManager.i.GetWords().fontsList[0]);
        SetBuyUpgradeButtonState(false);
        SetupUpgradesUI();
    }

    private void SetTexts()
    {
        backButtonText.text = LanguangeManager.i.GetWords().backWord;
        backButtonText.font = LanguangeManager.i.GetWords().fontsList[0];

        buyButtonText.text = LanguangeManager.i.GetWords().buyWord;
        buyButtonText.font = LanguangeManager.i.GetWords().fontsList[0];
    }

    private void SetupUpgradesUI()
    {
        int notBoughtUpgradesCount = 0;
        for(int i=0; i<uiUpgrades.Count; i++)
        {
            bool isUpgradeBought = Upgrades.i.GetUpgradeState((Upgrades.UpgradeType)i) == -1 ? false : true;

            if (isUpgradeBought)
            {
                // IS BOUGHT
                ShowUpgradePrice(i, "");
                SetTickState(i, true);
                //SetUpgradeButtonAction(i, null);
                SetUpgradeButtonAction(i, GetSetDestriptionAction(i, false));
            }
            else
            {
                // IS NOT BOUGHT SET BUY BUTTON IF CAN BUY
                notBoughtUpgradesCount++;
                ShowUpgradePrice(i, "$" + Upgrades.i.GetUpgradePrice((Upgrades.UpgradeType)i).ToString());
                SetTickState(i, false);
                SetUpgradeButtonAction(i, GetSetDestriptionAction(i, true));
            }
        }

        CheckBuyAllUpgradeAchievement(notBoughtUpgradesCount);
    }

    private void CheckBuyAllUpgradeAchievement(int notBoughtUpgradesCount)
    {
        if (notBoughtUpgradesCount == 0) SteamAchievements.i.UnlockAchievement(SteamAchievements.AchievementType.BuyAllAchievements);
    }

    private void SetBuyUpgradeButtonState(bool state)
    {
        buyButton.interactable = state;
    }

    private void ShowUpgradePrice(int index, string price)
    {
        uiUpgrades[index].priceText.text = price;
    }

    private void SetTickState(int index, bool state)
    {
        uiUpgrades[index].tick.gameObject.SetActive(state);
    }

    private void SetUpgradeButtonAction(int index, Action action)
    {
        RemoveUpgradeButtonListeners(index);
        uiUpgrades[index].upgradeButton.onClick.AddListener(() => action());
    }

    private void RemoveUpgradeButtonListeners(int index)
    {
        uiUpgrades[index].upgradeButton.onClick.RemoveAllListeners();
    }

    private void SetBuyButtonAction(int index)
    {
        if (MoneyWallet.i.ActualDolarsInWallet >= Upgrades.i.GetUpgradePrice((Upgrades.UpgradeType)index))
        {
            // Can buy this upgrade
            buyButton.onClick.RemoveAllListeners();
            Action action = GetBuyUpgradeButtonAction(index);
            buyButton.onClick.AddListener(() => action()); 
            SetBuyUpgradeButtonState(true);
        }
        else
        {
            // Can't buy this upgrade
            SetBuyUpgradeButtonState(false);
        }
    }

    private void SetUpgradeDescription(string text)
    {
        upgradeDescription.text = text;
    }

    private void SetUpgradeFont(TMP_FontAsset font)
    {
        upgradeDescription.font = font;
    }

    private Action GetSetDestriptionAction(int index, bool isLocked)
    {
        if(isLocked) return () => { 
            SetUpgradeDescription(LanguangeManager.i.GetWords().upgradeDestriptionsWords[index]);
            SetBuyButtonAction(index);
        };

        return () => { 
            SetUpgradeDescription(LanguangeManager.i.GetWords().upgradeDestriptionsWords[index]); 
            SetBuyUpgradeButtonState(false); 
        };
    }

    private Action GetBuyUpgradeButtonAction(int index)
    {
        return () => { 
            PlayUpgradeSound(); 
            SetBuyUpgradeButtonState(false); 
            Upgrades.i.UnlockUpgrade((Upgrades.UpgradeType)index); 
            InstantianeUpgradeEffect(index); 
            SetupUpgradesUI(); 
            SetUpgradeDescription("Select upgrade"); 
        };
    }

    private void InstantianeUpgradeEffect(int upgradeIndex)
    {
        Instantiate(GameAssets.i.upgradeEffect, uiUpgrades[upgradeIndex].upgradeButton.transform.position, Quaternion.identity);
    }

    private void I_OnLanguageChanged(object sender, EventArgs e)
    {
        SetTexts();
    }

    private void PlayUpgradeSound()
    {
        SoundManager.i.PlaySound(SoundManager.SoundType.UpgradeSound);
    }
}

[System.Serializable]
public class UIUpgrade
{
    public TextMeshProUGUI priceText;
    public Button upgradeButton;
    public Image tick;
}
