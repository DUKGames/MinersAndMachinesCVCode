using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LandSO", menuName = "ScriptableObjects/LandSO")]
public class LandSO : ScriptableObject
{
    public Lands.LandType LandType;
    public int cost;
}
