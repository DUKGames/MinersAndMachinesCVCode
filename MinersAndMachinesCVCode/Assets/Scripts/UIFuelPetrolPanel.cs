using UnityEngine;
using TMPro;

public class UIFuelPetrolPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI buyText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private FuelPetrol fuelPetrol;
    [SerializeField] private Animator animator;

    private void Start()
    {
        SetupWords();
        fuelPetrol.OnFuelPriceChanged += FuelPetrol_OnFuelPriceChanged;
    }

    public void ShowPanel()
    {
        TriggerAnimator(true);
    }

    public void HidePanel()
    {
        TriggerAnimator(false);
    }

    public void PlayRefuelSound()
    {
        SoundManager.i.PlaySound(SoundManager.SoundType.RefuelSound);
    }

    private void SetupWords()
    {
        buyText.text = LanguangeManager.i.GetWords().buyWord;
        buyText.font = LanguangeManager.i.GetWords().fontsList[0];
    }

    private void FuelPetrol_OnFuelPriceChanged(object sender, FuelPetrol.FuelPriceArgs e)
    {
        SetPriceText(e.newFuelPrice.ToString());
    }

    private void SetPriceText(string price)
    {
        priceText.text = price;
    }

    private void TriggerAnimator(bool newState)
    {
        animator.SetBool("Show", newState);
    }

}
