using System.Collections;
using UnityEngine;
using System;
public class FuelPetrol : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private int timeToChangePrice = 10;
    [SerializeField] private int priceChangeValue = 5;

    public event EventHandler<FuelPriceArgs> OnFuelPriceChanged;
  
    private int actualFuelPrice = 250; 
    private bool changePrice = true;
    private bool priceGoUp;
    private Coroutine coroutine;

    public class FuelPriceArgs : EventArgs
    {
        public int newFuelPrice;
    }

    public void StartChangePrice()
    {
        changePrice = true;
        coroutine = StartCoroutine(FuelPriceChange());
    }

    public void StopChangePrice()
    {
        if (coroutine != null) StopCoroutine(coroutine);
        changePrice = false;
    }

    public void BuyFuel()
    {
        if(MoneySystem.i.Money - actualFuelPrice >= 0)
        {
            // Can refuel
            if (FuelSystem.instance.Refuel(1000))
            {
                MoneySystem.i.Money -= actualFuelPrice;
            }
            else
            {
                int refilledAmount = FuelSystem.instance.Refuel();
                int cost = (int)(((float)actualFuelPrice / 1000) * refilledAmount);
                MoneySystem.i.Money -= cost;
            }
        }
        // Not enought money
    }

    private IEnumerator FuelPriceChange()
    {
        while (changePrice)
        {
            yield return new WaitForSeconds(timeToChangePrice);

            // Change price
            SetPrice(GetNewPrice());
        }
    }

    private int GetNewPrice()
    {
        int newPrice = actualFuelPrice;
        if (priceGoUp)
        {
            if(actualFuelPrice < Levels.i.GetLevelFuelMaxCost(LevelSetup.i.ActualLevel))
            {
                // Can increase price
                newPrice += priceChangeValue;
            }
            else
            {
                // Actual price is max price
                priceGoUp = false;
            }
        }
        else
        {
            if (actualFuelPrice > Levels.i.GetLevelFuelMinCost(LevelSetup.i.ActualLevel))
            {
                // Can decrease price
                newPrice -= priceChangeValue;
            }
            else
            {
                // Actual price is min price
                priceGoUp = true;
            }
        }

        return newPrice;
    }

    private void SetPrice(int newPrice)
    {
        actualFuelPrice = newPrice;
        OnFuelPriceChanged?.Invoke(this, new FuelPriceArgs { newFuelPrice = newPrice });
    }


}
