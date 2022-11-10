using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Machine", menuName = "ScriptableObjects/Machine")]
public class MachineSO : ScriptableObject
{
    public TileBase tileBase;
    public Machine machinePrefab;
    public GhostBuilding ghostBuilding;
    public Vector3Int machineOffset;

    public float machineSpeed;
    public int buildCost;
    public int refundMoney;
    public int fuelConsumptionPerBlock;

    public Vector3 deleteButtonOffset;
    public Vector3 deleteButtonScale;
    public bool canBeDeleted;

    public Vector3 fuelWarningOffset;

    public List<Sprite> workSpriteList;
    public float workFrameTime;
    public List<Sprite> driveSpriteList;
    public float driveFrameTime;
}
