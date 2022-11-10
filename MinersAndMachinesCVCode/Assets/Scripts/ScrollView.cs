using UnityEngine;

public class ScrollView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform content;

    [Header("Variables")]
    [SerializeField] private float maxUpScroll;
    [SerializeField] private float maxDownScroll;

    public void CheckScrollLimit()
    {
        CheckUpScroll();
        CheckDownScroll();
    }

    private void CheckUpScroll()
    {
        if (content.anchoredPosition.y < maxDownScroll) 
            return;

        SetContentYPosition(maxDownScroll);
    }

    private void CheckDownScroll()
    {
        if (content.anchoredPosition.y > maxUpScroll) 
            return;

        SetContentYPosition(maxUpScroll);
    }

    private void SetContentYPosition(float newY)
    {
        Vector3 position = content.anchoredPosition;
        position.y = newY;
        content.anchoredPosition = position;
    }
}
