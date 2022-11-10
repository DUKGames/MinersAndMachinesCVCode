using UnityEngine;
using TMPro;

public class UIText : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshPro text;
    [SerializeField] private TextMeshProUGUI uiText;

    public void SetText(string text)
    {
        this.text.text = text;
    }

    public void SetUiText(string text)
    {
        uiText.text = text;
    }

    public void SetTextColor(Color color)
    {
        text.color = color;
    }

    public void SetUiTextColor(Color color)
    {
        uiText.color = color;
    }

    public void SetUiTextFont(TMP_FontAsset font)
    {
        uiText.font = font;
    }
}
