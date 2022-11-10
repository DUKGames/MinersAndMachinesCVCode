using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ImageMouse : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public event EventHandler<OnMouseEnterArgs> OnMouseEnter;

    public class OnMouseEnterArgs : EventArgs
    {
        public bool isMouseOnImage;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnMouseEnter?.Invoke(this, new OnMouseEnterArgs { isMouseOnImage = true });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnMouseEnter?.Invoke(this, new OnMouseEnterArgs { isMouseOnImage = false });
    }

}
