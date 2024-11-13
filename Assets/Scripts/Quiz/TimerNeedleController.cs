using UnityEngine;

public class TimerNeedleController : MonoBehaviour
{
    private QuizTimer quizTimer;
    private float startRotation = 0f;
    private float endRotation = -360f;
    private RectTransform rectTransform;

    private void Start()
    {
        // Get the RectTransform component
        rectTransform = GetComponent<RectTransform>();

        // Get QuizTimer directly from QuizManager GameObject
        quizTimer = GameObject.Find("QuizManager").GetComponent<QuizTimer>();

        // Ensure timer is started - you might want to move this elsewhere depending on your game flow
        if (quizTimer != null)
        {
            quizTimer.StartTimer();
        }

        // Reset initial rotation
        if (rectTransform != null)
        {
            rectTransform.localRotation = Quaternion.Euler(0, 0, startRotation);
        }
    }

    private void Update()
    {
        if (quizTimer == null || rectTransform == null) return;


        float remainingRatio = Mathf.Clamp01(quizTimer.RemainingTime / quizTimer.TotalTime);
        float currentRotation = Mathf.Lerp(endRotation, startRotation, remainingRatio);

        rectTransform.localRotation = Quaternion.Euler(0, 0, currentRotation);
    }
}