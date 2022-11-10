using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Machines : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<MachineSO> machinesSO;
    [SerializeField] private Transform container;

    private List<Machine> spawnedMachines;

    public event EventHandler<WashplantEventArgs> OnWashplantDestroyed;

    public enum MachineType
    {
        Washplant,
        Driller,
        Dozer,
        Excavator,
        FuelTank,
        GoldPan,
    }

    private void Awake()
    {
        spawnedMachines = new List<Machine>();
    }

    public class WashplantEventArgs : EventArgs
    {
        public int otherSpawnedCount;
    }

    public Machine SpawnMachine(MachineType machineType, Vector3 position)
    {
        Machine machine = Instantiate(GetMachinePrefab(machineType), position, Quaternion.identity, container);
        spawnedMachines.Add(machine);
        return machine;
    }

    public GhostBuilding GetGhostBuildingPrefab(MachineType machineType)
    {
        return machinesSO[(int)machineType].ghostBuilding;
    }

    public Vector3Int GetMachineOffset(MachineType machineType)
    {
        return machinesSO[(int)machineType].machineOffset;
    }

    public int GetBuildCost(MachineType machineType)
    {
        return machinesSO[(int)machineType].buildCost;
    }

    public int GetRefundMoneyValue(MachineType machineType)
    {
        return machinesSO[(int)machineType].refundMoney;
    }

    public int GetFuelConsumptionPerBlock(MachineType machineType)
    {
        return machinesSO[(int)machineType].fuelConsumptionPerBlock;
    }

    public TileBase GetTileBase(MachineType machineType)
    {
        return machinesSO[(int)machineType].tileBase;
    }

    public bool CanBeDeleted(MachineType machineType)
    {
        return machinesSO[(int)machineType].canBeDeleted;
    }

    public Machine GetMachinePrefab(MachineType machineType)
    {
        return machinesSO[(int)machineType].machinePrefab;
    }

    public Vector3 GetDeleteButtonOffset(MachineType machineType)
    {
        return machinesSO[(int)machineType].deleteButtonOffset;
    }

    public Vector3 GetFuelWarningOffset(MachineType machineType)
    {
        return machinesSO[(int)machineType].fuelWarningOffset;
    }

    public Vector3 GetDeleteButonScale(MachineType machineType)
    {
        return machinesSO[(int)machineType].deleteButtonScale;
    }

    public void DestroyMachine(Machine machine)
    {
        spawnedMachines.Remove(machine);
        Destroy(machine.gameObject);
    }

    public List<Sprite> GetWorkSpriteList(MachineType machineType) 
    {
        return machinesSO[(int)machineType].workSpriteList;
    }

    public float GetWorkFrameTime(MachineType machineType)
    {
        return machinesSO[(int)machineType].workFrameTime;
    }

    public List<Sprite> GetDriveSpriteList(MachineType machineType)
    {
        return machinesSO[(int)machineType].driveSpriteList;
    }

    public float GetDriveFrameTime(MachineType machineType)
    {
        return machinesSO[(int)machineType].driveFrameTime;
    }

    // GET ALL SPAWNED MACHINES OF TYPE
    public List<Machine> GetSpawnedMachines(MachineType machineType)
    {
        return spawnedMachines.FindAll(x => x.MachineType.Equals(machineType));
    }

    public void DestroyAllMachines()
    {
        for(int i=0; i<spawnedMachines.Count; i++)
        {
            DestroyMachine(spawnedMachines[i]);
            i--;
        }
        spawnedMachines.Clear();
    }
}
