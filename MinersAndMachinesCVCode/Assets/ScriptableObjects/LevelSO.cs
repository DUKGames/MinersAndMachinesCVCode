using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level")]
public class LevelSO : ScriptableObject
{
    public int LevelStartMoney;
    public int DayTime;
    public int MaxGameplayDayTime;
    public int FuelMinCost;
    public int FuelMaxCost;
}
