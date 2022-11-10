using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    public bool spritesLoop { get; set; }
    private Coroutine spriteCoroutine;

    public void SetColorAlpha(float newAlphaValue)
    {
        Color color = spriteRenderer.color;
        color.a = newAlphaValue;
        SetColor(color);
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void SetSpritesLoop(List<Sprite> sprites, float frameTime)
    {
        if(sprites.Count > 1)
        {
            spritesLoop = true;
            spriteCoroutine = StartCoroutine(SpritesLoop(sprites, frameTime));
        }
        else
        {
            SetSprite(sprites[0]);
        }
    }

    public void StopSpritesLoop()
    {
        if (spriteCoroutine != null)
        {
            StopCoroutine(spriteCoroutine);
            spriteCoroutine = null;
        }
    }

    private IEnumerator SpritesLoop(List<Sprite> sprites, float frameTime)
    {
        int index = 0;
        while (spritesLoop)
        {
            if(index >= sprites.Count) 
            {
                index = 0;
            }

            SetSprite(sprites[index]);
            index++;

            yield return new WaitForSeconds(frameTime);
        }
    }

}
