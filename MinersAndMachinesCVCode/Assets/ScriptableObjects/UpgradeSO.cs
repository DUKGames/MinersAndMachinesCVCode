using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/Upgrade")]
public class UpgradeSO : ScriptableObject
{
    public Upgrades.UpgradeType upgradeType;
    public int cost;
}
