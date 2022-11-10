using UnityEngine;
using TMPro;

public class UIMoneySystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI moneyValueText;

    private void Start()
    {
        ShowMoneyValue(MoneySystem.i.Money);
        MoneySystem.i.OnMoneyAmountChanged += I_OnMoneyAmountChanged;
    }

    private void I_OnMoneyAmountChanged(object sender, MoneySystem.MoneyArgs e)
    {
        ShowMoneyValue(e.moneyAmount);
    }

    private void ShowMoneyValue(int value)
    {
        moneyValueText.text = value.ToString();
    }
}
