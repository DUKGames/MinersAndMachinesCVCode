using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public static Upgrades i;

    [Header("References")]
    [SerializeField] private List<UpgradeSO> upgradeSOList;
    
    private void Awake()
    {
        if (Upgrades.i == null)
        {
            i = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public enum UpgradeType
    {
        IceBreaker,
        StoneBreaker,
        WashDiax,
        WashRubin,
        WashEmerald,
        FuelBonus,
        DozerSpeed,
        DrillerSpeed,
        ExcavatorSpeed,
        WashEffectivy,
    }

    public int GetUpgradeState(UpgradeType upgradeType)
    {
        if (SteamCloudDataManager.i.SteamCloudPrefs.UpgradeState.ContainsKey(upgradeType))
        {
            return SteamCloudDataManager.i.SteamCloudPrefs.UpgradeState[upgradeType];
        }
        else
        {
            return -1;
        }
    }
    public void UnlockUpgrade(UpgradeType upgradeType)
    {
        MoneyWallet.i.ActualDolarsInWallet -= GetUpgradePrice(upgradeType);
        SetUpgradeState(upgradeType, 1);
    }

    public void LockUpgrade(UpgradeType upgradeType)
    {
        SetUpgradeState(upgradeType, -1);
    }

    public int GetUpgradePrice(UpgradeType upgradeType)
    {
        return upgradeSOList[(int)upgradeType].cost;
    }

    public int GetUpgradesCount()
    {
        return Enum.GetNames(typeof(UpgradeType)).Length;
    }

    public void ResetUpgrades()
    {
        for(int i=0; i<Enum.GetNames(typeof(UpgradeType)).Length; i++)
        {
            LockUpgrade((UpgradeType)i);
        }
    }

    private void SetUpgradeState(UpgradeType upgradeType, int value)
    {
        if (SteamCloudDataManager.i.SteamCloudPrefs.UpgradeState.ContainsKey(upgradeType))
        {
            SteamCloudDataManager.i.SteamCloudPrefs.UpgradeState[upgradeType] = value;
        }
        else
        {
            SteamCloudDataManager.i.SteamCloudPrefs.UpgradeState.Add(upgradeType, value);
        }
    }

}
