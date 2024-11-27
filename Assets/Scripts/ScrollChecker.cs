using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollChecker : MonoBehaviour
{
    public ScrollRect scrollRect; // Reference to the ScrollRect component
    public Button targetButton;  // The button to enable/disable
    public float threshold = 0.01f; // Allow a small margin of error

    void Start()
    {
        // Ensure the button starts as disabled
        targetButton.interactable = false;

        // Add a listener to detect scroll changes
        scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
    }

    void OnScrollValueChanged(Vector2 scrollPosition)
    {
        // Check if the scroll view is near the bottom
        if (IsScrolledToBottom())
        {
            targetButton.interactable = true; // Enable the button
        }
        else
        {
            targetButton.interactable = false; // Disable the button
        }
    }

    bool IsScrolledToBottom()
    {
        // Allow a small threshold to account for floating-point inaccuracies
        return scrollRect.verticalNormalizedPosition <= threshold;
    }
}
