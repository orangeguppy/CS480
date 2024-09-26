using UnityEngine;
using UnityEngine.UI;

public class ScrollViewController : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform contentRectTransform;

    public void ResetScroll()
    {
        scrollRect.normalizedPosition = Vector2.one;
    }

    public void UpdateContentSize()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentRectTransform);
    }
}