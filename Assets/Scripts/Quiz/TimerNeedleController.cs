using UnityEngine;

public class TimerNeedleController : MonoBehaviour
{
    private QuizTimer quizTimer;
    private float startRotation = 0f;
    private float endRotation = -360f;

    private void Start()
    {
        // Find QuizTimer on the Quiz GameObject (parent)
        quizTimer = GetComponentInParent<QuizTimer>();

        // Reset initial rotation
        transform.localRotation = Quaternion.Euler(0, 0, startRotation);
    }

    private void Update()
    {
        if (quizTimer != null && quizTimer.RemainingTime > 0)
        {
            float remainingRatio = quizTimer.RemainingTime / quizTimer.TotalTime;
            float currentRotation = Mathf.Lerp(endRotation, startRotation, remainingRatio);
            transform.localRotation = Quaternion.Euler(0, 0, currentRotation);
        }
    }
}