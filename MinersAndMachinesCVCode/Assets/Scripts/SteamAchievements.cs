using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class SteamAchievements : MonoBehaviour
{
    public static SteamAchievements i;
    public enum AchievementType
    {
        Tutorial,
        EndGame,
        ForestDone,
        JungleDone,
        DesertDone,
        IceIslandDone,
        BuyAllAchievements,
        MammothHorn,
        Bankrupt,
        Top10Ranking,
        DiamondKing,
        Mummy,
        Bedrock,
        Monke,
        Prosperity,
    }

    public enum StatType
    {
        MoneyStat,
        DozerStat,
        GravelStat,
    }


    private void Awake()
    {
        i = this;
    }

    public void UnlockAchievement(AchievementType achievementType)
    {
        if(SteamManager.Initialized)
        if (!IsAchievementUnlocked(achievementType))
        {
            SteamUserStats.SetAchievement(achievementType.ToString());
            SteamUserStats.StoreStats();
        }
    }

    private bool IsAchievementUnlocked(AchievementType achievementType)
    {
        bool isUnlocked = false;
        if (SteamManager.Initialized)
            SteamUserStats.GetAchievement(achievementType.ToString(), out isUnlocked);
        return isUnlocked;
    }

    public void AddStat(StatType statType, int value)
    {
        if (SteamManager.Initialized) 
        {
            SteamUserStats.SetStat(statType.ToString(), value + GetStat(statType));
            SteamUserStats.StoreStats();         
        }
    }

    private int GetStat(StatType statType)
    {
        int value;
        SteamUserStats.GetStat(statType.ToString(), out value);
        return value;
    }

}
