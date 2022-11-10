using System;
using System.Collections.Generic;
using UnityEngine;

public class Lands : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<LandSO> landSOList;
    
    [field: SerializeField]
    public List<Land> LandList { get; private set; }

    public enum LandType
    {
        Forest,
        Jungle,
        Desert,
        IceIsland
    }

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        // If this is first open game, first land must be unlocked
        if (!IsLandUnlocked(LandType.Forest))
            UnlockLand(LandType.Forest);
    }

    public bool IsLandUnlocked(LandType landType)
    {
        if (SteamCloudDataManager.i.SteamCloudPrefs.LandState.ContainsKey(landType))
        {
            int savedValue = SteamCloudDataManager.i.SteamCloudPrefs.LandState[landType];
            if (savedValue == -1 || savedValue == 0) return false;
        }
        else
        {
            return false;
        }

        return true;
    }

    public bool BuyLand(LandType landType)
    {
        if (CanBuyLand(landType))
        {
            MoneyWallet.i.ActualDolarsInWallet -= GetLandCost(landType);
            UnlockLand(landType);
            return true;
        }
        return false;
    }

    public bool CanBuyLand(LandType landType)
    {
        if (GetLandCost(landType) <= MoneyWallet.i.ActualDolarsInWallet) return true;
        return false;
    }

    public int GetLandCost(LandType landType)
    {
        return landSOList[(int)landType].cost;
    }

    public void ResetLands()
    {
        for(int i=0; i<Enum.GetNames(typeof(LandType)).Length; i++)
        {
            LockLand((LandType)i);
            ResetParcelProfitToLocked((LandType)i);
        }
    }

    private void UnlockLand(LandType landType)
    {
        SetLandStateInCloud(landType, 1);
        ResetParcelProfitToUnlockedNotPlayed(landType);
    }

    private void LockLand(LandType landType)
    {
        SetLandStateInCloud(landType, -1);
    }

    private void SetLandStateInCloud(LandType landType, int value)
    {
        if (SteamCloudDataManager.i.SteamCloudPrefs.LandState.ContainsKey(landType))
        {
            SteamCloudDataManager.i.SteamCloudPrefs.LandState[landType] = value;
        }
        else
        {
            SteamCloudDataManager.i.SteamCloudPrefs.LandState.Add(landType, value);
        }
    }

    private void ResetParcelProfitToUnlockedNotPlayed(LandType landType)
    {
        LandList[(int)landType].LandParcels.ResetParcelsToUnlockedNotPlayed();
    }

    private void ResetParcelProfitToLocked(LandType landType)
    {
        LandList[(int)landType].LandParcels.ResetParcelsToLocked();
    }
}
