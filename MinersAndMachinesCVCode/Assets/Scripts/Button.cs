using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IDeselectHandler
{
    [SerializeField] private Button button;
    [SerializeField] private Image image;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color specialColor;

    private bool disableButtonWhenClickOutside = false;
    private bool canBeEnabled;

    private Action actionToDo;

    public void DisableButtonWhenClickOutside(float delay)
    {
        if(delay > 0)
        {
            Invoke("DisableButonWithDelay", delay);
        }
        else
        {
            DisableButonWithDelay();
        }
    }

    private void DisableButonWithDelay()
    {
        disableButtonWhenClickOutside = true;
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if(disableButtonWhenClickOutside)
        if(EventSystem.current.currentSelectedGameObject == gameObject)
            {
                SetButtonGameobjectState(false, 0f);
                disableButtonWhenClickOutside = false;
            }
    }

    public void SetButtonState(bool newState)
    {
        button.interactable = newState;
    }

    private void EnableButtonGameobject()
    {
        if (canBeEnabled)
        gameObject.SetActive(true);
    }

    private void DisableButtonGameobject()
    {
        gameObject.SetActive(false);
    }

    private void SetCanBeEnabled(bool newState)
    {
        canBeEnabled = newState;
    }

    public void SetButtonGameobjectState(bool newState, float delay)
    {
        if (delay > 0)
        {
            if (newState)
            {
                SetCanBeEnabled(true);
                Invoke("EnableButtonGameobject", delay);
            }
            else
            {
                Invoke("DisableButtonGameobject", delay);
            }
        }
        else
        {
            if (newState)
            {
                SetCanBeEnabled(true);
                EnableButtonGameobject();
            }
            else
            {
                followGameobject = false;
                DisableButtonGameobject();
            }
        }
    }

    public void SetButtonAction(Action action)
    {
        if(actionToDo != null)
            button.onClick.RemoveListener(OnClickAction);

        actionToDo = action;
        button.onClick.AddListener(OnClickAction);
    }

    public void SetButtonColor(bool normalColor)
    {
        image.color = normalColor? this.normalColor : specialColor;
    }

    private void OnClickAction()
    {
        actionToDo();
        SetButtonGameobjectState(false, 0f);
    }

  

}

