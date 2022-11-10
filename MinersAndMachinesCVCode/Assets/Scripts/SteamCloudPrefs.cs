using System.Collections.Generic;

[System.Serializable]
public class SteamCloudPrefs
{
    public SteamCloudPrefs()
    {
        ParcelProfit = new Dictionary<string, int>();
        LandState = new Dictionary<Lands.LandType, int>();
        UpgradeState = new Dictionary<Upgrades.UpgradeType, int>();
        LandParcelsDoneAchievement = new Dictionary<Lands.LandType, int>();
    }

    public int PlayOnSaveNumber { get; set; }
    public bool NeedRefreshScoreInDatabase { get; set; }
    public int MoneyInWallet { get; set; }
    public int TutorialScroll { get; set; }
    public int Credit { get; set; }
    public float MusicVolumeSlider { get; set; }
    public float SFXVolumeSlider { get; set; }
    public string Nickname { get; set; }
    public int DiamondAchievementStatus { get; set; }
    public int BedrockAchievementStatus { get; set; }
    public int ProsperityAchievementStatus { get; set; }
    public int AddedMailsCount { get; set; }
    public Language.LanguageType ActualLanguage { get; set; }
    public Dictionary<Lands.LandType, int> LandParcelsDoneAchievement { get; set; }
    public Dictionary<string, int> ParcelProfit { get; set; }
    public Dictionary<Lands.LandType, int> LandState { get; set; }
    public Dictionary<Upgrades.UpgradeType, int> UpgradeState { get; set; }
}
