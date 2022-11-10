using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamCloudDataManager : MonoBehaviour
{
    public static SteamCloudDataManager i;
    public SteamCloudPrefs SteamCloudPrefs { get; set; }

    private void Awake()
    {
        if (SteamCloudDataManager.i == null)
        {
            i = this;
            LoadDataFromCloud();
        }
    }

    private void OnDestroy()
    {
        if(SteamCloudDataManager.i == this)
        {
            SaveData();
        }
    }

    private void LoadDataFromCloud()
    {
        SteamCloudPrefs loadedPrefs = SteamCloudFile.LoadData();
        if(loadedPrefs != null)
        {
            SteamCloudPrefs = loadedPrefs;
        }
        else
        {
            SteamCloudPrefs = new SteamCloudPrefs();
        }
    }

    private void SaveData()
    {
        SteamCloudFile.SaveData(SteamCloudPrefs);
    }
}
