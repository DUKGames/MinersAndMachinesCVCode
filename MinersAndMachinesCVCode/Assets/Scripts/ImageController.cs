using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageController
{
    [Header("References")]
    [SerializeField] private Image image;

    public void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void SetImageColor(Color color)
    {
        image.color = color;
    }
}
