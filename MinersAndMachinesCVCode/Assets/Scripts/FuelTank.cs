using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTank : MonoBehaviour
{
    [field: SerializeField]
    public int ActualCapacity { get; set; }

    [field: SerializeField]
    public int MaxCapacity { get; set; }

    [Header("References")]
    [SerializeField] private FuelTankIndicator fuelTankIndicator;

    public void ShowFuelFilling()
    {
        float newValue = (1 - (MaxCapacity - ActualCapacity) / (float)MaxCapacity);

        fuelTankIndicator.SetFuelState(newValue);
    }
}
