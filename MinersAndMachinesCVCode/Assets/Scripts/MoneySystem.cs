using System;
using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    private int money;

    public int Money { 
        get 
        { 
            return money; 
        } 
        set 
        {
            CheckSpentIncom(value); 
            money = value; 
            OnMoneyAmountChanged?.Invoke(this, new MoneyArgs { moneyAmount = value }); 
        }}

    public int SpentMoney { get; private set; }
    public int IncomMoney { get; private set; }

    public event EventHandler<MoneyArgs> OnMoneyAmountChanged;
    public static MoneySystem i { get; set; }

    private void Awake()
    {
        i = this;
    }

    public class MoneyArgs : EventArgs
    {
        public int moneyAmount;
    }

    public void Setup(int startMoneyValue)
    {
        Money = startMoneyValue;
        SpentMoney = 0;
        IncomMoney = 0;
    }

    private void CheckSpentIncom(int newValue)
    {
        if (newValue > money)
        {
            IncomMoney += newValue - money;
        }
        else
        {
            SpentMoney += money - newValue;
        }
    }
}
