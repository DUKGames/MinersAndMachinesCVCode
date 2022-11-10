using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    public static Levels i;

    [Header("References")]
    [SerializeField] private List<LevelSO> levelList;

    private void Awake()
    {
        if (Levels.i == null)
            i = this;
    }

    public int GetLevelStartMoney(GameSceneManager.Scene level)
    {
        return levelList[(int)level - 1].LevelStartMoney;
    }

    public int GetLevelDayTime(GameSceneManager.Scene level)
    {
        return levelList[(int)level - 1].DayTime;
    }

    public int GetLevelMaxGameplayDayTime(GameSceneManager.Scene level)
    {
        return levelList[(int)level - 1].MaxGameplayDayTime;
    }

    public int GetLevelFuelMinCost(GameSceneManager.Scene level)
    {
        return levelList[(int)level - 1].FuelMinCost;
    }

    public int GetLevelFuelMaxCost(GameSceneManager.Scene level)
    {
        return levelList[(int)level - 1].FuelMaxCost;
    }
}
