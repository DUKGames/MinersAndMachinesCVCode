using UnityEngine;
using System;

public class OnSelectSprite : MonoBehaviour
{
    public Action OnMouseEnterAction { private get; set; }
    public Action OnMouseExitAction { private get; set; }

    private void OnMouseEnter()
    {
        if (OnMouseEnterAction != null) 
            OnMouseEnterAction();
    }

    private void OnMouseExit()
    {
        if (OnMouseExitAction != null) 
            OnMouseExitAction();
    }
}
