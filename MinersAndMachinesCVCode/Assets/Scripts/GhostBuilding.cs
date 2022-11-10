using UnityEngine;

public class GhostBuilding : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform spriteCointainer;

    [Header("Variables")]
    [SerializeField] private Color lockedColor;
    [SerializeField] private Color unlockedColor;

    [field: SerializeField]
    public bool CanBeRotated { get; private set; }
    public bool isUnlocked { get; private set; }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void Rotate()
    {
        Vector3 actualScale = spriteCointainer.transform.localScale;
        actualScale.x = -actualScale.x;
        spriteCointainer.transform.localScale = actualScale;
    }

    public float GetXScale()
    {
        return spriteCointainer.transform.localScale.x;
    }

    public void ChangeState(bool canBeBulid)
    {
        if (canBeBulid)
        {
            SetColor(unlockedColor);
        }
        else
        {
            SetColor(lockedColor);
        }

        isUnlocked = canBeBulid;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }
}
