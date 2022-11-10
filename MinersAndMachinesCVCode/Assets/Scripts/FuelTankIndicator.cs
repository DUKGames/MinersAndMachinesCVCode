using UnityEngine;

public class FuelTankIndicator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    private float defaultY;

    private void Awake()
    {
        defaultY = spriteRenderer.size.y;
    }

    public void SetFuelState(float filledValue)
    {
        float newHeight = defaultY * filledValue;
        SetHeight(newHeight);
    }

    private void SetHeight(float value)
    {
        spriteRenderer.size = new Vector2(spriteRenderer.size.x, value);
    }
}
