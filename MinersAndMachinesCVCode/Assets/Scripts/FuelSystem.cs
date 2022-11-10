using System;
using System.Collections.Generic;
using UnityEngine;

public class FuelSystem : MonoBehaviour
{
    public static FuelSystem instance;

    [Header("References")]
    [SerializeField] private List<FuelTank> fuelTanks;

    private int actualWholeFuelCapacity;
    private int actualWholeTanksCapacity;

    public event EventHandler<FuelCapacityArgs> OnFuelTanksCapacityChanged;
    public event EventHandler OnRefuel;

    private void Awake()
    {
        if (FuelSystem.instance == null)
            instance = this;
    }

    private void OnEnable()
    {
        SetNewTanksCapacity(0);
        SetNewFuelCapacity(0);
    }

    public class FuelCapacityArgs : EventArgs
    {
        public int actualCapacity;
    }

    public int GetActualWholeFuelCapacity()
    {
        return actualWholeFuelCapacity;
    }

    public void AddFuelTank(FuelTank fuelTank)
    {
        fuelTanks.Add(fuelTank);
        fuelTank.ShowFuelFilling();
        RefreshFuelCapacityStatus();
        OnRefuel?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveFuelTank(FuelTank fuelTank)
    {
        FuelTank findedFuelTank = fuelTanks.Find(x => x == fuelTank);
        fuelTanks.Remove(findedFuelTank);
        int tempCapacity = actualWholeFuelCapacity - findedFuelTank.ActualCapacity;
        int tempTanksCapacity = actualWholeTanksCapacity - findedFuelTank.MaxCapacity;

        SetNewFuelCapacity(tempCapacity);
        SetNewTanksCapacity(tempTanksCapacity);
    }

    public bool TakeFuel(int amount)
    {
        int newFuelAmount = actualWholeFuelCapacity - amount;
        if(newFuelAmount >= 0)
        {
            SetNewFuelCapacity(newFuelAmount);
            TakeFuelFromTanks(amount);
            return true;
        }
        return false;
    }

    public int TakeFuel()
    {
        int actualFuelAmount = actualWholeFuelCapacity;
        SetNewFuelCapacity(0);
        TakeFuelFromTanks(actualFuelAmount);
        return actualFuelAmount;
    }

    public bool Refuel(int amount)
    {
        int newFuelAmount = actualWholeFuelCapacity + amount;
        if (newFuelAmount <= actualWholeTanksCapacity)
        {
            SetNewFuelCapacity(newFuelAmount);
            GiveFuelToTanks(amount);
            return true;
        }
        return false;
    }

    public int Refuel()
    {
        int actualFuelAmount = actualWholeFuelCapacity;
        int fillValue = actualWholeTanksCapacity - actualFuelAmount;
        SetNewFuelCapacity(actualWholeTanksCapacity);
        GiveFuelToTanks(fillValue);
        return fillValue;
    }

    public int GetFuelTanksCount()
    {
        return fuelTanks.Count;
    }

    private void RefreshFuelCapacityStatus()
    {
        int tempCapacity = 0;
        int tempTanksCapacity = 0;

        foreach(FuelTank fuelTank in fuelTanks)
        {
            tempCapacity += fuelTank.ActualCapacity;
            tempTanksCapacity += fuelTank.MaxCapacity;
        }

        SetNewFuelCapacity(tempCapacity);
        SetNewTanksCapacity(tempTanksCapacity);
    }

    private void SetNewFuelCapacity(int newValue)
    {
        actualWholeFuelCapacity = newValue;
        OnFuelTanksCapacityChanged?.Invoke(this, new FuelCapacityArgs { actualCapacity = actualWholeFuelCapacity });
    }

    private void SetNewTanksCapacity(int newValue)
    {
        actualWholeTanksCapacity = newValue;
    }

    private void TakeFuelFromTanks(int amount)
    {
        int lessFuelToTake = amount;
        foreach(var tank in fuelTanks)
        {
            int newTankCapacity = tank.ActualCapacity - lessFuelToTake;
            if(newTankCapacity >= 0)
            {
                // TAKE ALL NEEDEED FUEL FROM TANK
                tank.ActualCapacity = newTankCapacity;
                tank.ShowFuelFilling();
                break;
            }
            else
            {
                // TANK DON'T HAVE ENOUGH FUEL
                lessFuelToTake -= tank.ActualCapacity;
                tank.ActualCapacity = 0; 
                tank.ShowFuelFilling();
            }
        }
    }

    private void GiveFuelToTanks(int amount)
    {
        int lessFuelToGive = amount;
        foreach (var tank in fuelTanks)
        {
            if(tank.ActualCapacity != tank.MaxCapacity)
            {
                int newTankCapacity = tank.ActualCapacity + lessFuelToGive;
                if (newTankCapacity <= tank.MaxCapacity)
                {
                    // THIS TANK CAN TAKE ALL FUEL WHICH I WANT REFUEL
                    tank.ActualCapacity = newTankCapacity;
                    tank.ShowFuelFilling();
                    break;
                }
                else
                {
                    // TANK CAN TAKE SOMETHING, BUT NOT ALL FUEL
                    lessFuelToGive -= tank.MaxCapacity - tank.ActualCapacity;
                    tank.ActualCapacity = tank.MaxCapacity; 
                    tank.ShowFuelFilling();
                }
            }
        }

        OnRefuel?.Invoke(this, EventArgs.Empty);
    }
}
