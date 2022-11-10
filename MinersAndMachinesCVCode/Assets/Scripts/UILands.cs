using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILands : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Lands lands;
    [SerializeField] private List<Transform> padlockList;
    [SerializeField] private List<UIText> costList;
    [SerializeField] private List<Button> buttonList;
    [SerializeField] private UIParcels uIParcels;

    [SerializeField] private AnimatorHandler buttonRankingsAnimator;
    [SerializeField] private ImageMouse buttonRankingsImageMouse;

    private void OnEnable()
    {
        ShowStatesOnMap();
        buttonRankingsImageMouse.OnMouseEnter += ButtonRankingsImageMouse_OnMouseEnter;
    }

    private void OnDisable()
    {
        buttonRankingsImageMouse.OnMouseEnter -= ButtonRankingsImageMouse_OnMouseEnter;
    }

    // Show locked/unlocked land state on map
    public void ShowStatesOnMap()
    {
        for(int i=0; i < Enum.GetNames(typeof(Lands.LandType)).Length; i++)
        {
            if (lands.IsLandUnlocked((Lands.LandType)i))
            {
                // Land is unlocked
                SetPadlockState(i, false);
                SetCostTextState(i, false);
                SetButtonState(i, false);
            }
            else
            {
                // Land is locked
                SetPadlockState(i, true);
                SetCostTextState(i, true);
                SetLandCost(i, "$" + lands.GetLandCost((Lands.LandType)i));
                SetButtonState(i, true);

                Action action = GetButtonActionBuyLandType(i);
                SetButtonAction(i, action); 
                
            }
        }
    }

    private void ButtonRankingsImageMouse_OnMouseEnter(object sender, ImageMouse.OnMouseEnterArgs e)
    {
        if (e.isMouseOnImage)
        {
            TriggerButtonRankingsAnimator(true);
        }
        else
        {
            TriggerButtonRankingsAnimator(false);
        }
    }

    private void TriggerButtonRankingsAnimator(bool newValue)
    {
        buttonRankingsAnimator.TriggerAnimator("PanelShowed", newValue);
    }

    private void SetPadlockState(int index, bool newState)
    {
        padlockList[index].gameObject.SetActive(newState);
    }

    private void SetCostTextState(int index, bool newState)
    {
        costList[index].gameObject.SetActive(newState);
    }

    private void SetButtonAction(int index, Action newAction)
    {
        buttonList[index].onClick.RemoveAllListeners();
        buttonList[index].onClick.AddListener(() => { newAction(); });
    }

    private void SetButtonState(int index, bool newState)
    {
        buttonList[index].gameObject.SetActive(newState);
    }

    private void SetLandCost(int index, string value)
    {
        costList[index].SetText(value);
    }

    private Action GetButtonActionBuyLandType(int index)
    {
       return () => { 
           if (lands.BuyLand((Lands.LandType)index)) 
           { 
               InstantiateUnlockPadlockEffect(padlockList[index].transform.position); 
               ShowStatesOnMap(); 
               uIParcels.SetupParcelsUI(); 
           } 
           else 
           { 
               SoundManager.i.PlaySound(SoundManager.SoundType.ClickSound); 
           }};
    }


    private void InstantiateUnlockPadlockEffect(Vector3 position)
    {
        Instantiate(GameAssets.i.padlockEffect, position, Quaternion.identity);
    }
}

