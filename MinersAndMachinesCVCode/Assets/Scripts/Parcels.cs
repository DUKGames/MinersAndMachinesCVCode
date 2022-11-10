using System.Collections.Generic;
using UnityEngine;

public class Parcels : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<Parcel> parcelList;

    [field: SerializeField]
    public Lands.LandType LandType { get; set; }

    public int GetParcelListCount()
    {
        return parcelList.Count;
    }

    public void SetParcelProfit(int index, int profitValue)
    {
        string key = "Parcel/" + LandType + "/" + GetParcelSceneLevel(index);
        if (SteamCloudDataManager.i.SteamCloudPrefs.ParcelProfit.ContainsKey(key))
        {
            SteamCloudDataManager.i.SteamCloudPrefs.ParcelProfit[key] = profitValue;
        }
        else
        {
            SteamCloudDataManager.i.SteamCloudPrefs.ParcelProfit.Add(key, profitValue);
        }
    }

    public int GetParcelsProfit()
    {
        int profit = 0;

        for(int i=0; i<parcelList.Count; i++)
        {
            string key = "Parcel/" + LandType + "/" + GetParcelSceneLevel(i);
            if (SteamCloudDataManager.i.SteamCloudPrefs.ParcelProfit.ContainsKey(key))
            {
                if(SteamCloudDataManager.i.SteamCloudPrefs.ParcelProfit[key] != -2)
                profit += SteamCloudDataManager.i.SteamCloudPrefs.ParcelProfit[key];
            }
        }

        return profit;
    }

    public int GetParcelProfit(int index)
    {
        string key = "Parcel/" + LandType + "/" + GetParcelSceneLevel(index);
        if (SteamCloudDataManager.i.SteamCloudPrefs.ParcelProfit.ContainsKey(key))
        {
            return SteamCloudDataManager.i.SteamCloudPrefs.ParcelProfit[key];
        }
        else
        {
            return -1;
        }
    }

    public void ResetParcelsToUnlockedNotPlayed()
    {
        SetParcelsProfit(-2);
    }

    public void ResetParcelsToLocked()
    {
        SetParcelsProfit(-1);
    }

    public void SetParcelsProfit(int profitValue)
    {
        for (int parcelIndex = 0; parcelIndex<GetParcelListCount(); parcelIndex++)
        {
            SetParcelProfit(parcelIndex, profitValue);
        }
    }

    public GameSceneManager.Scene GetParcelSceneLevel(int index)
    {
        return parcelList[index].LevelScene;
    }
}
